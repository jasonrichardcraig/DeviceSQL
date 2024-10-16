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
        public static Types.RocMaster.RocMaster_AlarmRecordArray RocMaster_GetAlarms(SqlString deviceName, byte count, int startIndex)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var alarmsRecords = (device as Device.Roc.RocMaster).GetAlarms(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.RocMaster.RocMaster_AlarmRecordArray() { alarmRecords = alarmsRecords.Select(a => new Types.RocMaster.RocMaster_AlarmRecord() { data = a.data, Index = a.Index }).ToList() };
        }
    }
}
