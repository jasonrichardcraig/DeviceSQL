#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static int ROCMaster_GetCurrentROCPlusAlarmIndex(SqlString deviceName)
        {
            var deviceNameValue = deviceName.Value;
            return (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetCurrentROCPlusAlarmIndex(null, null, null, null);
        }
    }
}
