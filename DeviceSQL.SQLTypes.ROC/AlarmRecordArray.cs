#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.ROC
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct AlarmRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        public List<AlarmRecord> alarmRecords;

        #endregion

        #region Properties

        internal AlarmRecord this[int index]
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

        private List<AlarmRecord> AlarmRecords
        {
            get
            {
                if (alarmRecords == null)
                {
                    alarmRecords = new List<AlarmRecord>();
                }
                return alarmRecords;
            }
        }

        public static AlarmRecordArray Null
        {
            get
            {
                return (new AlarmRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", AlarmRecords.Select(parameter => parameter.ToString()));
        }

        public AlarmRecordArray AddAlarmRecord(AlarmRecord alarmRecord)
        {
            AlarmRecords.Add(alarmRecord);
            return this;
        }

        public static AlarmRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCAlarmRecords = new AlarmRecordArray();
            parsedROCAlarmRecords.alarmRecords = new List<AlarmRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCAlarmRecords.alarmRecords.Add(AlarmRecord.Parse(parsedString[i]));
            }

            return parsedROCAlarmRecords;
        }

        public AlarmRecord GetAlarmRecord(SqlInt32 index)
        {
            return AlarmRecords[index.Value];
        }

        public static AlarmRecordArray Empty()
        {
            var alarmRecordArray = new AlarmRecordArray { alarmRecords = new List<AlarmRecord>() };
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
                    var alarmRecord = new AlarmRecord();
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
