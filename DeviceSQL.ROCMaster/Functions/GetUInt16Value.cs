#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlInt32 GetUInt16Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var uInt16Parameter = new UInt16Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<UInt16Parameter>(null, null, null, null, ref uInt16Parameter);
            return 0; // uInt16Parameter.Value;
        }
    }
}
