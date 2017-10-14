#region Imported Types

using DeviceSQL.Device.MODBUS.Message;
using DeviceSQL.IO.Channels;
using DeviceSQL.IO.Channels.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

#endregion

namespace DeviceSQL.Device.MODBUS.IO
{
    public class Transport : ITransport
    {

        #region Constants

        public const int RESPONSE_HEADER_LENGTH = 2;
        public const int RESPONSE_HEADER_LENGTH_WITH_EXTENDED_UNIT_ID = 3;
        public const int RESPONSE_DATA_START_LENGTH = 2;

        #endregion

        #region Fields

        private bool loggingEnabled = false;
        private object channelLock = new object();

        #endregion

        #region Properties
        public bool TracingEnabled
        {
            get
            {
                return loggingEnabled;
            }
            set
            {
                loggingEnabled = value;
            }
        }

        public IChannel Channel
        {
            get;
            set;
        }

        public int NumberOfRetries
        {
            get;
            set;
        }

        public int ResponseReadDelayMilliseconds
        {
            get;
            set;
        }

        public int RequestWriteDelayMilliseconds
        {
            get;
            set;
        }

        public int WaitToRetryMilliseconds
        {
            get;
            set;
        }

        #endregion

        #region Constructor(s)

        internal Transport()
        {
        }

        internal Transport(IChannel channel)
        {
            Channel = channel;
        }

        #endregion

        #region Transport Methods

        internal virtual TResponseMessage UnicastMessage<TResponseMessage>(IMODBUSRequestMessage requestMessage)
where TResponseMessage : IMODBUSResponseMessage, new()
        {
            if (TracingEnabled)
            {
                Trace.WriteLine(string.Format("Function {0} Request Started At: {1}", requestMessage.FunctionCode.ToString(), DateTime.Now.ToString("O"), "Transport"));
            }
            var lastException = (Exception)null;
            var transactionStopWatch = Stopwatch.StartNew();
            IMODBUSResponseMessage response = default(TResponseMessage);
            int attempt = 0;
            bool success = false;
            lock (channelLock)
            {
                do
                {
                    try
                    {
                        attempt++;
                        if (RequestWriteDelayMilliseconds > 0)
                        {
                            if (TracingEnabled)
                            {
                                Trace.WriteLine(string.Format("Function {0} Request Write Delay: {1}ms", requestMessage.FunctionCode.ToString(), RequestWriteDelayMilliseconds.ToString("0.0")), "Transport");
                            }
                            TimedThreadBlocker.Wait(RequestWriteDelayMilliseconds);
                        }
                        Write(requestMessage);
                        if (ResponseReadDelayMilliseconds > 0)
                        {
                            if (TracingEnabled)
                            {
                                Trace.WriteLine(string.Format("Function {0} Request Read Delay: {1}", requestMessage.FunctionCode.ToString(), ResponseReadDelayMilliseconds.ToString("0.0")), "Transport");
                            }
                            TimedThreadBlocker.Wait(RequestWriteDelayMilliseconds);
                        }
                        response = ReadResponse<TResponseMessage>(requestMessage);
                        if (response is MODBUSErrorResponse)
                        {
                            throw new MODBUSSlaveException(response as MODBUSErrorResponse);
                        }
                        else
                        {
                            ValidateResponse(requestMessage, response);
                            transactionStopWatch.Stop();
                            if (TracingEnabled)
                            {
                                Trace.WriteLine(string.Format("Function {0} Request Completed in: {1}ms", requestMessage.FunctionCode.ToString(), transactionStopWatch.Elapsed.TotalMilliseconds.ToString()), "Transport");
                            }

                            return (TResponseMessage)response;
                        }
                    }
                    catch (Exception e)
                    {
                        lastException = e;
                        Trace.WriteLine(string.Format("Function {0} Request Error: {1}", requestMessage.FunctionCode.ToString(), e.Message), "Transport");
                        if (e is FormatException ||
                            e is NotImplementedException ||
                            e is TimeoutException ||
                            e is IOException)
                        {
                            if (attempt > NumberOfRetries)
                            {
                                goto Finish;
                            }
                            else
                            {
                                TimedThreadBlocker.Wait(WaitToRetryMilliseconds);
                            }
                        }
                        else
                        {
                            throw;
                        }
                    }
                    if (TracingEnabled)
                    {
                        Trace.WriteLine(string.Format("Function {0} Request Retry Number {1}", requestMessage.FunctionCode.ToString(), attempt.ToString()), "Transport");
                    }
                } while (!success && NumberOfRetries > attempt);
            }
            Finish:
            throw new TimeoutException("Device not responding to requests", lastException);
        }

