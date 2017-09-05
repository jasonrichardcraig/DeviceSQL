#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static Types.ROCMaster.ROCMaster_ROCPlusAlarmRecordArray ROCMaster_GetROCPlusAlarms(SqlString deviceName, byte count, int startIndex)
        {
            var deviceNameValue = deviceName.Value;
            var rocPlusAlarmRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusAlarms(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.ROCMaster.ROCMaster_ROCPlusAlarmRecordArray() { rocPlusAlarmRecords = rocPlusAlarmRecords.Select(a => new Types.ROCMaster.ROCMaster_ROCPlusAlarmRecord() { Data = a.data, Index = a.Index }).ToList() };
        }
    }
}
