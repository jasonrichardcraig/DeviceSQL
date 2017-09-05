#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_ROCPlusHistoryRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCMaster_ROCPlusHistoryRecord> rocPlusHistoryRecords;

        #endregion

        #region Properties

        internal ROCMaster_ROCPlusHistoryRecord this[int index]
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

        private List<ROCMaster_ROCPlusHistoryRecord> ROCPlusHistoryRecords
        {
            get
            {
                if (rocPlusHistoryRecords == null)
                {
                    rocPlusHistoryRecords = new List<ROCMaster_ROCPlusHistoryRecord>();
                }
                return rocPlusHistoryRecords;
            }
        }

        public static ROCMaster_ROCPlusHistoryRecordArray Null
        {
            get
            {
                return (new ROCMaster_ROCPlusHistoryRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ROCPlusHistoryRecords.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_ROCPlusHistoryRecordArray AddROCPlusHistoryRecord(ROCMaster_ROCPlusHistoryRecord rocPlusHistoryRecord)
        {
            ROCPlusHistoryRecords.Add(rocPlusHistoryRecord);
            return this;
        }

        public static ROCMaster_ROCPlusHistoryRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusHistoryRecords = new ROCMaster_ROCPlusHistoryRecordArray();
            parsedROCPlusHistoryRecords.rocPlusHistoryRecords = new List<ROCMaster_ROCPlusHistoryRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusHistoryRecords.rocPlusHistoryRecords.Add(ROCMaster_ROCPlusHistoryRecord.Parse(parsedString[i]));
            }

            return parsedROCPlusHistoryRecords;
        }

        public ROCMaster_ROCPlusHistoryRecord GetROCPlusHistoryRecord(SqlInt32 index)
        {
            return ROCPlusHistoryRecords[index.Value];
        }

        public static ROCMaster_ROCPlusHistoryRecordArray Empty()
        {
            var rocPlusHistoryRecord = new ROCMaster_ROCPlusHistoryRecordArray { rocPlusHistoryRecords = new List<ROCMaster_ROCPlusHistoryRecord>() };
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
                    var rocPlusHistoryRecord = new ROCMaster_ROCPlusHistoryRecord();
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
