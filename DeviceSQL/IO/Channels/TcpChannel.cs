﻿#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;

#endregion

namespace DeviceSQL.IO.Channels
{
    public partial class TcpChannel : IChannel
    {

        #region Fields

        private string name = "";
        private bool tracingEnabled = false;
        protected TcpClient tcpClient = new TcpClient();
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

        public string HostName
        {
            get;
            set;
        }

        public int HostPort
        {
            get;
            set;
        }

        public int ConnectionAttempts
        {
            get;
            set;
        }

        public int ConnectionRetryDelay
        {
            get;
            set;
        }

        internal int TcpClientReadTimeout
        {
            get;
            set;
        }

        internal int TcpClientWriteTimeout
        {
            get;
            set;
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


        public TcpClient TcpClient
        {
            get { return tcpClient; }
            internal set
            {
                tcpClient = value;
            }
        }

        public int ReadTimeout
        {
            get { return TcpClient.GetStream().ReadTimeout; }
            set
            {
                TcpClient.GetStream().ReadTimeout = value;
                TcpClientReadTimeout = value;
            }
        }

        public int WriteTimeout
        {
            get { return TcpClient.GetStream().WriteTimeout; }
            set
            {
                TcpClient.GetStream().WriteTimeout = value;
                TcpClientWriteTimeout = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                string connectionString = null;
                try
                {

                    if (TcpClient != null && TcpClient.Client != null && TcpClient.Client.RemoteEndPoint != null)
                    {
                        return string.Format("tcp://{0}", ((TcpClient.Client.RemoteEndPoint as System.Net.IPEndPoint).ToString()));
                    }
                    else
                    {
                        return "Not Connected";
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(string.Format("Error getting connection string: {0}", ex.Message));
                }
                return connectionString;
            }
        }

        public int NumberOfBytesAvailable
        {
            get
            {
                return TcpClient.Available;
            }
        }

        #endregion

        #region IO Methods

        public void Write(ref byte[] buffer, int offset, int count)
        {
            var currentConnectAttempts = 0;

            try
            {
                var masterStopWatch = Stopwatch.StartNew();
                var startTime = DateTime.Now;
                var bufferLength = buffer.Length;
                var writeBuffer = new byte[bufferLength];
                Buffer.BlockCopy(buffer, 0, writeBuffer, 0, bufferLength);
                TcpClient.GetStream().Write(writeBuffer, offset, count);
                masterStopWatch.Stop();
                if (TracingEnabled)
                {
                    Trace.WriteLine(string.Format("Channel,{0},{1},{2},ChannelWrite,{3},{4},{5},TCPChannel", Name, startTime.ToString("O"), (1000.0 * (((double)masterStopWatch.ElapsedTicks) * (1.0 / ((double)Stopwatch.Frequency)))), 0, count, HexConverter.ToHexString(buffer)));
                }
            }
            catch (Exception exception)
            {
                if (exception.InnerException is SocketException)
                {
                    goto Connect;
                }
                else if (exception is InvalidOperationException)
                {
                    goto Connect;
                }
                else
                {
                    throw exception;
                }
                Connect:
                currentConnectAttempts++;
                try
                {
                    TcpClient.Dispose();

                    TcpClient = new TcpClient();

                    TcpClient.Connect(HostName, HostPort);

                    ReadTimeout = TcpClientReadTimeout;

                    WriteTimeout = TcpClientWriteTimeout;

                    throw new IOException("Recconected to host");
                }
                catch (SocketException)
                {
                    if (currentConnectAttempts > ConnectionAttempts)
                    {
                        throw exception;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(ConnectionRetryDelay);
                        goto Connect;
                    }
                }
            }

        }

        public int Read(ref byte[] buffer, int offset, int count, int sequence)
        {
            var currentConnectAttempts = 0;
            try
            {

                var masterStopWatch = Stopwatch.StartNew();
                var startTime = DateTime.Now;
                var startTicks = startTime.Ticks;
                var bytesRead = new List<byte>();
                var networkStream = TcpClient.GetStream();
                var timeoutStopWatch = Stopwatch.StartNew();

                while (ReadTimeout > timeoutStopWatch.ElapsedMilliseconds)
                {
                    var byteValue = -1;
                    try
                    {
                        byteValue = networkStream.ReadByte();
                        if (byteValue >= 0)
                        {
                            bytesRead.Add((byte)byteValue);
                        }
                        if (count == bytesRead.Count)
                        {
                            goto Finish;
                        }
                    }
                    catch (System.IO.IOException toex)
                    {
                        masterStopWatch.Stop();

                        if (bytesRead.Count > 0)
                        {
                            Buffer.BlockCopy(bytesRead.ToArray(), 0, buffer, offset, bytesRead.Count);
                        }

                        if (TracingEnabled)
                        {
                            Trace.WriteLine(string.Format("Channel,{0},{1},{2},ChannelRead,{3},{4},{5},TCPChannel", Name, startTime.ToString("O"), (1000.0 * (((double)masterStopWatch.ElapsedTicks) * (1.0 / ((double)Stopwatch.Frequency)))), sequence, bytesRead.Count, HexConverter.ToHexString(bytesRead.ToArray())));
                        }

                        throw new TimeoutException("Read Timeout", toex);
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
                    Trace.WriteLine(string.Format("Channel,{0},{1},{2},ChannelRead,{3},{4},{5},TCPChannel", Name, startTime.ToString("O"), (1000.0 * (((double)masterStopWatch.ElapsedTicks) * (1.0 / ((double)Stopwatch.Frequency)))), sequence, bytesRead.Count, HexConverter.ToHexString(bytesRead.ToArray())));
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
            catch (Exception exception)
            {
                if (exception.InnerException is SocketException)
                {
                    goto Connect;
                }
                else if (exception is InvalidOperationException)
                {
                    goto Connect;
                }
                else
                {
                    throw exception;
                }
                Connect:
                currentConnectAttempts++;
                try
                {
                    TcpClient.Dispose();

                    TcpClient = new TcpClient();

                    TcpClient.Connect(HostName, HostPort);

                    ReadTimeout = TcpClientReadTimeout;

                    WriteTimeout = TcpClientWriteTimeout;

                    throw new IOException("Recconected to host");

                }
                catch (SocketException)
                {
                    if (currentConnectAttempts > ConnectionAttempts)
                    {
                        throw exception;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(ConnectionRetryDelay);
                        goto Connect;
                    }
                }
            }
        }

        public void FlushBuffer()
        {
            if (TcpClient != null && TcpClient.Connected)
            {
                var networkStream = TcpClient.GetStream();
                while (networkStream.DataAvailable)
                {
                    // Read and discard any remaining data in the buffer
                    networkStream.ReadByte();
                }
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
                if (TcpClient != null)
                {
                    try
                    {
                        TcpClient.Close();
                    }
                    catch (Exception ex)
                    {
                        Trace.Write(string.Format("Error closing TCP Client: {0}", ex.Message));
                    }
                    ((IDisposable)TcpClient).Dispose();
                    tcpClient = null;
                }
            }
        }

        #endregion

    }
}
