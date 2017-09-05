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
        public static Types.ROCMaster.ROCMaster_AlarmRecordArray ROCMaster_GetAlarms(SqlString deviceName, byte count, int startIndex)
        {
            var deviceNameValue = deviceName.Value;
            var alarmsRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetAlarms(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return new Types.ROCMaster.ROCMaster_AlarmRecordArray() { alarmRecords = alarmsRecords.Select(a => new Types.ROCMaster.ROCMaster_AlarmRecord() { data = a.data, Index = a.Index }).ToList() };
        }
    }
}
