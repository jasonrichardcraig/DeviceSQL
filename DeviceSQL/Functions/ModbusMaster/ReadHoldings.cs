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
        public static Types.ModbusMaster.HoldingRegisterArray ReadHoldings(SqlString deviceName, Types.ModbusMaster.HoldingRegisterArray holdingRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var holdingRegisters = new List<Device.Modbus.Data.HoldingRegister>(holdingRegisterArray.holdingRegisters.Select(holdingRegister => new Device.Modbus.Data.HoldingRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(holdingRegister.Address.RelativeAddress.Value), holdingRegister.Address.IsZeroBased.Value), holdingRegister.ByteSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadHoldingRegisters(null, ref holdingRegisters, null);
            return new Types.ModbusMaster.HoldingRegisterArray() { holdingRegisters = holdingRegisters.Select(holdingRegister => new Types.ModbusMaster.HoldingRegister() { Address = new Types.ModbusMaster.ModbusAddress { RelativeAddress = holdingRegister.Address.RelativeAddress, IsZeroBased = holdingRegister.Address.IsZeroBased }, ByteSwap = holdingRegister.ByteSwap, Data = holdingRegister.Data }).ToList() };
        }
    }
}
