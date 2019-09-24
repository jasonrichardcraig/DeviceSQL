#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;

#endregion

namespace DeviceSQL.IO.Channels
{
    public class SerialPortChannel : IChannel
    {

        #region Fields

        private string name = "";
        private bool tracingEnabled = false;
        private SerialPort serialPort = new SerialPort();
        private double numberOfInterFrameSpacingCharacters = 3.5;
        private object lockObject = new object();

        #endregion

        #region Properties

        public object LockObject
        {
            get
            {
                return lockObject;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool TracingEnabled
        {
            get
            {
                return tracingEnabled;
            }
            set
            {
                tracingEnabled = value;
            }
        }

        public SerialPort SerialPort
        {
            get { return serialPort; }
            set { serialPort = value; }
        }

        public int ReadTimeout
        {
            get { return SerialPort.ReadTimeout; }
            set { SerialPort.ReadTimeout = value; }
        }

        public int WriteTimeout
        {
            get { return SerialPort.WriteTimeout; }
            set { SerialPort.WriteTimeout = value; }
        }

        public string ConnectionString
        {
            get
            {
                return string.Format("{0}://{1},{2},{3},{4}", SerialPort.PortName.ToLower().Trim(), SerialPort.BaudRate.ToString(), SerialPort.DataBits.ToString(), SerialPort.Parity.ToString(), SerialPort.StopBits.ToString());
            }
        }

        public double NumberOfInterFrameSpacingCharacters
        {
            get
            {
                return numberOfInterFrameSpacingCharacters;
            }

            set
            {
                numberOfInterFrameSpacingCharacters = value;
            }
        }

        public int NumberOfBytesAvailable
        {
            get
            {
                return SerialPort.BytesToRead;
            }
        }

        #endregion

        #region IO Methods

        public void Write(ref byte[] buffer, int offset, int count)
        {
            var masterStopWatch = Stopwatch.StartNew();
            var startTime = DateTime.Now;
            var numberOfActualBitsPerByte = (1 + (SerialPort.Parity == Parity.None ? 0 : 1) + SerialPort.DataBits + (SerialPort.StopBits == StopBits.None ? 0 : SerialPort.StopBits == StopBits.One ? 1 : SerialPort.StopBits == StopBits.OnePointFive ? 1.5 : 2));
            var transmitTimeMilliseconds = ((count * numberOfActualBitsPerByte) / SerialPort.BaudRate) * 1000;
            var bufferLength = buffer.Length;
            var writeBuffer = new byte[bufferLength];
            Buffer.BlockCopy(buffer, 0, writeBuffer, 0, bufferLength);
            SerialPort.BaseStream.Write(writeBuffer, offset, count);
            SerialPort.BaseStream.Flush();
            while (SerialPort.BytesToWrite > 0)
            {
                Thread.Sleep(0);
            }

            TimedThreadBlocker.Wait((int)transmitTimeMilliseconds);

            masterStopWatch.Stop();
            if (TracingEnabled)
            {
                Trace.WriteLine(string.Format("Channel,{0},{1},{2},ChannelWrite,{3},{4},{5},SerialPortChannel", Name, startTime.ToString("O"), (1000.0 * (((double)masterStopWatch.ElapsedTicks) * (1.0 / ((double)Stopwatch.Frequency)))), 0, count, HexConverter.ToHexString(buffer)));
            }
        }

        public int Read(ref byte[] buffer, int offset, int count, int sequence)
        {
            var masterStopWatch = Stopwatch.StartNew();
            var startTime = DateTime.Now;
            var bytesRead = new List<byte>();
            var numberOfActualBitsPerByte = (1 + (SerialPort.Parity == Parity.None ? 0 : 1) + SerialPort.DataBits + (SerialPort.StopBits == StopBits.None ? 0 : SerialPort.StopBits == StopBits.One ? 1 : SerialPort.StopBits == StopBits.OnePointFive ? 1.5 : 2));
            var receiveTimeMilliseconds = ((count * numberOfActualBitsPerByte) / SerialPort.BaudRate) * 1000;

            TimedThreadBlocker.Wait((int)receiveTimeMilliseconds);

            var timeoutStopWatch = Stopwatch.StartNew();

            while (ReadTimeout > timeoutStopWatch.ElapsedMilliseconds)
            {
                var byteValue = -1;
                try
                {
                    byteValue = SerialPort.ReadByte();
                    if (byteValue >= 0)
                    {
                        bytesRead.Add((byte)byteValue);
                    }
                    if (count == bytesRead.Count)
                    {
                        goto Finish;
                    }
                }
                catch (TimeoutException toex)
                {
                    masterStopWatch.Stop();

                    if (bytesRead.Count > 0)
                    {
                        Buffer.BlockCopy(bytesRead.ToArray(), 0, buffer, offset, bytesRead.Count);
                    }

                    if (TracingEnabled)
                    {
                        Trace.WriteLine(string.Format("Channel,{0},{1},{2},ChannelRead,{3},{4},{5},SerialPortChannel", Name, startTime.ToString("O"), (1000.0 * (((double)masterStopWatch.ElapsedTicks) * (1.0 / ((double)Stopwatch.Frequency)))), sequence, bytesRead.Count, HexConverter.ToHexString(bytesRead.ToArray())));
                    }
                    throw toex;
                }
                Thread.Sleep(0);
            }

            Finish:
            timeoutStopWatch.Stop();
            masterStopWatch.Stop();

            if (bytesRead.Count > 0)
            {
                Buffer.BlockCopy(bytesRead.ToArray(), 0, buffer, offset, bytesRead.Count);
            }

            if (TracingEnabled)
            {
                Trace.WriteLine(string.Format("Channel,{0},{1},{2},ChannelRead,{3},{4},{5},SerialPortChannel", Name, startTime.ToString("O"), (1000.0 * (((double)masterStopWatch.ElapsedTicks) * (1.0 / ((double)Stopwatch.Frequency)))), sequence, bytesRead.Count, HexConverter.ToHexString(bytesRead.ToArray())));
            }

            if (count != bytesRead.Count)
            {
                throw new TimeoutException("Read Timeout");
            }
            else
            {
                return count;
            }

        }

        #endregion

        #region Helper Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (SerialPort != null)
                {
                    if (SerialPort.IsOpen)
                    {
                        try
                        {
                            SerialPort.Close();
                        }
                        catch (Exception ex)
                        {
                            Trace.Write(string.Format("Error closing serial port: {0}", ex.Message));
                        }
                    }
                    ((IDisposable)SerialPort).Dispose();
                    serialPort = null;
                }
            }
        }

        #endregion

    }
}
