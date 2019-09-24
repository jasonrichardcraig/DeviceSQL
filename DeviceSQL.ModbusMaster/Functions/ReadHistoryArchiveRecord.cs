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
        public static HistoryArchiveRecord ReadHistoryArchiveRecord(SqlString deviceName, ModbusAddress historyArchiveAddress, SqlInt32 index, SqlByte recordSize)
        {
            //var deviceNameValue = deviceName.Value;
            //var historyArchiveRecord = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadHistoryArchiveRecord(null, new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(historyArchiveAddress.RelativeAddress.Value), historyArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), recordSize.Value, null);
            return HistoryArchiveRecord.Null; // new Types.MODBUSMaster.MODBUSMaster_HistoryArchiveRecord() { Index = historyArchiveRecord.Index, Data = historyArchiveRecord.Data };
        }
    }
}
