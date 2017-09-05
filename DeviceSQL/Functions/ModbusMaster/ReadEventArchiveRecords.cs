#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class MODBUSMaster
    {
        [SqlFunction]
        public static Types.MODBUSMaster.MODBUSMaster_EventArchiveRecordArray MODBUSMaster_ReadEventArchiveRecords(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_MODBUSAddress eventArchiveAddress, SqlInt32 index)
        {
            var deviceNameValue = deviceName.Value;
            var eventArchiveRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadEventArchiveRecord(null, new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(eventArchiveAddress.RelativeAddress.Value), eventArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), null);
            return new Types.MODBUSMaster.MODBUSMaster_EventArchiveRecordArray() { eventArchiveRecords = eventArchiveRecords.Select(ear => new Types.MODBUSMaster.MODBUSMaster_EventArchiveRecord() { Index = ear.Index, Data = ear.Data }).ToList() };
        }
    }
}
