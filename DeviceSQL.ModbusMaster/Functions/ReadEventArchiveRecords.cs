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
        public static EventArchiveRecordArray ReadEventArchiveRecords(SqlString deviceName, ModbusAddress eventArchiveAddress, SqlInt32 index)
        {
            //var deviceNameValue = deviceName.Value;
            //var eventArchiveRecords = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadEventArchiveRecord(null, new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(eventArchiveAddress.RelativeAddress.Value), eventArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), null);
            return EventArchiveRecordArray.Null; // new Types.MODBUSMaster.MODBUSMaster_EventArchiveRecordArray() { eventArchiveRecords = eventArchiveRecords.Select(ear => new Types.MODBUSMaster.MODBUSMaster_EventArchiveRecord() { Index = ear.Index, Data = ear.Data }).ToList() };
        }
    }
}
