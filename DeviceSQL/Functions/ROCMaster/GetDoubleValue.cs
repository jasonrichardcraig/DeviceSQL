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
        public static SqlDouble RocMaster_GetDoubleValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var DoubleParameter = new DoubleParameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (device as Device.Roc.RocMaster).ReadParameter<DoubleParameter>(null, null, null, null, ref DoubleParameter);
            return DoubleParameter.NullableValue.HasValue ? DoubleParameter.Value : SqlDouble.Null;
        }
    }
}
