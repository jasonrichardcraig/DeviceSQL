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
        public static Types.RocMaster.RocMaster_EventRecordArray RocMaster_GetEvents(SqlString deviceName, byte count, int startIndex)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var eventRecords = (device as Device.Roc.RocMaster).GetEvents(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.RocMaster.RocMaster_EventRecordArray() { eventRecords = eventRecords.Select(e => new Types.RocMaster.RocMaster_EventRecord() { Data = e.data, Index = e.Index }).ToList() };
        }
    }
}
