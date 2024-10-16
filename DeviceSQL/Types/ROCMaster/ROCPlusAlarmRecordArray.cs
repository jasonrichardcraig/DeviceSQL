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
    public struct RocMaster_RocPlusAlarmRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_RocPlusAlarmRecord> rocPlusAlarmRecords;

        #endregion

        #region Properties

        internal RocMaster_RocPlusAlarmRecord this[int index]
        {
            get
            {
                return RocPlusAlarmRecords[index];
            }
            set
            {
                RocPlusAlarmRecords[index] = value;
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
                return RocPlusAlarmRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<RocMaster_RocPlusAlarmRecord> RocPlusAlarmRecords
        {
            get
            {
                if (rocPlusAlarmRecords == null)
                {
                    rocPlusAlarmRecords = new List<RocMaster_RocPlusAlarmRecord>();
                }
                return rocPlusAlarmRecords;
            }
        }

        public static RocMaster_RocPlusAlarmRecordArray Null
        {
            get
            {
                return (new RocMaster_RocPlusAlarmRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", RocPlusAlarmRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_RocPlusAlarmRecordArray AddRocPlusAlarmRecord(RocMaster_RocPlusAlarmRecord rocPlusAlarmRecord)
        {
            RocPlusAlarmRecords.Add(rocPlusAlarmRecord);
            return this;
        }

        public static RocMaster_RocPlusAlarmRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocPlusAlarmRecords = new RocMaster_RocPlusAlarmRecordArray();
            parsedRocPlusAlarmRecords.rocPlusAlarmRecords = new List<RocMaster_RocPlusAlarmRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocPlusAlarmRecords.rocPlusAlarmRecords.Add(RocMaster_RocPlusAlarmRecord.Parse(parsedString[i]));
            }

            return parsedRocPlusAlarmRecords;
        }

        public RocMaster_RocPlusAlarmRecord GetRocPlusAlarmRecord(SqlInt32 index)
        {
            return RocPlusAlarmRecords[index.Value];
        }

        public static RocMaster_RocPlusAlarmRecordArray Empty()
        {
            var rocPlusAlarmRecord = new RocMaster_RocPlusAlarmRecordArray { rocPlusAlarmRecords = new List<RocMaster_RocPlusAlarmRecord>() };
            return rocPlusAlarmRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            RocPlusAlarmRecords.Clear();
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
                    var rocPLusAlarmRecord = new RocMaster_RocPlusAlarmRecord();
                    rocPLusAlarmRecord.Read(binaryReader);
                    RocPlusAlarmRecords.Add(rocPLusAlarmRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; RocPlusAlarmRecords.Count > i; i++)
                {
                    RocPlusAlarmRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
