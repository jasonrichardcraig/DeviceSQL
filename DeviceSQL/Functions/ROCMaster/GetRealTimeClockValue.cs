#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlDateTime ROCMaster_GetRealTimeClockValue(SqlString deviceName)
        {
            var deviceNameValue = deviceName.Value;
            return new SqlDateTime((DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetRealTimeClockValue(null, null, null, null));
        }
    }
}
