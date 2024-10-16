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
        public static SqlBoolean RocMaster_SetInt16Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt16 value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var int16Parameter = new Int16Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
            (device as Device.Roc.RocMaster).WriteParameter<Int16Parameter>(null, null, null, null, int16Parameter);
            return true;
        }
    }
}