        internal virtual IMODBUSResponseMessage CreateResponse<TResponseMessage>(byte[] frame, IMODBUSRequestMessage requestMessage)
                where TResponseMessage : IMODBUSResponseMessage, new()
        {

            byte functionCode = (requestMessage.IsExtendedUnitId ? frame[2] : frame[1]);

            IMODBUSResponseMessage response;

            switch (functionCode)
            {
                case Device.ReadCoils:
                    response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadCoilRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.ReadDiscreteInputs:
                    response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadDiscreteInputRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.WriteSingleCoil:
                    response = MODBUSMessageFactory.CreateMODBUSResponseMessage<WriteBooleanResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.ReadInputRegisters:
                    response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadInputRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                case Device.ReadHoldingRegisters:
                    switch (requestMessage.DataType)
                    {
                        case Device.DataType.EventArchive:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadEventArchiveResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                        case Device.DataType.HistoryArchive:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadHistoryArchiveResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                        case Device.DataType.Short:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadShortsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                        case Device.DataType.Long:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadLongsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                        case Device.DataType.String:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadStringResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                        case Device.DataType.Float:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadFloatsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                        default:
                            response = MODBUSMessageFactory.CreateMODBUSResponseMessage<ReadHoldingRegistersResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                            break;
                    }
                    break;
                case Device.WriteMultipleRegisters:
                    response = MODBUSMessageFactory.CreateMODBUSResponseMessage<WriteFloatsResponse>(frame, requestMessage.IsExtendedUnitId, requestMessage);
                    break;
                default:
                    if (functionCode > Device.ExceptionOffset)
                    {
                        response = MODBUSMessageFactory.CreateMODBUSResponseMessage<MODBUSErrorResponse>(frame, requestMessage.IsExtendedUnitId);
                        break;
                    }
                    else
                    {
                        throw new FormatException(string.Format("Unknown Function Code ({0}).", functionCode.ToString()));
                    }
            }

            return response;
        }

        internal void ValidateResponse(IMODBUSRequestMessage request, IMODBUSResponseMessage response)
        {

            if (request.FunctionCode != response.FunctionCode)
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Received response with unexpected function code. Expected {0}, received {1}.", request.FunctionCode, response.FunctionCode));

            if (request.UnitId != response.UnitId)
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response source unit does not match request. Expected {0}, received {1}.", response.UnitId, request.UnitId));

            request.ValidateResponse(response);

        }

        public static int ResponseBytesToRead(byte[] frameStart, bool isExtendedUnitId)
        {
            byte functionCode = isExtendedUnitId ? frameStart[2] : frameStart[1];

            // exception response
            if (functionCode > Device.ExceptionOffset)
            {
                return 1;
            }

            int numBytes;
            switch (functionCode)
            {
                case Device.ReadCoils:
                case Device.ReadDiscreteInputs:
                case Device.ReadInputRegisters:
                case Device.ReadHoldingRegisters:
                    numBytes = isExtendedUnitId ? (frameStart[3] + 1) : (frameStart[2] + 1);
                    break;
                case Device.WriteSingleCoil:
                case Device.WriteMultipleRegisters:
                    numBytes = 4;
                    break;
                default:
                    throw new NotImplementedException(string.Format("Function code {0} not supported.", functionCode));
            }

            return numBytes;
        }

