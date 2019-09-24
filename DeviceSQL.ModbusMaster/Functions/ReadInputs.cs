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
        public static InputRegisterArray ReadInputs(SqlString deviceName, InputRegisterArray inputRegisterArray)
        {
            //var deviceNameValue = deviceName.Value;
            // var inputRegisters = new List<Device.MODBUS.Data.InputRegister>(inputRegisterArray.inputRegisters.Select(inputRegister => new Device.MODBUS.Data.InputRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(inputRegister.Address.RelativeAddress.Value), inputRegister.Address.IsZeroBased.Value), inputRegister.ByteSwap.Value)));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadInputRegisters(null, ref inputRegisters, null);
            return InputRegisterArray.Null; // new Types.MODBUSMaster.MODBUSMaster_InputRegisterArray() { inputRegisters = inputRegisters.Select(inputRegister => new Types.MODBUSMaster.MODBUSMaster_InputRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = inputRegister.Address.RelativeAddress, IsZeroBased = inputRegister.Address.IsZeroBased }, ByteSwap = inputRegister.ByteSwap, Data = inputRegister.Data }).ToList() };
        }
    }
}
