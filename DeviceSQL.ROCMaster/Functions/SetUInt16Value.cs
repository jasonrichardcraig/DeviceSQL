#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean SetUInt16Value(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlInt32 value)
        {
            //var deviceNameValue = deviceName.Value;
            //var uInt16Parameter = new UInt16Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = Convert.ToUInt16(value.Value) };
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<UInt16Parameter>(null, null, null, null, ref uInt16Parameter);
            return true;
        }
    }
}