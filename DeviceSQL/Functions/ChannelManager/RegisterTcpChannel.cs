#region Imported Types

using DeviceSQL.IO.Channels;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;

#endregion
namespace DeviceSQL.Functions
{
    public partial class ChannelManager
    {

        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlBoolean ChannelManager_RegisterTcpChannel(SqlString channelName, SqlString hostName, SqlInt32 hostPort, SqlInt32 connectAttempts, SqlInt32 readTimeout, SqlInt32 writeTimeout)
        {

            var channelNameValue = channelName.Value;
            var channels = DeviceSQL.Watchdog.Worker.Channels;

            if (channelNameValue.Count(c =>
            {
                switch (c)
                {
                    case '|':
                    case ';':
                    case ',':
                        return true;
                    default:
                        return false;
                }
            }) > 0)
            {
                throw new ArgumentException("Invalid channel name");
            }

            if (channels.Where(channel => channel.Name == channelNameValue).Count() == 0)
            {
                var tcpChannel = new TcpChannel()
                {
                    Name = channelNameValue,
                    HostName = hostName.Value,
                    HostPort = hostPort.Value,
                    ConnectionAttempts = connectAttempts.Value
                };

                var currentConnectAttempts = 0;

                Connect:
                currentConnectAttempts++;

                try
                {
                    
                    tcpChannel.TcpClient.Connect(tcpChannel.HostName, tcpChannel.HostPort);

                }
                catch(SocketException socketException)
                {
                    if (currentConnectAttempts > tcpChannel.ConnectionAttempts)
                    {
                        throw socketException;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        goto Connect;
                    }
                }
                
                tcpChannel.ReadTimeout = readTimeout.Value;
                tcpChannel.WriteTimeout = writeTimeout.Value;

                channels.Add(tcpChannel);

                return new SqlBoolean(true);

            }
            else
            {
                throw new ArgumentException("Channel name is already registered");
            }
        }
    }

}
