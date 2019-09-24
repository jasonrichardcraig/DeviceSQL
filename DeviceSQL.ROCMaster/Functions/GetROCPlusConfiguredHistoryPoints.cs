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
        public static ROCPlusHistoryPointArray GetROCPlusConfiguredHistoryPoints(SqlString deviceName, byte historySegment)
        {
            //var deviceNameValue = deviceName.Value;
            //var configuredHistoryPoints = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusConfiguredHistoryPoints(null, null, null, null, historySegment);
            return ROCPlusHistoryPointArray.Null; // new Types.ROCMaster.ROCMaster_ROCPlusHistoryPointArray() { historyPoints = configuredHistoryPoints };
        }
    }
}
