#region Imported Types

using DeviceSQL.SQLTypes.Modbus;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class MODBUSMaster
    {
        [SqlFunction]
        public static StringRegister ReadString(SqlString deviceName, StringRegister stringRegister)
        {
            //var deviceNameValue = deviceName.Value;
            // var stringRegisterValue = new Device.MODBUS.Data.StringRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(stringRegister.Address.RelativeAddress.Value), stringRegister.Address.IsZeroBased.Value), stringRegister.Length.Value);
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadStringRegister(null, null, ref stringRegisterValue);
            //stringRegister.Data = stringRegisterValue.Data;
            return StringRegister.Null; // stringRegister;
        }
    }
}
