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
        public static Types.MODBUSMaster.MODBUSMaster_LongRegisterArray MODBUSMaster_ReadLongs(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_LongRegisterArray longRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var longRegisters = new List<Device.MODBUS.Data.LongRegister>(longRegisterArray.longRegisters.Select(longRegister => new Device.MODBUS.Data.LongRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value, longRegister.WordSwap.Value)));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadLongRegisters(null, ref longRegisters, null);
            return new Types.MODBUSMaster.MODBUSMaster_LongRegisterArray() { longRegisters = longRegisters.Select(floatRegister => new Types.MODBUSMaster.MODBUSMaster_LongRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = floatRegister.Address.RelativeAddress, IsZeroBased = floatRegister.Address.IsZeroBased }, Data = floatRegister.Data }).ToList() };
        }
    }
}
