#region Imported Types

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
        }

        public int ReadTimeout
        {
            get { return TcpClient.GetStream().ReadTimeout; }
            set { TcpClient.GetStream().ReadTimeout = value; }
        }

        public int WriteTimeout
        {
            get { return TcpClient.GetStream().WriteTimeout; }
            set { TcpClient.GetStream().WriteTimeout = value; }
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
            catch (SocketException socketException)
            {
            Connect:
                currentConnectAttempts++;
                try
                {
                    TcpClient.Connect(HostName, HostPort);
                    throw new IOException("Recconected to host");
                }
                catch (SocketException)
                {
                    if (currentConnectAttempts > ConnectionAttempts)
                    {
                        throw socketException;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        goto Connect;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            catch(SocketException socketException)
            {
            Connect:
                currentConnectAttempts++;
                try
                {
                    TcpClient.Connect(HostName, HostPort);
                    throw new IOException("Recconected to host");
                }
                catch(SocketException)
                {
                    if(currentConnectAttempts > ConnectionAttempts)
                    {
                        throw socketException;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        goto Connect;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
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
