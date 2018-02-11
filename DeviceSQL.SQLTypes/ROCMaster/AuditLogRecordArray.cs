#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_AuditLogRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCMaster_AuditLogRecord> auditLogRecords;

        #endregion

        #region Properties

        internal ROCMaster_AuditLogRecord this[int index]
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

        private List<ROCMaster_AuditLogRecord> AuditLogRecords
        {
            get
            {
                if (auditLogRecords == null)
                {
                    auditLogRecords = new List<ROCMaster_AuditLogRecord>();
                }
                return auditLogRecords;
            }
        }

        public static ROCMaster_AuditLogRecordArray Null
        {
            get
            {
                return (new ROCMaster_AuditLogRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", AuditLogRecords.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_AuditLogRecordArray AddAuditLogRecord(ROCMaster_AuditLogRecord auditLogRecord)
        {
            AuditLogRecords.Add(auditLogRecord);
            return this;
        }

        public static ROCMaster_AuditLogRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCAuditLogRecords = new ROCMaster_AuditLogRecordArray();
            parsedROCAuditLogRecords.auditLogRecords = new List<ROCMaster_AuditLogRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCAuditLogRecords.auditLogRecords.Add(ROCMaster_AuditLogRecord.Parse(parsedString[i]));
            }

            return parsedROCAuditLogRecords;
        }

        public ROCMaster_AuditLogRecord GetAuditLogRecord(SqlInt32 index)
        {
            return AuditLogRecords[index.Value];
        }

        public static ROCMaster_AuditLogRecordArray Empty()
        {
            var auditLogRecordArray = new ROCMaster_AuditLogRecordArray { auditLogRecords = new List<ROCMaster_AuditLogRecord>() };
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
                    var auditLogRecord = new ROCMaster_AuditLogRecord();
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
