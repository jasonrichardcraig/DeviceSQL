// DeviceSQL ROC Libraries
#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public class ArchiveInfo
    {

        #region Fields

        public byte[] data;

        #endregion

        #region Properties

        public UInt16 AlarmLogPointer
        {
            get
            {
                return BitConverter.ToUInt16(data, 0);
            }
        }

        public UInt16 EventLogPointer
        {
            get
            {
                return BitConverter.ToUInt16(data, 2);
            }
        }

        public UInt16 BaseRamCurrentHistoricalHour
        {
            get
            {
                return BitConverter.ToUInt16(data, 4);
            }
        }

        public UInt16 BaseRam1CurrentHistoricalHour
        {
            get
            {
                return BitConverter.ToUInt16(data, 6);
            }
        }

        public UInt16 BaseRam2CurrentHistoricalHour
        {
            get
            {
                return BitConverter.ToUInt16(data, 8);
            }
        }

        public Byte BaseRamCurrentHistoricalDay
        {
            get
            {
                return data[12];
            }
        }

        public byte Base1RamCurrentHistoricalDay
        {
            get
            {
                return data[13];
            }
        }

        public byte BaseRam2CurrentHistoricalDay
        {
            get
            {
                return data[14];
            }
        }

        public UInt16 MaximumNumberOfAlarms
        {
            get
            {
                return BitConverter.ToUInt16(data, 16);
            }
        }

        public UInt16 MaximumNumberOfEvents
        {
            get
            {
                return BitConverter.ToUInt16(data, 18);
            }
        }

        public byte BaseRamNumberOfDays
        {
            get
            {
                return data[20];
            }
        }

        public byte BaseRam1NumberOfDays
        {
            get
            {
                return data[21];
            }
        }

        public byte BaseRam2NumberOfDays
        {
            get
            {
                return data[22];
            }
        }

        public UInt16 CurrentAuditLogPointer
        {
            get
            {
                return BitConverter.ToUInt16(data, 24);
            }
        }

        public byte MinutesPerHistoricalPeriod
        {
            get
            {
                return data[24];
            }
        }

        #endregion

        #region Constructor(s)

        internal ArchiveInfo(byte[] data)
        {
            this.data = data;
        }

        #endregion

    }
}
