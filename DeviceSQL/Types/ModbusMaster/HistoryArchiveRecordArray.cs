using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using System.Linq;
using System.IO;

namespace DeviceSQL.Types.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct MODBUSMaster_HistoryArchiveRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<MODBUSMaster_HistoryArchiveRecord> historyArchiveRecords;

        #endregion

        #region Properties

        internal MODBUSMaster_HistoryArchiveRecord this[int index]
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

        private List<MODBUSMaster_HistoryArchiveRecord> HistoryArchiveRecords
        {
            get
            {
                if (historyArchiveRecords == null)
                {
                    historyArchiveRecords = new List<MODBUSMaster_HistoryArchiveRecord>();
                }
                return historyArchiveRecords;
            }
        }

        public static MODBUSMaster_HistoryArchiveRecordArray Null
        {
            get
            {
                return (new MODBUSMaster_HistoryArchiveRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HistoryArchiveRecords.Select(historyArchiveRecord => historyArchiveRecord.ToString()));
        }

        public MODBUSMaster_HistoryArchiveRecordArray AddHistoryArchiveRecord(MODBUSMaster_HistoryArchiveRecord historyArchiveRecord)
        {
            HistoryArchiveRecords.Add(historyArchiveRecord);
            return this;
        }

        public static MODBUSMaster_HistoryArchiveRecordArray Parse(SqlString historyArchiveToParse)
        {
            if (historyArchiveToParse.IsNull)
            {
                return Null;
            }

            var parsedHistoryArchiveRecordArray = new MODBUSMaster_HistoryArchiveRecordArray()
            {
                historyArchiveRecords = new List<MODBUSMaster_HistoryArchiveRecord>()
            };

            var parsedHistoryArchive = historyArchiveToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedHistoryArchive.Length > i; i++)
            {
                parsedHistoryArchiveRecordArray.HistoryArchiveRecords.Add(MODBUSMaster_HistoryArchiveRecord.Parse(parsedHistoryArchive[i]));
            }

            return parsedHistoryArchiveRecordArray;
        }

        public MODBUSMaster_HistoryArchiveRecord GetHistoryArchiveRecord(SqlInt32 index)
        {
            return HistoryArchiveRecords[index.Value];
        }

        public static MODBUSMaster_HistoryArchiveRecordArray Empty()
        {
            var historyArchiveRecordArray = new MODBUSMaster_HistoryArchiveRecordArray() { historyArchiveRecords = new List<MODBUSMaster_HistoryArchiveRecord>() };
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
                    var historyArchiveRecord = new MODBUSMaster_HistoryArchiveRecord();
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