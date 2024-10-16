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
    public struct RocMaster_HistoryRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_HistoryRecord> historyRecords;

        #endregion

        #region Properties

        internal RocMaster_HistoryRecord this[int index]
        {
            get
            {
                return HistoryRecords[index];
            }
            set
            {
                HistoryRecords[index] = value;
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
                return HistoryRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<RocMaster_HistoryRecord> HistoryRecords
        {
            get
            {
                if (historyRecords == null)
                {
                    historyRecords = new List<RocMaster_HistoryRecord>();
                }
                return historyRecords;
            }
        }

        public static RocMaster_HistoryRecordArray Null
        {
            get
            {
                return (new RocMaster_HistoryRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HistoryRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_HistoryRecordArray AddHistoryRecord(RocMaster_HistoryRecord historyRecord)
        {
            HistoryRecords.Add(historyRecord);
            return this;
        }

        public static RocMaster_HistoryRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocHistoryRecords = new RocMaster_HistoryRecordArray();
            parsedRocHistoryRecords.historyRecords = new List<RocMaster_HistoryRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocHistoryRecords.historyRecords.Add(RocMaster_HistoryRecord.Parse(parsedString[i]));
            }

            return parsedRocHistoryRecords;
        }

        public RocMaster_HistoryRecord GetHistoryRecord(SqlInt32 index)
        {
            return HistoryRecords[index.Value];
        }

        public static RocMaster_HistoryRecordArray Empty()
        {
            var historyRecord = new RocMaster_HistoryRecordArray { historyRecords = new List<RocMaster_HistoryRecord>() };
            return historyRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            HistoryRecords.Clear();
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
                    var historyRecord = new RocMaster_HistoryRecord();
                    historyRecord.Read(binaryReader);
                    HistoryRecords.Add(historyRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; HistoryRecords.Count > i; i++)
                {
                    HistoryRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
