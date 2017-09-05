using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean ROCMaster_SetRealTimeClock(SqlString deviceName, SqlDateTime dateTime)
        {
            var deviceNameValue = deviceName.Value;
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).SetRealTimeClock(null, null, null, null, dateTime.Value);
            return true;
        }
    }
}
