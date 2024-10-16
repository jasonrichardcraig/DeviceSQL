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
    public struct RocMaster_RocPlusHistoryRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_RocPlusHistoryRecord> rocPlusHistoryRecords;

        #endregion

        #region Properties

        internal RocMaster_RocPlusHistoryRecord this[int index]
        {
            get
            {
                return RocPlusHistoryRecords[index];
            }
            set
            {
                RocPlusHistoryRecords[index] = value;
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
                return RocPlusHistoryRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<RocMaster_RocPlusHistoryRecord> RocPlusHistoryRecords
        {
            get
            {
                if (rocPlusHistoryRecords == null)
                {
                    rocPlusHistoryRecords = new List<RocMaster_RocPlusHistoryRecord>();
                }
                return rocPlusHistoryRecords;
            }
        }

        public static RocMaster_RocPlusHistoryRecordArray Null
        {
            get
            {
                return (new RocMaster_RocPlusHistoryRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", RocPlusHistoryRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_RocPlusHistoryRecordArray AddRocPlusHistoryRecord(RocMaster_RocPlusHistoryRecord rocPlusHistoryRecord)
        {
            RocPlusHistoryRecords.Add(rocPlusHistoryRecord);
            return this;
        }

        public static RocMaster_RocPlusHistoryRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocPlusHistoryRecords = new RocMaster_RocPlusHistoryRecordArray();
            parsedRocPlusHistoryRecords.rocPlusHistoryRecords = new List<RocMaster_RocPlusHistoryRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocPlusHistoryRecords.rocPlusHistoryRecords.Add(RocMaster_RocPlusHistoryRecord.Parse(parsedString[i]));
            }

            return parsedRocPlusHistoryRecords;
        }

        public RocMaster_RocPlusHistoryRecord GetRocPlusHistoryRecord(SqlInt32 index)
        {
            return RocPlusHistoryRecords[index.Value];
        }

        public static RocMaster_RocPlusHistoryRecordArray Empty()
        {
            var rocPlusHistoryRecord = new RocMaster_RocPlusHistoryRecordArray { rocPlusHistoryRecords = new List<RocMaster_RocPlusHistoryRecord>() };
            return rocPlusHistoryRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            RocPlusHistoryRecords.Clear();
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
                    var rocPlusHistoryRecord = new RocMaster_RocPlusHistoryRecord();
                    rocPlusHistoryRecord.Read(binaryReader);
                    RocPlusHistoryRecords.Add(rocPlusHistoryRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; RocPlusHistoryRecords.Count > i; i++)
                {
                    RocPlusHistoryRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
