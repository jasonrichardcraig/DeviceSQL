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
        public static SqlBoolean SetOperatorIdentification(SqlString deviceName, SqlString operatorId, SqlInt32 password)
        {
            var deviceNameValue = deviceName.Value;
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).SetOperatorIdentification(null, null, null, null, operatorId.IsNull ? "LOI" : operatorId.Value, Convert.ToUInt16(password.IsNull ? 1000 : password.Value));
            return true;
        }
    }
}
