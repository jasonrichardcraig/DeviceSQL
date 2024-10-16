#region Imported Types

using DeviceSQL.Registries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace DeviceSQL.IO.Channels
{
    public class TcpMuxChannel : TcpChannel, IMuxChannel
    {

        #region Fields

        private readonly object lockObject = new object();
        private int listenerPort = 0;
        private volatile int numberOfTcpClients = 0;
        private volatile bool stopRequested;
        private volatile bool hasStopped;
        private string sourceChannelName = "";
        private int requestDelay = 0;
        private int responseDelay = 0;
        private int responseTimeout = 3000;
        private int maximumNumberOfTcpClients = 5;
        private TcpListener tcpListener;
        private readonly List<TcpClient> tcpClients = new List<TcpClient>();

        #endregion

        #region Properties

        public IChannel SourceChannel
        {
            get
            {
                return ServiceRegistry.GetChannel(SourceChannelName);
            }
        }

        public string SourceChannelName
        {
            get
            {
                return sourceChannelName;
            }
            set
            {
                sourceChannelName = value;
            }
        }
        public int RequestDelay
        {
            get
            {
                return requestDelay;
            }
            set
            {
                requestDelay = value;
            }
        }

        public int ResponseDelay
        {
            get
            {
                return responseDelay;
            }
            set
            {
                responseDelay = value;
            }
        }

        public int ResponseTimeout
        {
            get
            {
                return responseTimeout;
            }
            set
            {
                responseTimeout = value;
            }
        }

        public int ListenerPort
        {
            get
            {
                return listenerPort;
            }
            set
            {
                listenerPort = value;
            }
        }

        public int MaximumNumberOfTcpClients
        {
            get
            {
                return maximumNumberOfTcpClients;
            }
            set
            {
                maximumNumberOfTcpClients = value;
            }
        }

        public IEnumerable<TcpClient> TcpClients
        {
            get
            {
                return tcpClients;
            }
        }

        #endregion

        #region Mux Methods

        public async void Run()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    tcpListener = new TcpListener(System.Net.IPAddress.Any, ListenerPort);
                    tcpListener.Start(MaximumNumberOfTcpClients);
                    stopRequested = false;
                    hasStopped = false;
                    while (!stopRequested)
                    {
                        try
                        {
                            if (MaximumNumberOfTcpClients > numberOfTcpClients)
                            {
                                var tcpClient = tcpListener.AcceptTcpClient();
                                var networkStream = tcpClient.GetStream();
                                var readBuffer = new byte[1500];

                                Interlocked.Increment(ref numberOfTcpClients);
                                tcpClient.Client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.KeepAlive, true);
                                tcpClients.Add(tcpClient);
                                networkStream.BeginRead(readBuffer, 0, 0, new AsyncCallback(TcpClientBeginReadCallback), tcpClient);
                            }

                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError($"Error in Mux startup: {ex.Message}");
                            Thread.Sleep(1000);
                        }
                    }

                    foreach (var tcpClient in tcpClients)
                    {
                        try
                        {
                            tcpClient.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError($"Error disposing TCP Client: {ex.Message}");
                        }
                    }
                    hasStopped = true;
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"Error starting Mux: {ex.Message}");
                }
            });
        }

        private void TcpClientBeginReadCallback(IAsyncResult asyncResult)
        {
            var tcpClient = asyncResult.AsyncState as TcpClient;

            try
            {
                if (asyncResult.IsCompleted)
                {
                    if (tcpClient != null)
                    {
                        try
                        {
                            var startTime = DateTime.Now;
                            var sourceChannelLockObject = SourceChannel.LockObject;
                            lock (sourceChannelLockObject)
                            {
                                lock (lockObject)
                                {
                                    var readBuffer = new byte[1500];
                                    var masterStopWatch = new Stopwatch();
                                    var requestBytes = new byte[] { };
                                    var networkStream = tcpClient.GetStream();

                                    this.tcpClient = tcpClient;

                                    TimedThreadBlocker.Wait(requestDelay);

                                    requestBytes = new byte[tcpClient.Available];

                                    if (requestBytes.Length > 0)
                                    {
                                        Read(ref requestBytes, 0, requestBytes.Length, 0);
                                        SourceChannel.Write(ref requestBytes, 0, requestBytes.Length);
                                        TimedThreadBlocker.Wait(responseDelay);
                                        masterStopWatch.Start();

                                        var sequence = 0;
                                        while (SourceChannel.NumberOfBytesAvailable > 0 && responseTimeout > masterStopWatch.ElapsedMilliseconds)
                                        {
                                            var responseBytes = new byte[SourceChannel.NumberOfBytesAvailable];

                                            SourceChannel.Read(ref responseBytes, 0, responseBytes.Length, sequence++);
                                            Write(ref responseBytes, 0, responseBytes.Length);
                                        }

                                        masterStopWatch.Stop();

                                    }

                                    networkStream.BeginRead(readBuffer, 0, 0, new AsyncCallback(TcpClientBeginReadCallback), tcpClient);

                                    return;

                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError($"Error reading newtwork stream: {ex.Message}");
                        }
                    }
                }

                try
                {
                    tcpClient.Dispose();
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"Error disposing TCP Client: {ex.Message}");
                }

                try
                {
                    tcpClients.Remove(tcpClient);
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"Error removing TCP Client from list: {ex.Message}");
                }

                Interlocked.Decrement(ref numberOfTcpClients);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error disposing TCP Client: {ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                try
                {
                    tcpListener.Stop();
                }
                catch (Exception ex)
                {
                    Trace.TraceError($"Error stopping TCP Listener: {ex.Message}");
                }

                stopRequested = true;

                while (!hasStopped)
                {
                    Thread.Sleep(0);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error stopping Mux: {ex.Message}");
            }
        }

        #endregion

    }
}
