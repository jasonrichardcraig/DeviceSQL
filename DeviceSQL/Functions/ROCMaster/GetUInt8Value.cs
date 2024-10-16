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
        public static SqlByte RocMaster_GetUInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var uInt8Parameter = new UInt8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (device as Device.Roc.RocMaster).ReadParameter<UInt8Parameter>(null, null, null, null, ref uInt8Parameter);
            return uInt8Parameter.Value;
        }
    }
}
