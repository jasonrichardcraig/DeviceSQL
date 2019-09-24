#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlByte GetUInt8Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            // var uInt8Parameter = new UInt8Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<UInt8Parameter>(null, null, null, null, ref uInt8Parameter);
            return 0; // uInt8Parameter.Value;
        }
    }
}
