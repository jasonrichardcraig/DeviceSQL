#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static Types.RocMaster.RocMaster_AuditLogRecordArray RocMaster_GetAuditLogRecords(SqlString deviceName, byte count, int startIndex)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var auditLogRecords = (device as Device.Roc.RocMaster).GetAuditLogRecords(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.RocMaster.RocMaster_AuditLogRecordArray() { auditLogRecords = auditLogRecords.Select(e => new Types.RocMaster.RocMaster_AuditLogRecord() { Data = e.data, Index = e.Index }).ToList() };
        }
    }
}
