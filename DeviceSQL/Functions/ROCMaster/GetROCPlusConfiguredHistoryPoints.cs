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
        public static Types.ROCMaster.ROCMaster_ROCPlusHistoryPointArray ROCMaster_GetROCPlusConfiguredHistoryPoints(SqlString deviceName, byte historySegment)
        {
            var deviceNameValue = deviceName.Value;
            var configuredHistoryPoints = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetROCPlusConfiguredHistoryPoints(null, null, null, null, historySegment);
            return new Types.ROCMaster.ROCMaster_ROCPlusHistoryPointArray() { historyPoints = configuredHistoryPoints };
        }
    }
}
