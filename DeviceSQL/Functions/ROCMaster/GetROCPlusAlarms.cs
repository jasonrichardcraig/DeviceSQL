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
        public static Types.RocMaster.RocMaster_RocPlusAlarmRecordArray RocMaster_GetRocPlusAlarms(SqlString deviceName, byte count, int startIndex)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var rocPlusAlarmRecords = (device as Device.Roc.RocMaster).GetRocPlusAlarms(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.RocMaster.RocMaster_RocPlusAlarmRecordArray() { rocPlusAlarmRecords = rocPlusAlarmRecords.Select(a => new Types.RocMaster.RocMaster_RocPlusAlarmRecord() { Data = a.data, Index = a.Index }).ToList() };
        }
    }
}
