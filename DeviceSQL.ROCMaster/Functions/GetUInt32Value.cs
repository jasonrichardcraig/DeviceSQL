#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlInt64 GetUInt32Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var uInt32Parameter = new UInt32Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<UInt32Parameter>(null, null, null, null, ref uInt32Parameter);
            return 0; // uInt32Parameter.Value;
        }
    }
}
