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
        public static ArchiveInformation GetArchiveInfo(SqlString deviceName)
        {
            //var deviceNameValue = deviceName.Value;
            //var archiveInfo = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetArchiveInfo(null, null, null, null);
            return ArchiveInformation.Null; // new Types.ROCMaster.ROCMaster_ArchiveInformation() { Data = archiveInfo.data };
        }
    }
}
