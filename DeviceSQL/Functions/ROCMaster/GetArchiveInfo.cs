#region Imported Types

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
        public static Types.RocMaster.RocMaster_ArchiveInformation RocMaster_GetArchiveInfo(SqlString deviceName)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var archiveInfo = (device as Device.Roc.RocMaster).GetArchiveInfo(null, null, null, null);
            return new Types.RocMaster.RocMaster_ArchiveInformation() { Data = archiveInfo.data };
        }
    }
}
