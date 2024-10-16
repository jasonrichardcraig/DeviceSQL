#region Imported Types

using DeviceSQL.Device.Roc.Data;
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
        public static SqlInt32 RocMaster_GetInt32Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var int32Parameter = new Int32Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (device as Device.Roc.RocMaster).ReadParameter<Int32Parameter>(null, null, null, null, ref int32Parameter);
            return int32Parameter.Value;
        }
    }
}
