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
        public static HistoryRecordArray GetHistory(SqlString deviceName, byte historicalRamArea, byte historyPointNumber, byte count, int startIndex)
        {
            //var deviceNameValue = deviceName.Value;
            // var historyRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetHistory(null, null, null, null, historicalRamArea, historyPointNumber, count, Convert.ToUInt16(startIndex));
            return HistoryRecordArray.Null; // new Types.ROCMaster.ROCMaster_HistoryRecordArray() { historyRecords = historyRecords.Select(h => new Types.ROCMaster.ROCMaster_HistoryRecord() { HistorySegment = h.HistorySegment, HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
