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
        public static AlarmRecordArray GetAlarms(SqlString deviceName, byte count, int startIndex)
        {
            //var deviceNameValue = deviceName.Value;
            //var alarmsRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetAlarms(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return AlarmRecordArray.Null; // new Types.ROCMaster.ROCMaster_AlarmRecordArray() { alarmRecords = alarmsRecords.Select(a => new Types.ROCMaster.ROCMaster_AlarmRecord() { data = a.data, Index = a.Index }).ToList() };
        }
    }
}
