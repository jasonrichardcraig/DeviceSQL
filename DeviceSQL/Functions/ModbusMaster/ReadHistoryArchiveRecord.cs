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
        public static Types.MODBUSMaster.MODBUSMaster_HistoryArchiveRecord ReadHistoryArchiveRecord(SqlString deviceName, Types.MODBUSMaster.MODBUSMaster_MODBUSAddress historyArchiveAddress, SqlInt32 index, SqlByte recordSize)
        {
            var deviceNameValue = deviceName.Value;
            var historyArchiveRecord = (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.MODBUS.MODBUSMaster).ReadHistoryArchiveRecord(null, new Device.MODBUS.Data.MODBUSAddress(Convert.ToUInt16(historyArchiveAddress.RelativeAddress.Value), historyArchiveAddress.IsZeroBased.Value), Convert.ToUInt16(index.Value), recordSize.Value, null);
            return new Types.MODBUSMaster.MODBUSMaster_HistoryArchiveRecord() { Index = historyArchiveRecord.Index, Data = historyArchiveRecord.Data };
        }
    }
}
