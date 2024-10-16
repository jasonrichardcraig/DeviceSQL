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
    public struct RocMaster_EventRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<RocMaster_EventRecord> eventRecords;

        #endregion

        #region Properties

        internal RocMaster_EventRecord this[int index]
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

        private List<RocMaster_EventRecord> EventRecords
        {
            get
            {
                if (eventRecords == null)
                {
                    eventRecords = new List<RocMaster_EventRecord>();
                }
                return eventRecords;
            }
        }

        public static RocMaster_EventRecordArray Null
        {
            get
            {
                return (new RocMaster_EventRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", EventRecords.Select(parameter => parameter.ToString()));
        }

        public RocMaster_EventRecordArray AddEventRecord(RocMaster_EventRecord eventRecord)
        {
            EventRecords.Add(eventRecord);
            return this;
        }

        public static RocMaster_EventRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedRocEventRecords = new RocMaster_EventRecordArray();
            parsedRocEventRecords.eventRecords = new List<RocMaster_EventRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedRocEventRecords.eventRecords.Add(RocMaster_EventRecord.Parse(parsedString[i]));
            }

            return parsedRocEventRecords;
        }

        public RocMaster_EventRecord GetEventRecord(SqlInt32 index)
        {
            return EventRecords[index.Value];
        }

        public static RocMaster_EventRecordArray Empty()
        {
            var eventRecordArray = new RocMaster_EventRecordArray { eventRecords = new List<RocMaster_EventRecord>() };
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
                    var eventRecord = new RocMaster_EventRecord();
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
