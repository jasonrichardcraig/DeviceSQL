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
        public static Types.MODBUSMaster.MODBUSMaster_ShortRegisterArray ReadShorts(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_ShortRegisterArray shortRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var shortRegisters = new List<Device.MODBUS.Data.ShortRegister>(shortRegisterArray.shortRegisters.Select(longRegister => new Device.MODBUS.Data.ShortRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadShortRegisters(null, ref shortRegisters, null);
            return new Types.MODBUSMaster.MODBUSMaster_ShortRegisterArray() { shortRegisters = shortRegisters.Select(shortRegister => new Types.MODBUSMaster.MODBUSMaster_ShortRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = shortRegister.Address.RelativeAddress, IsZeroBased = shortRegister.Address.IsZeroBased }, Data = shortRegister.Data }).ToList() };
        }
    }
}
