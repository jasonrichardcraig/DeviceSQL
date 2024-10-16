#region Imported Types

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
        public static SqlDateTime RocMaster_GetTimeValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var timeParameter = new Device.Roc.Data.TimeParameter(new Device.Roc.Data.Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            (device as Device.Roc.RocMaster).ReadParameter<Device.Roc.Data.TimeParameter>(null, null, null, null, ref timeParameter);
            return timeParameter.Value;
        }
    }
}
