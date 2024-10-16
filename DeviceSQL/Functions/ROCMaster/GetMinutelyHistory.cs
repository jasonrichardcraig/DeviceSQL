#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static Types.RocMaster.RocMaster_HistoryRecordArray RocMaster_GetMinutelyHistory(SqlString deviceName, byte historyPointNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var historyRecords = (device as Device.Roc.RocMaster).GetMinutelyHistory(null, null, null, null, historyPointNumber);
            return new Types.RocMaster.RocMaster_HistoryRecordArray() { historyRecords = historyRecords.Select(h => new Types.RocMaster.RocMaster_HistoryRecord() { HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
