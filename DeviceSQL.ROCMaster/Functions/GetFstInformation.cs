#region Imported Types

using DeviceSQL.SQLTypes.ROC;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static FSTInformation GetFstInformation(SqlString deviceName, SqlByte fstNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var rocMaster = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster);

            //var fstHeaderInfo = rocMaster.GetFstHeaderInfo(null, null, null, null, fstNumber.Value);
            //var fstSizeParameter = new UInt16Parameter(new Tlp(16, fstNumber.Value, 25));

            //rocMaster.ReadParameter(null, null, null, null, ref fstSizeParameter);

            //var fstSize = fstSizeParameter.Value;
            //var codeBytes = new List<byte>();

            //while (fstSize > codeBytes.Count)
            //{
            //    codeBytes.AddRange(rocMaster.GetFstCodeChunk(null, null, null, null, 0, (byte)codeBytes.Count, (fstSize - codeBytes.Count) > 240 ? (byte)240 : (byte)(fstSize - codeBytes.Count)).CodeBytes);
            //}

            //return new Types.ROCMaster.ROCMaster_FSTInformation()
            //{
            //    FSTNumber = fstNumber.Value,
            //    Version = fstHeaderInfo.Version,
            //    Description = fstHeaderInfo.Description,
            //    FSTCode = codeBytes.ToArray()
            //};

            return FSTInformation.Null;
        }

    }
}
