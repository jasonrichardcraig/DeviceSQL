#region Imported Types

using DeviceSQL.SQLTypes.ROC;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static AuditLogRecordArray GetAuditLogRecords(SqlString deviceName, byte count, int startIndex)
        {
            //var deviceNameValue = deviceName.Value;
            //var auditLogRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetAuditLogRecords(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return AuditLogRecordArray.Null; // new Types.ROCMaster.ROCMaster_AuditLogRecordArray() { auditLogRecords = auditLogRecords.Select(e => new Types.ROCMaster.ROCMaster_AuditLogRecord() { Data = e.data, Index = e.Index }).ToList() };
        }
    }
}
