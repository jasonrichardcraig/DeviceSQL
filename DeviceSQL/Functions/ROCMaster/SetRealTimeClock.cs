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
        public static SqlBoolean ROCMaster_SetRealTimeClock(SqlString deviceName, SqlDateTime dateTime)
        {
            var deviceNameValue = deviceName.Value;
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).SetRealTimeClock(null, null, null, null, dateTime.Value);
            return true;
        }
    }
}
