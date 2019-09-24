#region Imported Types

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlDouble GetDoubleValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber)
        {
            //var deviceNameValue = deviceName.Value;
            //var DoubleParameter = new DoubleParameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).ReadParameter<DoubleParameter>(null, null, null, null, ref DoubleParameter);
            return 0;// DoubleParameter.NullableValue.HasValue ? DoubleParameter.Value : SqlDouble.Null;
        }
    }
}
