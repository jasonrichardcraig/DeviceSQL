#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class MODBUSMaster
    {
        [SqlFunction]
        public static Types.MODBUSMaster.MODBUSMaster_HoldingRegisterArray ReadHoldings(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_HoldingRegisterArray holdingRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var holdingRegisters = new List<Device.MODBUS.Data.HoldingRegister>(holdingRegisterArray.holdingRegisters.Select(holdingRegister => new Device.MODBUS.Data.HoldingRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(holdingRegister.Address.RelativeAddress.Value), holdingRegister.Address.IsZeroBased.Value), holdingRegister.ByteSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadHoldingRegisters(null, ref holdingRegisters, null);
            return new Types.MODBUSMaster.MODBUSMaster_HoldingRegisterArray() { holdingRegisters = holdingRegisters.Select(holdingRegister => new Types.MODBUSMaster.MODBUSMaster_HoldingRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = holdingRegister.Address.RelativeAddress, IsZeroBased = holdingRegister.Address.IsZeroBased }, ByteSwap = holdingRegister.ByteSwap, Data = holdingRegister.Data }).ToList() };
        }
    }
}
