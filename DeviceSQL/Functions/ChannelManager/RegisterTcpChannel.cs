#region Imported Types

using DeviceSQL.IO.Channels;
using DeviceSQL.Registries;
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
        public static SqlBoolean ChannelManager_RegisterTcpChannel(SqlString channelName, SqlString hostName, SqlInt32 hostPort, SqlInt32 connectAttempts, SqlInt32 connectionRetryDelay, SqlInt32 readTimeout, SqlInt32 writeTimeout)
        {
            if (ServiceRegistry.GetChannel(channelName.Value) == null)
            {

                if (channelName.Value.Count(c =>
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

                var tcpChannel = new TcpChannel()
                {
                    Name = channelName.Value,
                    HostName = hostName.Value,
                    HostPort = hostPort.Value,
                    ConnectionAttempts = connectAttempts.Value,
                    ConnectionRetryDelay = connectionRetryDelay.Value
                };

                var currentConnectAttempts = 0;

            Connect:
                currentConnectAttempts++;

                try
                {
                    tcpChannel.TcpClient.Connect(tcpChannel.HostName, tcpChannel.HostPort);
                }
                catch (SocketException socketException)
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

                ServiceRegistry.RegisterChannel(tcpChannel);

                return new SqlBoolean(true);

            }
            else
            {
                throw new ArgumentException("Channel name is already registered");
            }
        }
    }

}
