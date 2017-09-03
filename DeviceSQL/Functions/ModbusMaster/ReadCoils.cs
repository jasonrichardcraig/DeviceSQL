using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace DeviceSQL.ModbusMaster
{
    public partial class Functions
    {
        [SqlFunction]
        public static UserDefinedTypes.CoilRegisterArray ReadCoils(SqlString deviceName, UserDefinedTypes.CoilRegisterArray coilRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var coilRegisters = new List<Device.Modbus.Data.CoilRegister>(coilRegisterArray.coilRegisters.Select(coilRegister => new Device.Modbus.Data.CoilRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(coilRegister.Address.RelativeAddress.Value), coilRegister.Address.IsZeroBased.Value))));
            (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadCoilRegisters(null, ref coilRegisters, null);
            return new UserDefinedTypes.CoilRegisterArray() { coilRegisters = coilRegisters.Select(coilRegister => new UserDefinedTypes.CoilRegister() { Address = new UserDefinedTypes.ModbusAddress { RelativeAddress = coilRegister.Address.RelativeAddress, IsZeroBased = coilRegister.Address.IsZeroBased }, Data = coilRegister.Data }).ToList() };
        }
    }
}
