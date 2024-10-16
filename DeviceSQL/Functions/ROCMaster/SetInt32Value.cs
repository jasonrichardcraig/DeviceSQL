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
        public static SqlBoolean RocMaster_SetInt32Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt32 value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var int32Parameter = new Int32Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
            (device as Device.Roc.RocMaster).WriteParameter<Int32Parameter>(null, null, null, null, int32Parameter);
            return true;
        }
    }
}
