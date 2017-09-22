#region Imported Types

using DeviceSQL.Device.ROC.Message;
using DeviceSQL.Device.ROC.Utility;
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

namespace DeviceSQL.Device.ROC.IO
{
    public class Transport : ITransport
    {

        #region Fields

        private bool loggingEnabled = false;
        private object channelLock = new object();

        #endregion

        #region Constants

        public const byte CRC_LENGTH = 2;
        public const int RESPONSE_FRAME_START_LENGTH = 5;

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

        #region Transport Methods

        internal virtual TResponseMessage UnicastMessage<TResponseMessage>(IROCRequestMessage requestMessage)
            where TResponseMessage : IROCResponseMessage, new()
        {
            if (TracingEnabled)
            {
                Trace.WriteLine(string.Format("ROCMaster.Transport,WriteDelay,{0},{1}", requestMessage.OpCode.ToString(), DateTime.Now.ToString("O"), "Transport"));
            }
            var lastException = (Exception)null;
            var transactionStopWatch = Stopwatch.StartNew();
            IROCResponseMessage responseMessage = default(TResponseMessage);
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
                                Trace.WriteLine(string.Format("ROCMaster.Transport,WriteDelay,{0},{1}", requestMessage.OpCode.ToString(), RequestWriteDelayMilliseconds.ToString("0.0")));
                            }
                            TimedThreadBlocker.Wait(RequestWriteDelayMilliseconds);
                        }
                        Write(requestMessage);
                        if (ResponseReadDelayMilliseconds > 0)
                        {
                            if (TracingEnabled)
                            {                                                          
                                Trace.WriteLine(string.Format("ROCMaster.Transport,ReadDelay,{0},{1}", requestMessage.OpCode.ToString(), ResponseReadDelayMilliseconds.ToString("0.0")));
                            }
                            TimedThreadBlocker.Wait(RequestWriteDelayMilliseconds);
                        }
                        responseMessage = ReadResponse<TResponseMessage>(requestMessage);
                        if (responseMessage.OpCode == 255)
                        {
                            var opCode255Response = responseMessage as OpCode255Response;
                            if (opCode255Response != null)
                            {
                                throw new OpCode255Exception(opCode255Response);
                            }
                        }
                        else
                        {
                            ValidateResponse(requestMessage, responseMessage);
                            transactionStopWatch.Stop();
                            if (TracingEnabled)
                            {
                                Trace.WriteLine(string.Format("ROCMaster.Transport,Read,{0},{1},{3},{4}", requestMessage.OpCode.ToString(), transactionStopWatch.Elapsed.TotalMilliseconds.ToString(), HexConverter.ToHexString(requestMessage.ProtocolDataUnit), HexConverter.ToHexString(responseMessage.ProtocolDataUnit)));
                            }
                            return (TResponseMessage)responseMessage;
                        }
                    }
                    catch (Exception e)
                    {
                        lastException = e;
                        Trace.WriteLine(string.Format("ROCMaster.Transport.Error,{0},{1}", requestMessage.OpCode.ToString(), e.Message), "Transport");
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
                        Trace.WriteLine(string.Format("ROCMaster.Transport.Warning,Retry,{0},{1}", requestMessage.OpCode.ToString(), attempt.ToString()));
                    }
                } while (!success && NumberOfRetries > attempt);
            }
            Finish:
            throw new TimeoutException("Device not responding to requests", lastException);
        }

        internal virtual IROCResponseMessage CreateResponse<TResponseMessage>(byte[] frame, IROCRequestMessage requestMessage)
            where TResponseMessage : IROCResponseMessage, new()
        {
            byte opCode = frame[4];

            IROCResponseMessage response;

            switch (opCode)
            {
                case Device.OpCode7:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode007Response>(frame);
                    break;
                case Device.OpCode8:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode008Response>(frame);
                    break;
                case Device.OpCode17:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode017Response>(frame);
                    break;
                case Device.OpCode80:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode080Response>(frame, requestMessage);
                    break;
                case Device.OpCode120:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode120Response>(frame);
                    break;
                case Device.OpCode118:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode118Response>(frame);
                    break;
                case Device.OpCode119:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode119Response>(frame);
                    break;
                case Device.OpCode121:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode121Response>(frame);
                    break;
                case Device.OpCode122:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode122Response>(frame);
                    break;
                case Device.OpCode126:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode126Response>(frame, requestMessage);
                    break;
                case Device.OpCode130:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode130Response>(frame, requestMessage);
                    break;
                case Device.OpCode131:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode131Response>(frame, requestMessage);
                    break;
                case Device.OpCode132:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode132Response>(frame, requestMessage);
                    break;
                case Device.OpCode136:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode136Response>(frame, requestMessage);
                    break;
                case Device.OpCode139:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode139Response>(frame, requestMessage);
                    break;
                case Device.OpCode165:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode165Response>(frame, requestMessage);
                    break;
                case Device.OpCode166:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode166Response>(frame);
                    break;
                case Device.OpCode167:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode167Response>(frame, requestMessage);
                    break;
                case Device.OpCode180:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode180Response>(frame, requestMessage);
                    break;
                case Device.OpCode181:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode181Response>(frame);
                    break;
                case Device.OpCode255:
                    response = ROCMessageFactory.CreateROCResponseMessage<OpCode255Response>(frame, requestMessage);
                    break;
                default:
                    throw new FormatException(string.Format("Unknown OpCode ({0}).", opCode.ToString()));
            }

            return response;
        }

        internal void ValidateResponse(IROCRequestMessage request, IROCResponseMessage response)
        {

            if (request.OpCode != response.OpCode)
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Received response with unexpected OpCode. Expected {0}, received {1}.", request.OpCode, response.OpCode));

            if (request.DestinationUnit != response.SourceUnit)
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response source unit does not match request. Expected {0}, received {1}.", response.DestinationUnit, request.SourceUnit));

            if (request.DestinationGroup != response.SourceGroup)
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response source group does not match request. Expected {0}, received {1}.", response.DestinationGroup, request.SourceGroup));

            request.ValidateResponse(response);

        }

        internal static int ResponseBytesToRead(byte[] frameStart)
        {
            return frameStart[5] + CRC_LENGTH;
        }

        internal IROCResponseMessage ReadResponse<TResponseMessage>(IROCRequestMessage requestMessage)
            where TResponseMessage : IROCResponseMessage, new()
        {
            var sequence = 0;
            var referenceHeader = new byte[] { requestMessage.SourceUnit, requestMessage.SourceGroup, requestMessage.DestinationUnit, requestMessage.DestinationGroup, requestMessage.OpCode };
            var errorHeader = new byte[] { requestMessage.SourceUnit, requestMessage.SourceGroup, requestMessage.DestinationUnit, requestMessage.DestinationGroup, Device.OpCode255 };
            var header = Read(RESPONSE_FRAME_START_LENGTH, sequence);

            while ((!header.SequenceEqual(referenceHeader)) && (!header.SequenceEqual(errorHeader)))
            {
                sequence++;
                var streamByte = Read(1, sequence)[0];
                header = new byte[] { header[1], header[2], header[3], header[4], streamByte };
            }

            if (header.SequenceEqual(errorHeader))
            {
                var errorBytes = new List<byte>();
                var errorFrameBytes = new List<byte>(header);
                var numberOfBytesAvailable = Channel.NumberOfBytesAvailable;
                while (numberOfBytesAvailable > 0)
                {
                    sequence++;
                    errorBytes.AddRange(Read(numberOfBytesAvailable, (-1 * sequence)));
                    Thread.Sleep(500);
                    numberOfBytesAvailable = Channel.NumberOfBytesAvailable;
                }

                for (var i = 0; errorBytes.Count > i; i++)
                {
                    errorFrameBytes.Add(errorBytes[i]);

                    if (ChecksumsMatch(errorFrameBytes.ToArray(), new byte[] { errorFrameBytes[errorFrameBytes.Count - 2], errorFrameBytes[errorFrameBytes.Count - 1] }))
                    {
                        return CreateResponse<TResponseMessage>(errorFrameBytes.ToArray(), requestMessage);
                    }
                }
                throw new IOException("Checksum Error");
            }
            else
            {
                sequence++;
                byte[] frameStart = header.Concat(Read(1, sequence)).ToArray();
                sequence++;
                byte[] frameEnd = Read(ResponseBytesToRead(frameStart), sequence);
                byte[] frameBytes = frameStart.Concat(frameEnd).ToArray();

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
                else
                {
                    return responseMessage;
                }
            }
        }

        internal byte[] BuildMessageFrame(IROCMessage message)
        {
            var messageBody = new List<byte>();
            messageBody.AddRange(message.MessageFrame);

            using (CRC16 Hash = new CRC16())
            {
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

        internal void Write(IROCMessage message)
        {
            var frameBytes = BuildMessageFrame(message);
            var frameLength = frameBytes.Length;
            var bufferedBytes = new byte[frameLength];
            Buffer.BlockCopy(frameBytes, 0, bufferedBytes, 0, frameLength);
            Channel.Write(ref bufferedBytes, 0, frameLength);
        }

        #endregion

        #region Helper Methods

        internal bool ChecksumsMatch(IROCMessage message, byte[] messageFrame)
        {
            if (messageFrame.Length < message.MessageFrame.Length)
            {
                return false;
            }
            // Compute the hash
            using (CRC16 Hash = new CRC16())
            {
                var crc = BitConverter.ToUInt16(Hash.ComputeHash(message.MessageFrame, 0, (messageFrame.Length - CRC_LENGTH)), 0);
                return (BitConverter.ToUInt16(messageFrame, messageFrame.Length - CRC_LENGTH) == crc);
            }
        }

        internal bool ChecksumsMatch(byte[] messageFrame, byte[] referenceCrcBytes)
        {
            using (CRC16 Hash = new CRC16())
            {
                var crc = BitConverter.ToUInt16(Hash.ComputeHash(messageFrame, 0, (messageFrame.Length - CRC_LENGTH)), 0);
                return crc == BitConverter.ToUInt16(referenceCrcBytes, 0);
            }
        }

        #endregion

    }
}
