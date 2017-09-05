using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;
using DeviceSQL.Device.ROC.Data;

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static int ROCMaster_ClearAuditLogEventFlags(SqlString deviceName, byte numberOfAuditLogRecordsToClear, int startingAuditLogPointer)
        {
            var deviceNameValue = deviceName.Value;
            return (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ClearAuditLogEventFlags(null, null, null, null, numberOfAuditLogRecordsToClear, Convert.ToUInt16(startingAuditLogPointer));
        }
    }
}
