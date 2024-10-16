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
    public struct RocMaster_RocPlusEventRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_RocPlusEventRecord> rocPlusEventRecords;

        #endregion

        #region Properties

        internal RocMaster_RocPlusEventRecord this[int index]
        {
            get
            {
                return RocPlusEventRecords[index];
            }
            set
            {
                RocPlusEventRecords[index] = value;
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
                return RocPlusEventRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<RocMaster_RocPlusEventRecord> RocPlusEventRecords
        {
            get
            {
                if (rocPlusEventRecords == null)
                {
                    rocPlusEventRecords = new List<RocMaster_RocPlusEventRecord>();
                }
                return rocPlusEventRecords;
            }
        }

        public static RocMaster_RocPlusEventRecordArray Null
        {
            get
            {
                return (new RocMaster_RocPlusEventRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", RocPlusEventRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_RocPlusEventRecordArray AddRocPlusEventRecord(RocMaster_RocPlusEventRecord rocPlusEventRecord)
        {
            RocPlusEventRecords.Add(rocPlusEventRecord);
            return this;
        }

        public static RocMaster_RocPlusEventRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocPlusEventRecords = new RocMaster_RocPlusEventRecordArray();
            parsedRocPlusEventRecords.rocPlusEventRecords = new List<RocMaster_RocPlusEventRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocPlusEventRecords.rocPlusEventRecords.Add(RocMaster_RocPlusEventRecord.Parse(parsedString[i]));
            }

            return parsedRocPlusEventRecords;
        }

        public RocMaster_RocPlusEventRecord GetRocPlusEventRecord(SqlInt32 index)
        {
            return RocPlusEventRecords[index.Value];
        }

        public static RocMaster_EventRecordArray Empty()
        {
            var eventRecord = new RocMaster_EventRecordArray { eventRecords = new List<RocMaster_EventRecord>() };
            return eventRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            RocPlusEventRecords.Clear();
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
                    var rocPlusEventRecord = new RocMaster_RocPlusEventRecord();
                    rocPlusEventRecord.Read(binaryReader);
                    RocPlusEventRecords.Add(rocPlusEventRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; RocPlusEventRecords.Count > i; i++)
                {
                    RocPlusEventRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
