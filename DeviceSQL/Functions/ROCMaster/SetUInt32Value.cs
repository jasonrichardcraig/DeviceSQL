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
        public static SqlBoolean RocMaster_SetUInt32Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt64 value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var uInt32Parameter = new UInt32Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = Convert.ToUInt32(value.Value) };
            (device as Device.Roc.RocMaster).WriteParameter<UInt32Parameter>(null, null, null, null, uInt32Parameter);
            return true;
        }
    }
}
