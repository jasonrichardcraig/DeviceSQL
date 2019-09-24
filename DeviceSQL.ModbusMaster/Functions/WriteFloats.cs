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
        public static SqlBoolean WriteFloats(SqlString deviceName, FloatRegisterArray floatRegisterArray)
        {
            //var deviceNameValue = deviceName.Value;
           // var floatRegisters = new List<Device.MODBUS.Data.FloatRegister>(floatRegisterArray.floatRegisters.Select(floatRegister => new Device.MODBUS.Data.FloatRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(floatRegister.Address.RelativeAddress.Value), floatRegister.Address.IsZeroBased.Value), floatRegister.ByteSwap.Value, floatRegister.WordSwap.Value)));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).WriteFloatRegisters(null, floatRegisters, null);
            return true;
        }
    }
}
