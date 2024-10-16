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
        public static Types.RocMaster.RocMaster_RocPlusHistoryRecordArray RocMaster_GetRocPlusHistory(SqlString deviceName, byte historySegment, int historyIndex, byte historyType, byte startingHistoryPoint, byte numberOfHistoryPoints, byte numberOfTimePeriods)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var rocPlusHistoryRecords = (device as Device.Roc.RocMaster).GetRocPlusHistory(null, null, null, null, historySegment, Convert.ToUInt16(historyIndex), historyType, startingHistoryPoint, numberOfHistoryPoints, numberOfTimePeriods);
            return new Types.RocMaster.RocMaster_RocPlusHistoryRecordArray() { rocPlusHistoryRecords = rocPlusHistoryRecords.Select(h => new Types.RocMaster.RocMaster_RocPlusHistoryRecord() { HistorySegment = h.HistorySegment, HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
