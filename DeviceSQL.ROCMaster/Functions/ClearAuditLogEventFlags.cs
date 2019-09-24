#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static int ClearAuditLogEventFlags(SqlString deviceName, byte numberOfAuditLogRecordsToClear, int startingAuditLogPointer)
        {
            //var deviceNameValue = deviceName.Value;
            return 0; // (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ClearAuditLogEventFlags(null, null, null, null, numberOfAuditLogRecordsToClear, Convert.ToUInt16(startingAuditLogPointer));
        }
    }
}
