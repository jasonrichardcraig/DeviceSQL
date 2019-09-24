using DeviceSQL.IO.Channels;
using DeviceSQL.Service.IOC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DeviceSQL.Service.API
{
    public class ChannelController : ApiController
    {

        public ConcurrentDictionary<string, IChannel> ChannelConcurrentDictionary
        {
            get
            {
                return SimpleIOC.Default.GetInstance<ConcurrentDictionary<string, IChannel>>();
            }
        }

        public EventLog EventLog
        {
            get
            {
                return SimpleIOC.Default.GetInstance<EventLog>();
            }
        }

        [HttpGet]
        [Route("api/Channels/GetChannels")]
        public IEnumerable<IChannel> GetChannels()
        {
            foreach (var channelKeyValuePair in ChannelConcurrentDictionary)
            {
                yield return channelKeyValuePair.Value;
            }
        }

        [HttpGet]
        [Route("api/Channels/RemoveChannel")]
        public void RemoveChannel(string channelName)
        {
            if (ChannelConcurrentDictionary.TryRemove(channelName, out IChannel channel))
            {
                try
                {
                    channel.Dispose();
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry($"Error disposing channel: {ex.Message}", EventLogEntryType.Error);
                    throw new Exception($"Error disposing channel: {ex.Message}");
                }
            }
            else
            {
                EventLog.WriteEntry("Channel does not exist", EventLogEntryType.Error);
                throw new Exception("Channel does not exist");
            }
        }

        [HttpGet]
        [Route("api/Channels/AddTcpChannel")]
        public void AddTcpChannel(string channelName, string hostName, int hostPort, int connectionAttempts, int connectionRetryDelay, int readTimeout, int writeTimeout)
        {
            int currentConnectAttempts = 0;
            TcpChannel tcpChannel;

           

            try
            {
                tcpChannel = new TcpChannel()
                {
                    HostName = hostName,
                    HostPort = hostPort,
                    ConnectionAttempts = connectionAttempts,
                    ConnectionRetryDelay = connectionRetryDelay,
                    TcpClientReadTimeout = readTimeout,
                    TcpClientWriteTimeout = writeTimeout
            };

                if (ChannelConcurrentDictionary.TryAdd(channelName, tcpChannel))
                {

                Connect:
                    currentConnectAttempts++;

                    try
                    {
                        tcpChannel.TcpClient = new TcpClient();
                        tcpChannel.TcpClient.Connect(tcpChannel.HostName, tcpChannel.HostPort);
                        tcpChannel.ReadTimeout = tcpChannel.TcpClientReadTimeout;
                        tcpChannel.WriteTimeout = tcpChannel.TcpClientWriteTimeout;
                    }
                    catch (SocketException)
                    {
                        if (currentConnectAttempts > tcpChannel.ConnectionAttempts)
                        {
                            throw new Exception("Unable to connect to host");
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(tcpChannel.ConnectionRetryDelay);
                            goto Connect;
                        }
                    }
                }
                else
                {
                    EventLog.WriteEntry("TCP channel already exists", EventLogEntryType.Error);
                    throw new Exception("TCP channel already exists");
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry($"Error creating TCP channel: {ex.Message}", EventLogEntryType.Error);
                throw new Exception($"Error creating TCP channel: {ex.Message}");
            }
        }
    }
}
