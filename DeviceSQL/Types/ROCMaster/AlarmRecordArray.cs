#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.RocMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct RocMaster_AlarmRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_AlarmRecord> alarmRecords;

        #endregion

        #region Properties

        internal RocMaster_AlarmRecord this[int index]
        {
            get
            {
                return AlarmRecords[index];
            }
            set
            {
                AlarmRecords[index] = value;
            }
        }

        public bool IsNull
        {
            get;
            internal set;
        }

        public int Length
        {
            get
            {
                return AlarmRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<RocMaster_AlarmRecord> AlarmRecords
        {
            get
            {
                if (alarmRecords == null)
                {
                    alarmRecords = new List<RocMaster_AlarmRecord>();
                }
                return alarmRecords;
            }
        }

        public static RocMaster_AlarmRecordArray Null
        {
            get
            {
                return (new RocMaster_AlarmRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", AlarmRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_AlarmRecordArray AddAlarmRecord(RocMaster_AlarmRecord alarmRecord)
        {
            AlarmRecords.Add(alarmRecord);
            return this;
        }

        public static RocMaster_AlarmRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocAlarmRecords = new RocMaster_AlarmRecordArray();
            parsedRocAlarmRecords.alarmRecords = new List<RocMaster_AlarmRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocAlarmRecords.alarmRecords.Add(RocMaster_AlarmRecord.Parse(parsedString[i]));
            }

            return parsedRocAlarmRecords;
        }

        public RocMaster_AlarmRecord GetAlarmRecord(SqlInt32 index)
        {
            return AlarmRecords[index.Value];
        }

        public static RocMaster_AlarmRecordArray Empty()
        {
            var alarmRecordArray = new RocMaster_AlarmRecordArray { alarmRecords = new List<RocMaster_AlarmRecord>() };
            return alarmRecordArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            AlarmRecords.Clear();
            IsNull = binaryReader.ReadBoolean();

            if (IsNull)
            {
                return;
            }
            else
            {
                var length = binaryReader.ReadInt32();

                for (var i = 0; length > i; i++)
                {
                    var alarmRecord = new RocMaster_AlarmRecord();
                    alarmRecord.Read(binaryReader);
                    AlarmRecords.Add(alarmRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; AlarmRecords.Count > i; i++)
                {
                    AlarmRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
