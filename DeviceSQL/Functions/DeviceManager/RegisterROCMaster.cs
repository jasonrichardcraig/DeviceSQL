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
        public static SqlBoolean DeviceManager_RegisterRocMaster(SqlString channelName, SqlString deviceName, SqlByte deviceAddress, SqlByte deviceGroup, SqlByte hostAddress, SqlByte hostGroup, SqlInt32 numberOfRetries, SqlInt32 waitToRetry, SqlInt32 requestWriteDelay, SqlInt32 responseReadDelay)
        {
            try
            {
                if (ServiceRegistry.GetChannel(deviceName.Value) == null)
                {
                    var rocMaster = new Device.Roc.RocMaster(ServiceRegistry.GetChannel(channelName.Value))
                    {
                        Name = deviceName.Value,
                        DeviceAddress = deviceAddress.Value,
                        DeviceGroup = deviceGroup.Value,
                        HostAddress = hostAddress.Value,
                        HostGroup = hostGroup.Value
                    };

                    rocMaster.Transport.NumberOfRetries = numberOfRetries.Value;
                    rocMaster.Transport.WaitToRetryMilliseconds = waitToRetry.Value;
                    rocMaster.Transport.RequestWriteDelayMilliseconds = requestWriteDelay.Value;
                    rocMaster.Transport.ResponseReadDelayMilliseconds = responseReadDelay.Value;

                    ServiceRegistry.RegisterDevice(rocMaster);

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
