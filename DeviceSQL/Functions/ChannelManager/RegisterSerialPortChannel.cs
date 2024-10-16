#region Imported Types

using DeviceSQL.IO.Channels;
using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ChannelManager
    {
        [SqlFunction]
        public static SqlBoolean ChannelManager_RegisterSerialPortChannel(SqlString channelName, SqlString portName, SqlInt32 baudRate, SqlByte dataBits, SqlByte parity, SqlByte stopBits, SqlInt32 readTimeout, SqlInt32 writeTimeout)
        {
            try
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

                    var serialPortChannel = new SerialPortChannel()
                    {
                        Name = channelName.Value,
                        ReadTimeout = readTimeout.Value,
                        WriteTimeout = writeTimeout.Value
                    };

                    serialPortChannel.SerialPort.PortName = portName.Value;
                    serialPortChannel.SerialPort.BaudRate = baudRate.Value;
                    serialPortChannel.SerialPort.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), parity.Value.ToString());
                    serialPortChannel.SerialPort.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), stopBits.Value.ToString());
                    serialPortChannel.SerialPort.Handshake = System.IO.Ports.Handshake.None;
                    serialPortChannel.SerialPort.DtrEnable = false;
                    serialPortChannel.SerialPort.RtsEnable = false;

                    serialPortChannel.SerialPort.Open();

                    ServiceRegistry.RegisterChannel(serialPortChannel);

                    return new SqlBoolean(true);

                }
                else
                {
                    throw new ArgumentException("Channel name is already registered");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error registering channel: {0}", ex.Message));
            }
            return new SqlBoolean(false);
        }
    }
}
