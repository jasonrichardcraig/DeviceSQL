using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;
using DeviceSQL.Device.Modbus;

namespace DeviceSQL.Functions
{
    public partial class DeviceManager
    {
        [SqlFunction]
        public static SqlBoolean RegisterModbusMaster(SqlString channelName, SqlString deviceName, SqlInt32 unitId, SqlBoolean useExtendedAddressing, SqlInt32 numberOfRetries, SqlInt32 waitToRetry, SqlInt32 requestWriteDelay, SqlInt32 responseReadDelay)
        {
            try
            {
                var deviceNameValue = deviceName.Value;
                var devices = DeviceSQL.Watchdog.Worker.Devices;
                if (devices.Where(device => device.Name == deviceNameValue).Count() == 0)
                {
                    var channelNameValue = channelName.Value;
                    var modbusMaster = new Device.Modbus.ModbusMaster(DeviceSQL.Watchdog.Worker.Channels.First(channel => channel.Name == channelNameValue))
                    {
                        Name = deviceNameValue,
                        UnitId = Convert.ToUInt16(unitId.Value),
                        UseExtendedAddressing = useExtendedAddressing.Value
                    };

                    modbusMaster.Transport.NumberOfRetries = numberOfRetries.Value;
                    modbusMaster.Transport.WaitToRetryMilliseconds = waitToRetry.Value;
                    modbusMaster.Transport.RequestWriteDelayMilliseconds = requestWriteDelay.Value;
                    modbusMaster.Transport.ResponseReadDelayMilliseconds = responseReadDelay.Value;

                    devices.Add(modbusMaster);

                    return new SqlBoolean(true);

                }
                else
                {
                    throw new ArgumentException("Device name is already registered");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error registering device: {0}", ex.Message));
            }
            return new SqlBoolean(false);
        }
    }
}
