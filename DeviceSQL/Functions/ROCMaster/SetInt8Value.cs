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
        public static SqlBoolean RocMaster_SetInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt16 value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var int8Parameter = new Int8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = Convert.ToSByte(value.Value) };
            (device as Device.Roc.RocMaster).WriteParameter<Int8Parameter>(null, null, null, null, int8Parameter);
            return true;
        }
    }
}
