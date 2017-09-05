using DeviceSQL.Device.ROC.Data;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_ROCPlusAlarmRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCMaster_ROCPlusAlarmRecord> rocPlusAlarmRecords;

        #endregion

        #region Properties

        internal ROCMaster_ROCPlusAlarmRecord this[int index]
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

        private List<ROCMaster_ROCPlusAlarmRecord> ROCPlusAlarmRecords
        {
            get
            {
                if (rocPlusAlarmRecords == null)
                {
                    rocPlusAlarmRecords = new List<ROCMaster_ROCPlusAlarmRecord>();
                }
                return rocPlusAlarmRecords;
            }
        }

        public static ROCMaster_ROCPlusAlarmRecordArray Null
        {
            get
            {
                return (new ROCMaster_ROCPlusAlarmRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", ROCPlusAlarmRecords.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_ROCPlusAlarmRecordArray AddROCPlusAlarmRecord(ROCMaster_ROCPlusAlarmRecord rocPlusAlarmRecord)
        {
            ROCPlusAlarmRecords.Add(rocPlusAlarmRecord);
            return this;
        }

        public static ROCMaster_ROCPlusAlarmRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCPlusAlarmRecords = new ROCMaster_ROCPlusAlarmRecordArray();
            parsedROCPlusAlarmRecords.rocPlusAlarmRecords = new List<ROCMaster_ROCPlusAlarmRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCPlusAlarmRecords.rocPlusAlarmRecords.Add(ROCMaster_ROCPlusAlarmRecord.Parse(parsedString[i]));
            }

            return parsedROCPlusAlarmRecords;
        }

        public ROCMaster_ROCPlusAlarmRecord GetROCPlusAlarmRecord(SqlInt32 index)
        {
            return ROCPlusAlarmRecords[index.Value];
        }

        public static ROCMaster_ROCPlusAlarmRecordArray Empty()
        {
            var rocPlusAlarmRecord = new ROCMaster_ROCPlusAlarmRecordArray { rocPlusAlarmRecords = new List<ROCMaster_ROCPlusAlarmRecord>() };
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
                    var rocPLusAlarmRecord = new ROCMaster_ROCPlusAlarmRecord();
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
