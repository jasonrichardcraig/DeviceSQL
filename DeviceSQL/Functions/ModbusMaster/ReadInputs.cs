#region Imported Types

using DeviceSQL.Registries;
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
        public static Types.ModbusMaster.ModbusMaster_InputRegisterArray ModbusMaster_ReadInputs(SqlString deviceName, Types.ModbusMaster.ModbusMaster_InputRegisterArray inputRegisterArray)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            var inputRegisters = new List<Device.Modbus.Data.InputRegister>(inputRegisterArray.inputRegisters.Select(inputRegister => new Device.Modbus.Data.InputRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(inputRegister.Address.RelativeAddress.Value), inputRegister.Address.IsZeroBased.Value), inputRegister.ByteSwap.Value)));
            (device as Device.Modbus.ModbusMaster).ReadInputRegisters(null, ref inputRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_InputRegisterArray() { inputRegisters = inputRegisters.Select(inputRegister => new Types.ModbusMaster.ModbusMaster_InputRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = inputRegister.Address.RelativeAddress, IsZeroBased = inputRegister.Address.IsZeroBased }, ByteSwap = inputRegister.ByteSwap, Data = inputRegister.Data }).ToList() };
        }
    }
}
