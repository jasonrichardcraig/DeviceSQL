using DeviceSQL.Device.ROC.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_HistoryRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCMaster_HistoryRecord> historyRecords;

        #endregion

        #region Properties

        internal ROCMaster_HistoryRecord this[int index]
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

        private List<ROCMaster_HistoryRecord> HistoryRecords
        {
            get
            {
                if (historyRecords == null)
                {
                    historyRecords = new List<ROCMaster_HistoryRecord>();
                }
                return historyRecords;
            }
        }

        public static ROCMaster_HistoryRecordArray Null
        {
            get
            {
                return (new ROCMaster_HistoryRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", HistoryRecords.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_HistoryRecordArray AddHistoryRecord(ROCMaster_HistoryRecord historyRecord)
        {
            HistoryRecords.Add(historyRecord);
            return this;
        }

        public static ROCMaster_HistoryRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCHistoryRecords = new ROCMaster_HistoryRecordArray();
            parsedROCHistoryRecords.historyRecords = new List<ROCMaster_HistoryRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCHistoryRecords.historyRecords.Add(ROCMaster_HistoryRecord.Parse(parsedString[i]));
            }

            return parsedROCHistoryRecords;
        }

        public ROCMaster_HistoryRecord GetHistoryRecord(SqlInt32 index)
        {
            return HistoryRecords[index.Value];
        }

        public static ROCMaster_HistoryRecordArray Empty()
        {
            var historyRecord = new ROCMaster_HistoryRecordArray { historyRecords = new List<ROCMaster_HistoryRecord>() };
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
                    var historyRecord = new ROCMaster_HistoryRecord();
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
