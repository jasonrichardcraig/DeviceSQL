#region Imported Types

using DeviceSQL.SQLTypes.ROC;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static HistoryRecordArray GetMinutelyHistory(SqlString deviceName, byte historyPointNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var historyRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetMinutelyHistory(null, null, null, null, historyPointNumber);
            return HistoryRecordArray.Null; // new Types.ROCMaster.ROCMaster_HistoryRecordArray() { historyRecords = historyRecords.Select(h => new Types.ROCMaster.ROCMaster_HistoryRecord() { HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
