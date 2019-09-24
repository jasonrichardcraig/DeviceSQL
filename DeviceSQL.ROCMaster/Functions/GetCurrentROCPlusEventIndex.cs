#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static int GetCurrentROCPlusEventIndex(SqlString deviceName)
        {
            //var deviceNameValue = deviceName.Value;
            return 0;// (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetCurrentROCPlusEventIndex(null, null, null, null);
        }
    }
}
