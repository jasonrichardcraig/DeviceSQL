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
        public static SqlBoolean ROCMaster_SetOperatorIdentification(SqlString deviceName, SqlString operatorId, SqlInt32 password)
        {
            var deviceNameValue = deviceName.Value;
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).SetOperatorIdentification(null, null, null, null, operatorId.IsNull ? "LOI" : operatorId.Value, Convert.ToUInt16(password.IsNull ? 1000 : password.Value));
            return true;
        }
    }
}
