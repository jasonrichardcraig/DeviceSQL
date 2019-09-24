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
        public static ROCPlusEventRecordArray GetROCPlusEvents(SqlString deviceName, byte count, int startIndex)
        {
            //var deviceNameValue = deviceName.Value;
            //var rocPlusEventRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusEvents(null, null, null, null, count, Convert.ToUInt16(startIndex));
            return ROCPlusEventRecordArray.Null; // new Types.ROCMaster.ROCMaster_ROCPlusEventRecordArray() { rocPlusEventRecords = rocPlusEventRecords.Select(e => new Types.ROCMaster.ROCMaster_ROCPlusEventRecord() { Data = e.data, Index = e.Index }).ToList() };
        }
    }
}
