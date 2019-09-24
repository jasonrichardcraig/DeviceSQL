#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlDateTime GetRealTimeClockValueWithCentury(SqlString deviceName, SqlInt32 century)
        {
            //var deviceNameValue = deviceName.Value;
            return SqlDateTime.Null; // new SqlDateTime((DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetRealTimeClockValue(null, null, null, null, Convert.ToUInt16(century.Value)));
        }
    }
}
