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
        public static ROCPlusHistoryRecordArray GetROCPlusHistory(SqlString deviceName, byte historySegment, int historyIndex, byte historyType, byte startingHistoryPoint, byte numberOfHistoryPoints, byte numberOfTimePeriods)
        {
            //var deviceNameValue = deviceName.Value;
            //var rocPlusHistoryRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusHistory(null, null, null, null, historySegment, Convert.ToUInt16(historyIndex), historyType, startingHistoryPoint, numberOfHistoryPoints, numberOfTimePeriods);
            return ROCPlusHistoryRecordArray.Null; // new Types.ROCMaster.ROCMaster_ROCPlusHistoryRecordArray() { rocPlusHistoryRecords = rocPlusHistoryRecords.Select(h => new Types.ROCMaster.ROCMaster_ROCPlusHistoryRecord() { HistorySegment = h.HistorySegment, HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
