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
        public static EventRecordArray GetEvents(SqlString deviceName, byte count, int startIndex)
        {
            //var deviceNameValue = deviceName.Value;
            //var eventRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetEvents(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return EventRecordArray.Null; // new Types.ROCMaster.ROCMaster_EventRecordArray() { eventRecords = eventRecords.Select(e => new Types.ROCMaster.ROCMaster_EventRecord() { Data = e.data, Index = e.Index }).ToList() };
        }
    }
}
