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
        public static Types.RocMaster.RocMaster_RocPlusEventRecordArray RocMaster_GetRocPlusEvents(SqlString deviceName, byte count, int startIndex)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var rocPlusEventRecords = (device as Device.Roc.RocMaster).GetRocPlusEvents(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.RocMaster.RocMaster_RocPlusEventRecordArray() { rocPlusEventRecords = rocPlusEventRecords.Select(e => new Types.RocMaster.RocMaster_RocPlusEventRecord() { Data = e.data, Index = e.Index }).ToList() };
        }
    }
}
