#region Imported Types

using DeviceSQL.Device.Roc.Data;
using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static SqlBoolean RocMaster_SetUInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlByte value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var uInt8Parameter = new UInt8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
            (device as Device.Roc.RocMaster).WriteParameter<UInt8Parameter>(null, null, null, null, uInt8Parameter);
            return true;
        }
    }
}
