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
        public static CoilRegisterArray ReadCoils(SqlString deviceName, CoilRegisterArray coilRegisterArray)
        {
            //var deviceNameValue = deviceName.Value;
            //var coilRegisters = new List<Device.MODBUS.Data.CoilRegister>(coilRegisterArray.coilRegisters.Select(coilRegister => new Device.MODBUS.Data.CoilRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(coilRegister.Address.RelativeAddress.Value), coilRegister.Address.IsZeroBased.Value))));
            //(DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadCoilRegisters(null, ref coilRegisters, null);
            return CoilRegisterArray.Null; // new Types.MODBUSMaster.MODBUSMaster_CoilRegisterArray() { coilRegisters = coilRegisters.Select(coilRegister => new Types.MODBUSMaster.MODBUSMaster_CoilRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = coilRegister.Address.RelativeAddress, IsZeroBased = coilRegister.Address.IsZeroBased }, Data = coilRegister.Data }).ToList() };
        }
    }
}
