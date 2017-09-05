using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace DeviceSQL.Functions
{
    public partial class ModbusMaster
    {
        [SqlFunction]
        public static Types.ModbusMaster.CoilRegisterArray ReadCoils(SqlString deviceName, Types.ModbusMaster.CoilRegisterArray coilRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var coilRegisters = new List<Device.Modbus.Data.CoilRegister>(coilRegisterArray.coilRegisters.Select(coilRegister => new Device.Modbus.Data.CoilRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(coilRegister.Address.RelativeAddress.Value), coilRegister.Address.IsZeroBased.Value))));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadCoilRegisters(null, ref coilRegisters, null);
            return new Types.ModbusMaster.CoilRegisterArray() { coilRegisters = coilRegisters.Select(coilRegister => new Types.ModbusMaster.CoilRegister() { Address = new Types.ModbusMaster.ModbusAddress { RelativeAddress = coilRegister.Address.RelativeAddress, IsZeroBased = coilRegister.Address.IsZeroBased }, Data = coilRegister.Data }).ToList() };
        }
    }
}
