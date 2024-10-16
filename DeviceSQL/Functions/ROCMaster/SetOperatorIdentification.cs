#region Imported Types

using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static SqlBoolean RocMaster_SetOperatorIdentification(SqlString deviceName, SqlString operatorId, SqlInt32 password)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            (device as Device.Roc.RocMaster).SetOperatorIdentification(null, null, null, null, operatorId.IsNull ? "LOI" : operatorId.Value, Convert.ToUInt16(password.IsNull ? 1000 : password.Value));
            return true;
        }
    }
}
