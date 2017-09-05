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
        public static Types.ModbusMaster.DiscreteInputRegisterArray ReadDiscreteInputs(SqlString deviceName, Types.ModbusMaster.DiscreteInputRegisterArray discreteInputRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var discreteInputRegisters = new List<Device.Modbus.Data.DiscreteInputRegister>(discreteInputRegisterArray.discreteInputRegisters.Select(discreteInputRegister => new Device.Modbus.Data.DiscreteInputRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(discreteInputRegister.Address.RelativeAddress.Value), discreteInputRegister.Address.IsZeroBased.Value))));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadDiscreteInputRegisters(null, ref discreteInputRegisters, null);
            return new Types.ModbusMaster.DiscreteInputRegisterArray() { discreteInputRegisters = discreteInputRegisters.Select(discreteInputRegister => new Types.ModbusMaster.DiscreteInputRegister() { Address = new Types.ModbusMaster.ModbusAddress { RelativeAddress = discreteInputRegister.Address.RelativeAddress, IsZeroBased = discreteInputRegister.Address.IsZeroBased }, Data = discreteInputRegister.Data }).ToList() };
        }
    }
}