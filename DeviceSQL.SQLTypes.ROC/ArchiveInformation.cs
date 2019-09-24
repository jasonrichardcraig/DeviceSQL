#region Imported Types

using DeviceSQL.SQLTypes.ROC.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

#endregion

namespace DeviceSQL.SQLTypes.ROC
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = 27)]
    public struct ArchiveInformation : INullable, IBinarySerialize
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public bool IsNull
        {
            get;
            internal set;
        }

        public static ArchiveInformation Null
        {
            get
            {
                return new ArchiveInformation() { IsNull = true };
            }
        }

        public byte[] Data
        {
            get
            {
                if (data == null)
                {
                    data = new byte[26];
                }
                return data;
            }
            set
            {
                data = value;
            }
        }

        public int AlarmLogPointer
        {
            get
            {
                return new ArchiveInfo(Data).AlarmLogPointer;
            }
        }

        public int EventLogPointer
        {
            get
            {
                return new ArchiveInfo(Data).EventLogPointer;
            }
        }

        public int BaseRamCurrentHistoricalHour
        {
            get
            {
                return new ArchiveInfo(Data).BaseRamCurrentHistoricalHour;
            }
        }

        public int BaseRam1CurrentHistoricalHour
        {
            get
            {
                return new ArchiveInfo(Data).BaseRam1CurrentHistoricalHour;
            }
        }

        public int BaseRam2CurrentHistoricalHour
        {
            get
            {
                return new ArchiveInfo(Data).BaseRam2CurrentHistoricalHour;
            }
        }

        public byte BaseRamCurrentHistoricalDay
        {
            get
            {
                return new ArchiveInfo(Data).BaseRamCurrentHistoricalDay;
            }
        }

        public byte Base1RamCurrentHistoricalDay
        {
            get
            {
                return new ArchiveInfo(Data).Base1RamCurrentHistoricalDay;
            }
        }

        public byte BaseRam2CurrentHistoricalDay
        {
            get
            {
                return new ArchiveInfo(Data).BaseRam2CurrentHistoricalDay;
            }
        }

        public int MaximumNumberOfAlarms
        {
            get
            {
                return new ArchiveInfo(Data).MaximumNumberOfAlarms;
            }
        }

        public int MaximumNumberOfEvents
        {
            get
            {
                return new ArchiveInfo(Data).MaximumNumberOfEvents;
            }
        }

        public byte BaseRamNumberOfDays
        {
            get
            {
                return new ArchiveInfo(Data).BaseRamNumberOfDays;
            }
        }

        public byte BaseRam1NumberOfDays
        {
            get
            {
                return new ArchiveInfo(Data).BaseRam1NumberOfDays;
            }
        }

        public byte BaseRam2NumberOfDays
        {
            get
            {
                return new ArchiveInfo(Data).BaseRam2NumberOfDays;
            }
        }

        public int CurrentAuditLogPointer
        {
            get
            {
                return new ArchiveInfo(Data).CurrentAuditLogPointer;
            }
        }

        public byte MinutesPerHistoricalPeriod
        {
            get
            {
                return new ArchiveInfo(Data).MinutesPerHistoricalPeriod;
            }
        }

        #endregion

        #region Helper Methods

        public static ArchiveInformation Parse(SqlString stringToParse)
        {
            var base64Bytes = Convert.FromBase64String(stringToParse.Value);
            if (base64Bytes.Length == 24)
            {
                return new ArchiveInformation() { Data = base64Bytes };
            }
            else
            {
                throw new ArgumentException("Input must be exactly 24 bytes");
            }
        }

        public override string ToString()
        {
            return Convert.ToBase64String(Data);
        }


        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            IsNull = binaryReader.ReadBoolean();
            if (!IsNull)
            {
                Data = binaryReader.ReadBytes(26);
            }
        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            if (!IsNull)
            {
                binaryWriter.Write(Data, 0, 26);
            }
        }

        #endregion

    }
}
