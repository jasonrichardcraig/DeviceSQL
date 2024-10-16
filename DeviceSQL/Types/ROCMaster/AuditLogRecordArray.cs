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
    public struct RocMaster_AuditLogRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_AuditLogRecord> auditLogRecords;

        #endregion

        #region Properties

        internal RocMaster_AuditLogRecord this[int index]
        {
            get
            {
                return AuditLogRecords[index];
            }
            set
            {
                AuditLogRecords[index] = value;
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
                return AuditLogRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<RocMaster_AuditLogRecord> AuditLogRecords
        {
            get
            {
                if (auditLogRecords == null)
                {
                    auditLogRecords = new List<RocMaster_AuditLogRecord>();
                }
                return auditLogRecords;
            }
        }

        public static RocMaster_AuditLogRecordArray Null
        {
            get
            {
                return (new RocMaster_AuditLogRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", AuditLogRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_AuditLogRecordArray AddAuditLogRecord(RocMaster_AuditLogRecord auditLogRecord)
        {
            AuditLogRecords.Add(auditLogRecord);
            return this;
        }

        public static RocMaster_AuditLogRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocAuditLogRecords = new RocMaster_AuditLogRecordArray();
            parsedRocAuditLogRecords.auditLogRecords = new List<RocMaster_AuditLogRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocAuditLogRecords.auditLogRecords.Add(RocMaster_AuditLogRecord.Parse(parsedString[i]));
            }

            return parsedRocAuditLogRecords;
        }

        public RocMaster_AuditLogRecord GetAuditLogRecord(SqlInt32 index)
        {
            return AuditLogRecords[index.Value];
        }

        public static RocMaster_AuditLogRecordArray Empty()
        {
            var auditLogRecordArray = new RocMaster_AuditLogRecordArray { auditLogRecords = new List<RocMaster_AuditLogRecord>() };
            return auditLogRecordArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            AuditLogRecords.Clear();
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
                    var auditLogRecord = new RocMaster_AuditLogRecord();
                    auditLogRecord.Read(binaryReader);
                    AuditLogRecords.Add(auditLogRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; AuditLogRecords.Count > i; i++)
                {
                    AuditLogRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
