using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;
using DeviceSQL.Device.ROC.Data;

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static Types.ROCMaster.ROCMaster_ROCPlusHistoryRecordArray ROCMaster_GetROCPlusHistoryByHistoryPointArray(SqlString deviceName, byte historySegment, int historyIndex, byte historyType, byte startingHistoryPoint, bool requestTimeStamps, byte numberOfHistoryPoints, Types.ROCMaster.ROCMaster_ROCPlusHistoryPointArray requestedHistoryPoints, byte numberOfTimePeriods)
        {
            var deviceNameValue = deviceName.Value;
            var rocPlusHistoryRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusHistory(null, null, null, null, historySegment, Convert.ToUInt16(historyIndex), historyType, requestTimeStamps, requestedHistoryPoints.historyPoints, numberOfTimePeriods);
            return new Types.ROCMaster.ROCMaster_ROCPlusHistoryRecordArray() { rocPlusHistoryRecords = rocPlusHistoryRecords.Select(h => new Types.ROCMaster.ROCMaster_ROCPlusHistoryRecord() { HistorySegment = h.HistorySegment, HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
