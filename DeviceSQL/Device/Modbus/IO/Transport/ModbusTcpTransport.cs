using DeviceSQL.Device.Modbus.Message;
using DeviceSQL.IO.Channels;
using DeviceSQL.IO.Channels.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace DeviceSQL.Device.Modbus.IO
{
    public class ModbusTcpTransport : IModbusTransport
    {
        private object channelLock = new object();
        private bool loggingEnabled = false;
        private ushort transactionId = 0;

        #region Properties

        public bool TracingEnabled
        {
            get { return loggingEnabled; }
            set { loggingEnabled = value; }
        }

        public IChannel Channel { get; set; }
        public int NumberOfRetries { get; set; }
        public int ResponseReadDelayMilliseconds { get; set; }
        public int RequestWriteDelayMilliseconds { get; set; }
        public int WaitToRetryMilliseconds { get; set; }

        #endregion

        #region Constructor(s)

        internal ModbusTcpTransport() { }
        internal ModbusTcpTransport(IChannel channel)
        {
            Channel = channel;
        }

        #endregion

        #region Main Transport Methods

        public TResponseMessage UnicastMessage<TResponseMessage>(IModbusRequestMessage requestMessage)
                where TResponseMessage : IModbusResponseMessage, new()
        {
            if (TracingEnabled)
            {
                Trace.WriteLine($"Function {requestMessage.FunctionCode} Request Started At: {DateTime.Now:O}", "Transport");
            }

            var lastException = (Exception)null;
            var transactionStopWatch = Stopwatch.StartNew();
            IModbusResponseMessage response = default(TResponseMessage);
            int attempt = 0;

            lock (channelLock)
            {
                do
                {
                    try
                    {
                        attempt++;

                        // Increment and reset transaction ID if it exceeds 65535 (maximum for a 16-bit unsigned integer)
                        transactionId = (ushort)((transactionId + 1) % 65536);

                        HandleWriteDelay(requestMessage); // Handle write delay with tracing

                        Write(requestMessage, transactionId); // Pass transaction ID to write

                        HandleReadDelay(requestMessage); // Handle read delay with tracing

                        response = ReadResponse<TResponseMessage>(requestMessage, transactionId); // Pass transaction ID to read

                        if (response is ModbusErrorResponse modbusError)
                        {
                            throw new ModbusSlaveException(modbusError);
                        }

                        ValidateResponse(requestMessage, response);
                        transactionStopWatch.Stop();

                        if (TracingEnabled)
                        {
                            Trace.WriteLine($"Function {requestMessage.FunctionCode} Request Completed in: {transactionStopWatch.Elapsed.TotalMilliseconds}ms", "Transport");
                        }

                        return (TResponseMessage)response;
                    }
                    catch (Exception e)
                    {
                        lastException = e;
                        Trace.WriteLine($"Function {requestMessage.FunctionCode} Request Error: {e.Message}", "Transport");

                        if (IsRetryableException(e) && attempt <= NumberOfRetries)
                        {
                            TimedThreadBlocker.Wait(WaitToRetryMilliseconds); // Wait before retrying
                        }
                        else
                        {
                            throw;
                        }
                    }
                } while (attempt <= NumberOfRetries);
            }

            throw new TimeoutException("Device not responding to requests", lastException);
        }

        #endregion

        #region Supporting Methods

        private void HandleWriteDelay(IModbusRequestMessage requestMessage)
        {
            if (RequestWriteDelayMilliseconds > 0 && TracingEnabled)
            {
                Trace.WriteLine($"Function {requestMessage.FunctionCode} Request Write Delay: {RequestWriteDelayMilliseconds}ms", "Transport");
            }

            TimedThreadBlocker.Wait(RequestWriteDelayMilliseconds);
        }

        private void HandleReadDelay(IModbusRequestMessage requestMessage)
        {
            if (ResponseReadDelayMilliseconds > 0 && TracingEnabled)
            {
                Trace.WriteLine($"Function {requestMessage.FunctionCode} Request Read Delay: {ResponseReadDelayMilliseconds}ms", "Transport");
            }

            TimedThreadBlocker.Wait(ResponseReadDelayMilliseconds);
        }

        private bool IsRetryableException(Exception e)
        {
            return e is FormatException || e is NotImplementedException || e is TimeoutException || e is IOException;
        }

        #endregion

        #region Write/Read Methods with Transaction ID

        internal void Write(IModbusMessage message, ushort transactionId)
        {
            var frameBytes = BuildTcpMessageFrame(message, transactionId); // Build message with MBAP header
            var frameLength = frameBytes.Length;
            var bufferedBytes = new byte[frameLength];
            Buffer.BlockCopy(frameBytes, 0, bufferedBytes, 0, frameLength);
            Channel.Write(ref bufferedBytes, 0, frameLength);
        }

        internal IModbusResponseMessage ReadResponse<TResponseMessage>(IModbusRequestMessage requestMessage, ushort transactionId)
            where TResponseMessage : IModbusResponseMessage, new()
        {
            var mbapHeader = ReadMbapHeader(requestMessage); // Read MBAP Header

            // Extract and convert the Transaction ID from the MBAP header
            var responseTransactionId = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(mbapHeader, 0));
            if (responseTransactionId != transactionId)
            {
                throw new IOException("Mismatched Transaction ID in response");
            }

            // Read the remaining PDU and validate it
            var response = ReadPduAndValidate<TResponseMessage>(mbapHeader, requestMessage);
            return response;
        }

        #endregion

        #region MBAP & PDU Methods

        private byte[] BuildTcpMessageFrame(IModbusMessage message, ushort transactionId)
        {
            List<byte> messageFrame = new List<byte>();

            // MBAP Header
            messageFrame.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)transactionId)));// Transaction ID (2 bytes)
            messageFrame.AddRange(new byte[] { 0x00, 0x00 }); // Protocol ID (2 bytes, always 0)

            var length = (ushort)(message.ProtocolDataUnit.Length + (message.IsExtendedUnitId ? 2 : 1));
            messageFrame.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)length))); // Length (2 bytes)

            // Add Unit ID (1 or 2 bytes depending on IsExtendedUnitId)
            if (message.IsExtendedUnitId)
            {
                messageFrame.AddRange(BitConverter.GetBytes(message.UnitId)); // 2-byte Unit ID
            }
            else
            {
                messageFrame.Add((byte)message.UnitId); // 1-byte Unit ID
            }

            // Add the actual Modbus PDU
            messageFrame.AddRange(message.ProtocolDataUnit);

            return messageFrame.ToArray();
        }

        private byte[] ReadMbapHeader(IModbusRequestMessage requestMessage)
        {
            // Read MBAP Header (7 bytes if no extended Unit ID, 8 bytes if extended)
            int headerLength = requestMessage.IsExtendedUnitId ? 8 : 7;
            return Read(headerLength, 0); // Read the required number of bytes for MBAP
        }

        private TResponseMessage ReadPduAndValidate<TResponseMessage>(byte[] mbapHeader, IModbusRequestMessage requestMessage)
            where TResponseMessage : IModbusResponseMessage, new()
        {
            // Extract the total length from the MBAP header (correcting for network byte order)
            var totalLength = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(mbapHeader, 4));

            // Subtract 1 byte for the Unit ID
            var pduLength = totalLength - 1;

            // Read the remaining PDU (Function Code + Data)
            var pdu = Read(pduLength, 0);

            // Combine MBAP header and PDU to create the full response frame
            var responseFrame = mbapHeader.Skip(6).Concat(pdu).ToArray();

            // Create and return the response message
            return CreateResponse<TResponseMessage>(responseFrame, requestMessage);
        }

        private byte[] Read(int count, int sequence)
        {
            var frameBytes = new byte[count];
            var bufferedBytes = new byte[count];
            int numBytesRead = Channel.Read(ref frameBytes, 0, count, sequence);
            Buffer.BlockCopy(frameBytes, 0, bufferedBytes, 0, count);
            return bufferedBytes;
        }

        #endregion

        #region Response Creation and Validation

        internal virtual TResponseMessage CreateResponse<TResponseMessage>(byte[] frame, IModbusRequestMessage requestMessage)
            where TResponseMessage : IModbusResponseMessage, new()
        {
            // Response creation logic based on function code
            byte functionCode = requestMessage.IsExtendedUnitId ? frame[2] : frame[1];

            IModbusResponseMessage response;

            switch (functionCode)
            {
                case Device.ReadCoils:
                    response = ModbusMessageFactory.CreateModbusResponseMessage<ReadCoilRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.ReadDiscreteInputs:
                    response = ModbusMessageFactory.CreateModbusResponseMessage<ReadDiscreteInputRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.WriteSingleCoil:
                    response = ModbusMessageFactory.CreateModbusResponseMessage<WriteBooleanResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.ReadInputRegisters:
                    response = ModbusMessageFactory.CreateModbusResponseMessage<ReadInputRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.ReadHoldingRegisters:
                    if (requestMessage is ReadShortsRequest)
                    {
                        response = ModbusMessageFactory.CreateModbusResponseMessage<ReadShortsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    }
                    else if(requestMessage is ReadFloatsRequest)
                    {
                        response = ModbusMessageFactory.CreateModbusResponseMessage<ReadFloatsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    }
                    else if(requestMessage is ReadLongsRequest)
                    {
                        response = ModbusMessageFactory.CreateModbusResponseMessage<ReadLongsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    }
                    else
                    {
                        response = ModbusMessageFactory.CreateModbusResponseMessage<ReadHoldingRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    }
                    break;
                default:
                    throw new FormatException($"Unknown Function Code ({functionCode}).");
            }

            return (TResponseMessage)response;
        }

        internal void ValidateResponse(IModbusRequestMessage request, IModbusResponseMessage response)
        {
            if (request.FunctionCode != response.FunctionCode)
                throw new IOException($"Received response with unexpected function code. Expected {request.FunctionCode}, received {response.FunctionCode}.");

            if (request.UnitId != response.UnitId)
                throw new IOException($"Response source unit does not match request. Expected {request.UnitId}, received {response.UnitId}.");

            request.ValidateResponse(response);
        }

        #endregion
    }
}
