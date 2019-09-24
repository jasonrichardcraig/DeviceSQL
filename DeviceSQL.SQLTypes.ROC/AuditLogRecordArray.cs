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
    public struct AuditLogRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        public List<AuditLogRecord> auditLogRecords;

        #endregion

        #region Properties

        internal AuditLogRecord this[int index]
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

        private List<AuditLogRecord> AuditLogRecords
        {
            get
            {
                if (auditLogRecords == null)
                {
                    auditLogRecords = new List<AuditLogRecord>();
                }
                return auditLogRecords;
            }
        }

        public static AuditLogRecordArray Null
        {
            get
            {
                return (new AuditLogRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", AuditLogRecords.Select(parameter => parameter.ToString()));
        }

        public AuditLogRecordArray AddAuditLogRecord(AuditLogRecord auditLogRecord)
        {
            AuditLogRecords.Add(auditLogRecord);
            return this;
        }

        public static AuditLogRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCAuditLogRecords = new AuditLogRecordArray();
            parsedROCAuditLogRecords.auditLogRecords = new List<AuditLogRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCAuditLogRecords.auditLogRecords.Add(AuditLogRecord.Parse(parsedString[i]));
            }

            return parsedROCAuditLogRecords;
        }

        public AuditLogRecord GetAuditLogRecord(SqlInt32 index)
        {
            return AuditLogRecords[index.Value];
        }

        public static AuditLogRecordArray Empty()
        {
            var auditLogRecordArray = new AuditLogRecordArray { auditLogRecords = new List<AuditLogRecord>() };
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
                    var auditLogRecord = new AuditLogRecord();
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