        internal IMODBUSResponseMessage ReadResponse<TResponseMessage>(IMODBUSRequestMessage requestMessage)
            where TResponseMessage : IMODBUSResponseMessage, new()
        {
            var sequence = 0;
            var referenceHeader = requestMessage.IsExtendedUnitId ? BitConverter.GetBytes(requestMessage.UnitId).Concat(new byte[] { requestMessage.FunctionCode }) : new byte[] { Convert.ToByte(requestMessage.UnitId) }.Concat(new byte[] { requestMessage.FunctionCode });
            var errorHeader = requestMessage.IsExtendedUnitId ? BitConverter.GetBytes(requestMessage.UnitId).Concat(new byte[] { Convert.ToByte(requestMessage.FunctionCode + 128) }) : new byte[] { Convert.ToByte(requestMessage.UnitId) }.Concat(new byte[] { Convert.ToByte(requestMessage.FunctionCode + 128) });
            var header = Read(requestMessage.IsExtendedUnitId ? RESPONSE_HEADER_LENGTH_WITH_EXTENDED_UNIT_ID : RESPONSE_HEADER_LENGTH, sequence);

            while ((!header.SequenceEqual(referenceHeader)) && (!header.SequenceEqual(errorHeader)))
            {
                sequence++;
                var streamByte = Read(1, sequence)[0];
                if (requestMessage.IsExtendedUnitId)
                {
                    header = new byte[] { header[1], header[2], streamByte };
                }
                else
                {
                    header = new byte[] { header[1], streamByte };
                }
            }

            var frameStart = header.Concat(Read(RESPONSE_DATA_START_LENGTH, sequence)).ToArray();
            sequence++;
            var numberOfBytesToRead = ResponseBytesToRead(frameStart, requestMessage.IsExtendedUnitId);
            var frameEnd = Read(numberOfBytesToRead, sequence);
            sequence++;
            var frameBytes = frameStart.Concat(frameEnd).ToArray();
            sequence++;

            var numberOfBytesAvailable = Channel.NumberOfBytesAvailable;
            while (numberOfBytesAvailable > 0)
            {
                sequence++;
                Read(numberOfBytesAvailable, (-1 * sequence));
                Thread.Sleep(0);
                numberOfBytesAvailable = Channel.NumberOfBytesAvailable;
            }

            var responseMessage = CreateResponse<TResponseMessage>(frameBytes, requestMessage);
            if (!ChecksumsMatch(responseMessage, frameBytes))
            {
                throw new IOException("Checksum Error");
            }

            return responseMessage;
        }

        internal byte[] BuildMessageFrame(IMODBUSMessage message)
        {
            List<byte> messageBody = new List<byte>();
            messageBody.AddRange(message.MessageFrame);

            using (Crc16 Hash = new Crc16())
            {
                Hash.Initialize();
                messageBody.AddRange(Hash.ComputeHash(message.MessageFrame, 0, messageBody.Count));
            }

            return messageBody.ToArray();
        }

        internal byte[] Read(int count, int sequence)
        {
            var frameBytes = new byte[count];
            var bufferedBytes = new byte[count];
            int numBytesRead = Channel.Read(ref frameBytes, 0, count, sequence);
            Buffer.BlockCopy(frameBytes, 0, bufferedBytes, 0, count);
            return bufferedBytes;
        }

        internal void Write(IMODBUSMessage message)
        {
            var frameBytes = BuildMessageFrame(message);
            var frameLength = frameBytes.Length;
            var bufferedBytes = new byte[frameLength];
            Buffer.BlockCopy(frameBytes, 0, bufferedBytes, 0, frameLength);
            Channel.Write(ref bufferedBytes, 0, frameLength);
        }

        #endregion

        #region Helper Methods

        internal bool ChecksumsMatch(IMessage message, byte[] messageFrame)
        {
            if (message.MessageFrame.Length < (messageFrame.Length - 2))
            {
                return false;
            }
            // Compute the hash
            using (Crc16 Hash = new Crc16())
            {
                Hash.Initialize();
                var crc = BitConverter.ToUInt16(Hash.ComputeHash(message.MessageFrame, 0, (messageFrame.Length - 2)), 0);
                return (BitConverter.ToUInt16(messageFrame, messageFrame.Length - 2) == crc);
            }
        }

        #endregion

    }
}
