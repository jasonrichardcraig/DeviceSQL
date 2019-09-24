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
    public struct HistoryRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<HistoryRecord> historyRecords;

        #endregion

        #region Properties

        internal HistoryRecord this[int index]
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

        private List<HistoryRecord> HistoryRecords
        {
            get
            {
                if (historyRecords == null)
                {
                    historyRecords = new List<HistoryRecord>();
                }
                return historyRecords;
            }
        }

        public static HistoryRecordArray Null
        {
            get
            {
                return (new HistoryRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HistoryRecords.Select(parameter => parameter.ToString()));
        }

        public HistoryRecordArray AddHistoryRecord(HistoryRecord historyRecord)
        {
            HistoryRecords.Add(historyRecord);
            return this;
        }

        public static HistoryRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCHistoryRecords = new HistoryRecordArray();
            parsedROCHistoryRecords.historyRecords = new List<HistoryRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCHistoryRecords.historyRecords.Add(HistoryRecord.Parse(parsedString[i]));
            }

            return parsedROCHistoryRecords;
        }

        public HistoryRecord GetHistoryRecord(SqlInt32 index)
        {
            return HistoryRecords[index.Value];
        }

        public static HistoryRecordArray Empty()
        {
            var historyRecord = new HistoryRecordArray { historyRecords = new List<HistoryRecord>() };
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
                    var historyRecord = new HistoryRecord();
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
