#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class ModbusMaster
    { 
        [SqlFunction]
        public static Types.ModbusMaster.InputRegisterArray ReadInputs(SqlString deviceName, Types.ModbusMaster.InputRegisterArray inputRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var inputRegisters = new List<Device.Modbus.Data.InputRegister>(inputRegisterArray.inputRegisters.Select(inputRegister => new Device.Modbus.Data.InputRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(inputRegister.Address.RelativeAddress.Value), inputRegister.Address.IsZeroBased.Value), inputRegister.ByteSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadInputRegisters(null, ref inputRegisters, null);
            return new Types.ModbusMaster.InputRegisterArray() { inputRegisters = inputRegisters.Select(inputRegister => new Types.ModbusMaster.InputRegister() { Address = new Types.ModbusMaster.ModbusAddress { RelativeAddress = inputRegister.Address.RelativeAddress, IsZeroBased = inputRegister.Address.IsZeroBased }, ByteSwap = inputRegister.ByteSwap, Data = inputRegister.Data }).ToList() };
        }
    }
}
