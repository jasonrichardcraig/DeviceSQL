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
        public static ROCPlusAlarmRecordArray GetROCPlusAlarms(SqlString deviceName, byte count, int startIndex)
        {
            //var deviceNameValue = deviceName.Value;
            //var rocPlusAlarmRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusAlarms(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return ROCPlusAlarmRecordArray.Null; // new Types.ROCMaster.ROCMaster_ROCPlusAlarmRecordArray() { rocPlusAlarmRecords = rocPlusAlarmRecords.Select(a => new Types.ROCMaster.ROCMaster_ROCPlusAlarmRecord() { Data = a.data, Index = a.Index }).ToList() };
        }
    }
}
