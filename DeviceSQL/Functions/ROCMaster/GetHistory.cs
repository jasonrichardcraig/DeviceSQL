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
        public static Types.ROCMaster.ROCMaster_HistoryRecordArray ROCMaster_GetHistory(SqlString deviceName, byte historicalRamArea, byte historyPointNumber, byte count, int startIndex)
        {
            var deviceNameValue = deviceName.Value;
            var historyRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetHistory(null, null, null, null, historicalRamArea, historyPointNumber, count, Convert.ToUInt16(startIndex));
            return new Types.ROCMaster.ROCMaster_HistoryRecordArray() { historyRecords = historyRecords.Select(h => new Types.ROCMaster.ROCMaster_HistoryRecord() { HistorySegment = h.HistorySegment, HistoryPointNumber = h.HistoryPointNumber, Index = h.Index, Value = h.Value }).ToList() };
        }
    }
}
