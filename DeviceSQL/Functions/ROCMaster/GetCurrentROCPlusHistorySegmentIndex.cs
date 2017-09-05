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
        public static int ROCMaster_GetCurrentROCPlusHistorySegmentIndex(SqlString deviceName, byte historySegment, byte historyType)
        {
            var deviceNameValue = deviceName.Value;
            return (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetCurrentROCPlusHistorySegmentIndex(null, null, null, null, historySegment, historyType);
        }
    }
}
