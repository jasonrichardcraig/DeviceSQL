#region Imported Types

using DeviceSQL.Registries;
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
        public static SqlBoolean DeviceManager_RegisterModbusMaster(SqlString channelName, SqlString deviceName, SqlBoolean useMbapHeaders, SqlBoolean useExtendedAddressing, SqlInt32 unitId, SqlInt32 numberOfRetries, SqlInt32 waitToRetry, SqlInt32 requestWriteDelay, SqlInt32 responseReadDelay)
        {
            try
            {
                if (ServiceRegistry.GetDevice(deviceName.Value) == null)
                {
                    var ModbusMaster = new Device.Modbus.ModbusMaster(ServiceRegistry.GetChannel(channelName.Value), useMbapHeaders.Value)
                    {
                        Name = deviceName.Value,
                        UseExtendedAddressing = useExtendedAddressing.Value,
                        UnitId = Convert.ToUInt16(unitId.Value)
                    };

                    ModbusMaster.Transport.NumberOfRetries = numberOfRetries.Value;
                    ModbusMaster.Transport.WaitToRetryMilliseconds = waitToRetry.Value;
                    ModbusMaster.Transport.RequestWriteDelayMilliseconds = requestWriteDelay.Value;
                    ModbusMaster.Transport.ResponseReadDelayMilliseconds = responseReadDelay.Value;

                    ServiceRegistry.RegisterDevice(ModbusMaster);

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
