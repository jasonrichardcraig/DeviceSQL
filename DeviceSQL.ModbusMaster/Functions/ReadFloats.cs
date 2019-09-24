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
        public static FloatRegisterArray ReadFloats(SqlString deviceName, FloatRegisterArray floatRegisterArray)
        {
            //var deviceNameValue = deviceName.Value;
            //var floatRegisters = new List<Device.MODBUS.Data.FloatRegister>(floatRegisterArray.floatRegisters.Select(floatRegister => new Device.MODBUS.Data.FloatRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(floatRegister.Address.RelativeAddress.Value), floatRegister.Address.IsZeroBased.Value), floatRegister.ByteSwap.Value, floatRegister.WordSwap.Value)));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadFloatRegisters(null, ref floatRegisters, null);
            return FloatRegisterArray.Null; // new Types.MODBUSMaster.MODBUSMaster_FloatRegisterArray() { floatRegisters = floatRegisters.Select(floatRegister => new Types.MODBUSMaster.MODBUSMaster_FloatRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = floatRegister.Address.RelativeAddress, IsZeroBased = floatRegister.Address.IsZeroBased }, Data = floatRegister.Data }).ToList() };
        }
    }
}
