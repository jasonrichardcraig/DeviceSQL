#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class AuditLogRecord : EventRecord
    {

        #region Properties

        public ushort SequenceNumber
        {
            get
            {
                return Convert.ToUInt16(BitConverter.ToUInt16(data, 23) & 32767);
            }
        }

        public bool EventNotSaved
        {
            get
            {
                return ((BitConverter.ToUInt16(data, 23) & 32768) == 0);
            }
        }

        #endregion

        #region Constructor

        internal AuditLogRecord(ushort index, byte[] data) : base(index, data)
        {
        }

        #endregion

    }
}
