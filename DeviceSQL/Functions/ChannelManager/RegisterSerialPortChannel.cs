using DeviceSQL.IO.Channels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace DeviceSQL.ChannelManager
{
    partial class Functions
    {
        [SqlFunction]
        public static SqlBoolean RegisterSerialPortChannel(SqlString channelName, SqlString portName, SqlInt32 baudRate, SqlByte dataBits, SqlByte parity, SqlByte stopBits, SqlInt32 readTimeout, SqlInt32 writeTimeout)
        {
            try
            {
                var channelNameValue = channelName.Value;
                var channels = Watchdog.Worker.Channels;
                if (channels.Where(serialPortChannel => serialPortChannel.Name == channelNameValue).Count() == 0)
                {
                    var serialPortChannel = new SerialPortChannel()
                    {
                        Name = channelNameValue,
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

                    channels.Add(serialPortChannel);

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
