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
    public struct ROCPlusAlarmRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCPlusAlarmRecord> rocPlusAlarmRecords;

        #endregion

        #region Properties

        internal ROCPlusAlarmRecord this[int index]
        {
            get
            {
                return ROCPlusAlarmRecords[index];
            }
            set
            {
                ROCPlusAlarmRecords[index] = value;
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
                return ROCPlusAlarmRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<ROCPlusAlarmRecord> ROCPlusAlarmRecords
        {
            get
            {
                if (rocPlusAlarmRecords == null)
                {
                    rocPlusAlarmRecords = new List<ROCPlusAlarmRecord>();
                }
                return rocPlusAlarmRecords;
            }
        }

        public static ROCPlusAlarmRecordArray Null
        {
            get
            {
                return (new ROCPlusAlarmRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ROCPlusAlarmRecords.Select(parameter => parameter.ToString()));
        }

        public ROCPlusAlarmRecordArray AddROCPlusAlarmRecord(ROCPlusAlarmRecord rocPlusAlarmRecord)
        {
            ROCPlusAlarmRecords.Add(rocPlusAlarmRecord);
            return this;
        }

        public static ROCPlusAlarmRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusAlarmRecords = new ROCPlusAlarmRecordArray();
            parsedROCPlusAlarmRecords.rocPlusAlarmRecords = new List<ROCPlusAlarmRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusAlarmRecords.rocPlusAlarmRecords.Add(ROCPlusAlarmRecord.Parse(parsedString[i]));
            }

            return parsedROCPlusAlarmRecords;
        }

        public ROCPlusAlarmRecord GetROCPlusAlarmRecord(SqlInt32 index)
        {
            return ROCPlusAlarmRecords[index.Value];
        }

        public static ROCPlusAlarmRecordArray Empty()
        {
            var rocPlusAlarmRecord = new ROCPlusAlarmRecordArray { rocPlusAlarmRecords = new List<ROCPlusAlarmRecord>() };
            return rocPlusAlarmRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            ROCPlusAlarmRecords.Clear();
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
                    var rocPLusAlarmRecord = new ROCPlusAlarmRecord();
                    rocPLusAlarmRecord.Read(binaryReader);
                    ROCPlusAlarmRecords.Add(rocPLusAlarmRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; ROCPlusAlarmRecords.Count > i; i++)
                {
                    ROCPlusAlarmRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
