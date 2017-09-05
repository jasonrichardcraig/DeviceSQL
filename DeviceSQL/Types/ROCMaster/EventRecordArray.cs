#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Types.ROCMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct ROCMaster_EventRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<ROCMaster_EventRecord> eventRecords;

        #endregion

        #region Properties

        internal ROCMaster_EventRecord this[int index]
        {
            get
            {
                return EventRecords[index];
            }
            set
            {
                EventRecords[index] = value;
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
                return EventRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<ROCMaster_EventRecord> EventRecords
        {
            get
            {
                if (eventRecords == null)
                {
                    eventRecords = new List<ROCMaster_EventRecord>();
                }
                return eventRecords;
            }
        }

        public static ROCMaster_EventRecordArray Null
        {
            get
            {
                return (new ROCMaster_EventRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", EventRecords.Select(parameter => parameter.ToString()));
        }

        public ROCMaster_EventRecordArray AddEventRecord(ROCMaster_EventRecord eventRecord)
        {
            EventRecords.Add(eventRecord);
            return this;
        }

        public static ROCMaster_EventRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCEventRecords = new ROCMaster_EventRecordArray();
            parsedROCEventRecords.eventRecords = new List<ROCMaster_EventRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCEventRecords.eventRecords.Add(ROCMaster_EventRecord.Parse(parsedString[i]));
            }

            return parsedROCEventRecords;
        }

        public ROCMaster_EventRecord GetEventRecord(SqlInt32 index)
        {
            return EventRecords[index.Value];
        }

        public static ROCMaster_EventRecordArray Empty()
        {
            var eventRecordArray = new ROCMaster_EventRecordArray { eventRecords = new List<ROCMaster_EventRecord>() };
            return eventRecordArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            EventRecords.Clear();
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
                    var eventRecord = new ROCMaster_EventRecord();
                    eventRecord.Read(binaryReader);
                    EventRecords.Add(eventRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; EventRecords.Count > i; i++)
                {
                    EventRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}
