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
        public static SqlInt32 RocMaster_GetUInt16Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var uInt16Parameter = new UInt16Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (device as Device.Roc.RocMaster).ReadParameter<UInt16Parameter>(null, null, null, null, ref uInt16Parameter);
            return uInt16Parameter.Value;
        }
    }
}
