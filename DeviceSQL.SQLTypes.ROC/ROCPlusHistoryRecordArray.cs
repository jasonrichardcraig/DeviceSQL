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
    public struct ROCPlusHistoryRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCPlusHistoryRecord> rocPlusHistoryRecords;

        #endregion

        #region Properties

        internal ROCPlusHistoryRecord this[int index]
        {
            get
            {
                return ROCPlusHistoryRecords[index];
            }
            set
            {
                ROCPlusHistoryRecords[index] = value;
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
                return ROCPlusHistoryRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<ROCPlusHistoryRecord> ROCPlusHistoryRecords
        {
            get
            {
                if (rocPlusHistoryRecords == null)
                {
                    rocPlusHistoryRecords = new List<ROCPlusHistoryRecord>();
                }
                return rocPlusHistoryRecords;
            }
        }

        public static ROCPlusHistoryRecordArray Null
        {
            get
            {
                return (new ROCPlusHistoryRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ROCPlusHistoryRecords.Select(parameter => parameter.ToString()));
        }

        public ROCPlusHistoryRecordArray AddROCPlusHistoryRecord(ROCPlusHistoryRecord rocPlusHistoryRecord)
        {
            ROCPlusHistoryRecords.Add(rocPlusHistoryRecord);
            return this;
        }

        public static ROCPlusHistoryRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusHistoryRecords = new ROCPlusHistoryRecordArray();
            parsedROCPlusHistoryRecords.rocPlusHistoryRecords = new List<ROCPlusHistoryRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusHistoryRecords.rocPlusHistoryRecords.Add(ROCPlusHistoryRecord.Parse(parsedString[i]));
            }

            return parsedROCPlusHistoryRecords;
        }

        public ROCPlusHistoryRecord GetROCPlusHistoryRecord(SqlInt32 index)
        {
            return ROCPlusHistoryRecords[index.Value];
        }

        public static ROCPlusHistoryRecordArray Empty()
        {
            var rocPlusHistoryRecord = new ROCPlusHistoryRecordArray { rocPlusHistoryRecords = new List<ROCPlusHistoryRecord>() };
            return rocPlusHistoryRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            ROCPlusHistoryRecords.Clear();
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
                    var rocPlusHistoryRecord = new ROCPlusHistoryRecord();
                    rocPlusHistoryRecord.Read(binaryReader);
                    ROCPlusHistoryRecords.Add(rocPlusHistoryRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; ROCPlusHistoryRecords.Count > i; i++)
                {
                    ROCPlusHistoryRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
