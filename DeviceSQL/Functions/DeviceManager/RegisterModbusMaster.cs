#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class DeviceManager
    {
        [SqlFunction]
        public static SqlBoolean DeviceManager_RegisterMODBUSMaster(SqlString channelName, SqlString deviceName, SqlInt32 unitId, SqlBoolean useExtendedAddressing, SqlInt32 numberOfRetries, SqlInt32 waitToRetry, SqlInt32 requestWriteDelay, SqlInt32 responseReadDelay)
        {
            try
            {
                var deviceNameValue = deviceName.Value;
                var devices = DeviceSQL.Watchdog.Worker.Devices;
                if (devices.Where(device => device.Name == deviceNameValue).Count() == 0)
                {
                    var channelNameValue = channelName.Value;
                    var MODBUSMaster = new Device.MODBUS.MODBUSMaster(DeviceSQL.Watchdog.Worker.Channels.First(channel => channel.Name == channelNameValue))
                    {
                        Name = deviceNameValue,
                        UnitId = Convert.ToUInt16(unitId.Value),
                        UseExtendedAddressing = useExtendedAddressing.Value
                    };

                    MODBUSMaster.Transport.NumberOfRetries = numberOfRetries.Value;
                    MODBUSMaster.Transport.WaitToRetryMilliseconds = waitToRetry.Value;
                    MODBUSMaster.Transport.RequestWriteDelayMilliseconds = requestWriteDelay.Value;
                    MODBUSMaster.Transport.ResponseReadDelayMilliseconds = responseReadDelay.Value;

                    devices.Add(MODBUSMaster);

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
