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
        public static SqlBoolean RocMaster_SetRealTimeClock(SqlString deviceName, SqlDateTime dateTime)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            (device as Device.Roc.RocMaster).SetRealTimeClock(null, null, null, null, dateTime.Value);
            return true;
        }
    }
}
