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
        public static Types.ROCMaster.ROCMaster_ArchiveInformation ROCMaster_GetArchiveInfo(SqlString deviceName)
        {
            var deviceNameValue = deviceName.Value;
            var archiveInfo = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).GetArchiveInfo(null, null, null, null);
            return new Types.ROCMaster.ROCMaster_ArchiveInformation() { Data = archiveInfo.data };
        }
    }
}
