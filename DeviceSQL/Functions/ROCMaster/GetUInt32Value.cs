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
        public static SqlInt64 RocMaster_GetUInt32Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var uInt32Parameter = new UInt32Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (device as Device.Roc.RocMaster).ReadParameter<UInt32Parameter>(null, null, null, null, ref uInt32Parameter);
            return uInt32Parameter.Value;
        }
    }
}
