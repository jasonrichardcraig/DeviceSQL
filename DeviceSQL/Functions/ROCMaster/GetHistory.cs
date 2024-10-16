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
        public static Types.RocMaster.RocMaster_HistoryRecordArray RocMaster_GetHistory(SqlString deviceName, byte historicalRamArea, byte historyPointNumber, byte count, int startIndex)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var historyRecords = (device as Device.Roc.RocMaster).GetHistory(null, null, null, null, historicalRamArea, historyPointNumber, count, Convert.ToUInt16(startIndex));
            return new Types.RocMaster.RocMaster_HistoryRecordArray() { historyRecords = historyRecords.Select(h => new Types.RocMaster.RocMaster_HistoryRecord() { HistorySegment = h.HistorySegment, HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
