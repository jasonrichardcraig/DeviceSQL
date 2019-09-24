#region Imported Types

using DeviceSQL.SQLTypes.Modbus;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;

#endregion

namespace DeviceSQL.Functions
{
    public partial class MODBUSMaster
    {
        [SqlFunction]
        public static LongRegisterArray ReadLongs(SqlString deviceName, LongRegisterArray longRegisterArray)
        {
            // var deviceNameValue = deviceName.Value;
            //var longRegisters = new List<Device.MODBUS.Data.LongRegister>(longRegisterArray.longRegisters.Select(longRegister => new Device.MODBUS.Data.LongRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value, longRegister.WordSwap.Value)));
            // (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadLongRegisters(null, ref longRegisters, null);
            return LongRegisterArray.Null; // new Types.MODBUSMaster.MODBUSMaster_LongRegisterArray() { longRegisters = longRegisters.Select(floatRegister => new Types.MODBUSMaster.MODBUSMaster_LongRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = floatRegister.Address.RelativeAddress, IsZeroBased = floatRegister.Address.IsZeroBased }, Data = floatRegister.Data }).ToList() };
        }
    }
}
