#region Imported Types

using DeviceSQL.Device.Roc.Data;
using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static Types.RocMaster.RocMaster_FSTInformation RocMaster_GetFstInformation(SqlString deviceName, SqlByte fstNumber)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var rocMaster = (device as Device.Roc.RocMaster);

            var fstHeaderInfo = rocMaster.GetFstHeaderInfo(null, null, null, null, fstNumber.Value);
            var fstSizeParameter = new UInt16Parameter(new Tlp(16, fstNumber.Value, 25));

            rocMaster.ReadParameter(null, null, null, null, ref fstSizeParameter);

            var fstSize = fstSizeParameter.Value;
            var codeBytes = new List<byte>();

            while (fstSize > codeBytes.Count)
            {
                codeBytes.AddRange(rocMaster.GetFstCodeChunk(null, null, null, null, 0, (byte)codeBytes.Count, (fstSize - codeBytes.Count) > 240 ? (byte)240 : (byte)(fstSize - codeBytes.Count)).CodeBytes);
            }

            return new Types.RocMaster.RocMaster_FSTInformation()
            {
                FSTNumber = fstNumber.Value,
                Version = fstHeaderInfo.Version,
                Description = fstHeaderInfo.Description,
                FSTCode = codeBytes.ToArray()
            };

        }

    }
}
