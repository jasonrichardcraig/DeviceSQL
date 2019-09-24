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
    public struct ROCPlusEventRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCPlusEventRecord> rocPlusEventRecords;

        #endregion

        #region Properties

        internal ROCPlusEventRecord this[int index]
        {
            get
            {
                return ROCPlusEventRecords[index];
            }
            set
            {
                ROCPlusEventRecords[index] = value;
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
                return ROCPlusEventRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<ROCPlusEventRecord> ROCPlusEventRecords
        {
            get
            {
                if (rocPlusEventRecords == null)
                {
                    rocPlusEventRecords = new List<ROCPlusEventRecord>();
                }
                return rocPlusEventRecords;
            }
        }

        public static ROCPlusEventRecordArray Null
        {
            get
            {
                return (new ROCPlusEventRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ROCPlusEventRecords.Select(parameter => parameter.ToString()));
        }

        public ROCPlusEventRecordArray AddROCPlusEventRecord(ROCPlusEventRecord rocPlusEventRecord)
        {
            ROCPlusEventRecords.Add(rocPlusEventRecord);
            return this;
        }

        public static ROCPlusEventRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusEventRecords = new ROCPlusEventRecordArray();
            parsedROCPlusEventRecords.rocPlusEventRecords = new List<ROCPlusEventRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusEventRecords.rocPlusEventRecords.Add(ROCPlusEventRecord.Parse(parsedString[i]));
            }

            return parsedROCPlusEventRecords;
        }

        public ROCPlusEventRecord GetROCPlusEventRecord(SqlInt32 index)
        {
            return ROCPlusEventRecords[index.Value];
        }

        public static EventRecordArray Empty()
        {
            var eventRecord = new EventRecordArray { eventRecords = new List<EventRecord>() };
            return eventRecord;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            ROCPlusEventRecords.Clear();
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
                    var rocPlusEventRecord = new ROCPlusEventRecord();
                    rocPlusEventRecord.Read(binaryReader);
                    ROCPlusEventRecords.Add(rocPlusEventRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; ROCPlusEventRecords.Count > i; i++)
                {
                    ROCPlusEventRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
