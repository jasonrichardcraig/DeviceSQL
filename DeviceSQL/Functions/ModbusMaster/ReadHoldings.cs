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
        public static UserDefinedTypes.HoldingRegisterArray ReadHoldings(SqlString deviceName, UserDefinedTypes.HoldingRegisterArray holdingRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var holdingRegisters = new List<Device.Modbus.Data.HoldingRegister>(holdingRegisterArray.holdingRegisters.Select(holdingRegister => new Device.Modbus.Data.HoldingRegister(new Device.Modbus.Data.ModbusAddress(Convert.ToUInt16(holdingRegister.Address.RelativeAddress.Value), holdingRegister.Address.IsZeroBased.Value), holdingRegister.ByteSwap.Value)));
            (Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.Modbus.ModbusMaster).ReadHoldingRegisters(null, ref holdingRegisters, null);
            return new UserDefinedTypes.HoldingRegisterArray() { holdingRegisters = holdingRegisters.Select(holdingRegister => new UserDefinedTypes.HoldingRegister() { Address = new UserDefinedTypes.ModbusAddress { RelativeAddress = holdingRegister.Address.RelativeAddress, IsZeroBased = holdingRegister.Address.IsZeroBased }, ByteSwap = holdingRegister.ByteSwap, Data = holdingRegister.Data }).ToList() };
        }
    }
}
