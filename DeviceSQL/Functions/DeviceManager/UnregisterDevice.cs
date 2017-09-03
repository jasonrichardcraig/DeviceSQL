using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace DeviceSQL.DeviceManager
{
    public partial class Functions
    {
        [SqlFunction]
        public static SqlBoolean DeviceManager_UnregisterDevice(SqlString deviceName)
        {
            try
            {
                var deviceNameValue = deviceName.Value;
                var devices = Watchdog.Worker.Devices;
                var devicesToRemove = devices.Where(channel => channel.Name == deviceNameValue).ToList();
                devicesToRemove.ForEach((device) =>
                {
                    try
                    {
                        devices.TryTake(out device);
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(string.Format("Error unregistering device: {0}", ex.Message));
                    }
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Error unregistering device: {0}", ex.Message));
            }
            return new SqlBoolean(true);
        }
    }
}
