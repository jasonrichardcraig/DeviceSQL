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
        public static Types.MODBUSMaster.MODBUSMaster_DiscreteInputRegisterArray ReadDiscreteInputs(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_DiscreteInputRegisterArray discreteInputRegisterArray)
        {
            var deviceNameValue = deviceName.Value;
            var discreteInputRegisters = new List<Device.MODBUS.Data.DiscreteInputRegister>(discreteInputRegisterArray.discreteInputRegisters.Select(discreteInputRegister => new Device.MODBUS.Data.DiscreteInputRegister(new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(discreteInputRegister.Address.RelativeAddress.Value), discreteInputRegister.Address.IsZeroBased.Value))));
            (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadDiscreteInputRegisters(null, ref discreteInputRegisters, null);
            return new Types.MODBUSMaster.MODBUSMaster_DiscreteInputRegisterArray() { discreteInputRegisters = discreteInputRegisters.Select(discreteInputRegister => new Types.MODBUSMaster.MODBUSMaster_DiscreteInputRegister() { Address = new Types.MODBUSMaster.MODBUSMaster_MODBUSAddress { RelativeAddress = discreteInputRegister.Address.RelativeAddress, IsZeroBased = discreteInputRegister.Address.IsZeroBased }, Data = discreteInputRegister.Data }).ToList() };
        }
    }
}