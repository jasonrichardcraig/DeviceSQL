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
    public struct ROCMaster_ROCPlusEventRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCMaster_ROCPlusEventRecord> rocPlusEventRecords;

        #endregion

        #region Properties

        internal ROCMaster_ROCPlusEventRecord this[int index]
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

        private List<ROCMaster_ROCPlusEventRecord> ROCPlusEventRecords
        {
            get
            {
                if (rocPlusEventRecords == null)
                {
                    rocPlusEventRecords = new List<ROCMaster_ROCPlusEventRecord>();
                }
                return rocPlusEventRecords;
            }
        }

        public static ROCMaster_ROCPlusEventRecordArray Null
        {
            get
            {
                return (new ROCMaster_ROCPlusEventRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ROCPlusEventRecords.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_ROCPlusEventRecordArray AddROCPlusEventRecord(ROCMaster_ROCPlusEventRecord rocPlusEventRecord)
        {
            ROCPlusEventRecords.Add(rocPlusEventRecord);
            return this;
        }

        public static ROCMaster_ROCPlusEventRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusEventRecords = new ROCMaster_ROCPlusEventRecordArray();
            parsedROCPlusEventRecords.rocPlusEventRecords = new List<ROCMaster_ROCPlusEventRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusEventRecords.rocPlusEventRecords.Add(ROCMaster_ROCPlusEventRecord.Parse(parsedString[i]));
            }

            return parsedROCPlusEventRecords;
        }

        public ROCMaster_ROCPlusEventRecord GetROCPlusEventRecord(SqlInt32 index)
        {
            return ROCPlusEventRecords[index.Value];
        }

        public static ROCMaster_EventRecordArray Empty()
        {
            var eventRecord = new ROCMaster_EventRecordArray { eventRecords = new List<ROCMaster_EventRecord>() };
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
                    var rocPlusEventRecord = new ROCMaster_ROCPlusEventRecord();
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
