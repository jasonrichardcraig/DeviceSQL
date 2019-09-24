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
        public static ShortRegisterArray ReadShorts(SqlString deviceName, ShortRegisterArray shortRegisterArray)
        {
            //var deviceNameValue = deviceName.Value;
            //var shortRegisters = new List<Device.MODBUS.Data.ShortRegister>(shortRegisterArray.shortRegisters.Select(longRegister => new Device.MODBUS.Data.ShortRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(longRegister.Address.RelativeAddress.Value), longRegister.Address.IsZeroBased.Value), longRegister.ByteSwap.Value)));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadShortRegisters(null, ref shortRegisters, null);
            return ShortRegisterArray.Null; // new Types.MODBUSMaster.MODBUSMaster_ShortRegisterArray() { shortRegisters = shortRegisters.Select(shortRegister => new Types.MODBUSMaster.MODBUSMaster_ShortRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = shortRegister.Address.RelativeAddress, IsZeroBased = shortRegister.Address.IsZeroBased }, Data = shortRegister.Data }).ToList() };
        }
    }
}
