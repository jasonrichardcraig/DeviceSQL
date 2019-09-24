#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.Modbus
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct HistoryArchiveRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<HistoryArchiveRecord> historyArchiveRecords;

        #endregion

        #region Properties

        internal HistoryArchiveRecord this[int index]
        {
            get
            {
                return HistoryArchiveRecords[index];
            }
            set
            {
                HistoryArchiveRecords[index] = value;
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
                return HistoryArchiveRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<HistoryArchiveRecord> HistoryArchiveRecords
        {
            get
            {
                if (historyArchiveRecords == null)
                {
                    historyArchiveRecords = new List<HistoryArchiveRecord>();
                }
                return historyArchiveRecords;
            }
        }

        public static HistoryArchiveRecordArray Null
        {
            get
            {
                return (new HistoryArchiveRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HistoryArchiveRecords.Select(historyArchiveRecord => historyArchiveRecord.ToString()));
        }

        public HistoryArchiveRecordArray AddHistoryArchiveRecord(HistoryArchiveRecord historyArchiveRecord)
        {
            HistoryArchiveRecords.Add(historyArchiveRecord);
            return this;
        }

        public static HistoryArchiveRecordArray Parse(SqlString historyArchiveToParse)
        {
            if (historyArchiveToParse.IsNull)
            {
                return Null;
            }

            var parsedHistoryArchiveRecordArray = new HistoryArchiveRecordArray()
            {
                historyArchiveRecords = new List<HistoryArchiveRecord>()
            };

            var parsedHistoryArchive = historyArchiveToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedHistoryArchive.Length > i; i++)
            {
                parsedHistoryArchiveRecordArray.HistoryArchiveRecords.Add(HistoryArchiveRecord.Parse(parsedHistoryArchive[i]));
            }

            return parsedHistoryArchiveRecordArray;
        }

        public HistoryArchiveRecord GetHistoryArchiveRecord(SqlInt32 index)
        {
            return HistoryArchiveRecords[index.Value];
        }

        public static HistoryArchiveRecordArray Empty()
        {
            var historyArchiveRecordArray = new HistoryArchiveRecordArray() { historyArchiveRecords = new List<HistoryArchiveRecord>() };
            return historyArchiveRecordArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            HistoryArchiveRecords.Clear();
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
                    var historyArchiveRecord = new HistoryArchiveRecord();
                    historyArchiveRecord.Read(binaryReader);
                    HistoryArchiveRecords.Add(historyArchiveRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; HistoryArchiveRecords.Count > i; i++)
                {
                    HistoryArchiveRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}