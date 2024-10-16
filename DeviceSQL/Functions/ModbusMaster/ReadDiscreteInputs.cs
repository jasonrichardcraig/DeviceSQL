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
        public static Types.ModbusMaster.ModbusMaster_DiscreteInputRegisterArray ModbusMaster_ReadDiscreteInputs(SqlString deviceName, Types.ModbusMaster.ModbusMaster_DiscreteInputRegisterArray discreteInputRegisterArray)
        {
            var discreteInputRegisters = new List<Device.Modbus.Data.DiscreteInputRegister>(discreteInputRegisterArray.discreteInputRegisters.Select(discreteInputRegister => new Device.Modbus.Data.DiscreteInputRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(discreteInputRegister.Address.RelativeAddress.Value), discreteInputRegister.Address.IsZeroBased.Value))));
            var device = ServiceRegistry.GetDevice(deviceName.Value);
            (device as Device.Modbus.ModbusMaster).ReadDiscreteInputRegisters(null, ref discreteInputRegisters, null);
            return new Types.ModbusMaster.ModbusMaster_DiscreteInputRegisterArray() { discreteInputRegisters = discreteInputRegisters.Select(discreteInputRegister => new Types.ModbusMaster.ModbusMaster_DiscreteInputRegister() { Address = new Types.ModbusMaster.ModbusMaster_ModbusAddress { RelativeAddress = discreteInputRegister.Address.RelativeAddress, IsZeroBased = discreteInputRegister.Address.IsZeroBased }, Data = discreteInputRegister.Data }).ToList() };
        }
    }
}