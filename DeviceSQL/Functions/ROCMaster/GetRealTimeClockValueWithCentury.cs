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
        public static SqlDateTime ROCMaster_GetRealTimeClockValueWithCentury(SqlString deviceName, SqlInt32 century)
        {
            var deviceNameValue = deviceName.Value;
            return new SqlDateTime((DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetRealTimeClockValue(null, null, null, null, Convert.ToUInt16(century.Value)));
        }
    }
}
