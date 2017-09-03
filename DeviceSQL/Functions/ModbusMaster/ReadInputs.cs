#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.ModbusMaster
{
    public partial class Functions
    {
        [SqlFunction]
        public static UserDefinedTypes.InputRegisterArray ModbusMaster_ReadInputs(SqlString deviceName, UserDefinedTypes.InputRegisterArray inputRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var inputRegisters = new List<Device.Modbus.Data.InputRegister>(inputRegisterArray.inputRegisters.Select(inputRegister => new Device.Modbus.Data.InputRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(inputRegister.Address.RelativeAddress.Value), inputRegister.Address.IsZeroBased.Value), inputRegister.ByteSwap.Value)));
            (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadInputRegisters(null, ref inputRegisters, null);
            return new UserDefinedTypes.InputRegisterArray() { inputRegisters = inputRegisters.Select(inputRegister => new UserDefinedTypes.InputRegister() { Address = new UserDefinedTypes.ModbusAddress { RelativeAddress = inputRegister.Address.RelativeAddress, IsZeroBased = inputRegister.Address.IsZeroBased }, ByteSwap = inputRegister.ByteSwap, Data = inputRegister.Data }).ToList() };
        }
    }
}
