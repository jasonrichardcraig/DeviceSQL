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
        public static SqlBoolean RocMaster_SetFloatValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlSingle value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var flpParameter = new FlpParameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
            (device as Device.Roc.RocMaster).WriteParameter<FlpParameter>(null, null, null, null, flpParameter);
            return true;
        }
    }
}
