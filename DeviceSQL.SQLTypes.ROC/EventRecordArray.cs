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
    public struct EventRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<EventRecord> eventRecords;

        #endregion

        #region Properties

        internal EventRecord this[int index]
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

        private List<EventRecord> EventRecords
        {
            get
            {
                if (eventRecords == null)
                {
                    eventRecords = new List<EventRecord>();
                }
                return eventRecords;
            }
        }

        public static EventRecordArray Null
        {
            get
            {
                return (new EventRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", EventRecords.Select(parameter => parameter.ToString()));
        }

        public EventRecordArray AddEventRecord(EventRecord eventRecord)
        {
            EventRecords.Add(eventRecord);
            return this;
        }

        public static EventRecordArray Parse(SqlString stringToParse)
        {
            if (stringToParse.IsNull)
            {
                return Null;
            }

            var parsedROCEventRecords = new EventRecordArray();
            parsedROCEventRecords.eventRecords = new List<EventRecord>();
            var parsedString = stringToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedString.Length > i; i++)
            {
                parsedROCEventRecords.eventRecords.Add(EventRecord.Parse(parsedString[i]));
            }

            return parsedROCEventRecords;
        }

        public EventRecord GetEventRecord(SqlInt32 index)
        {
            return EventRecords[index.Value];
        }

        public static EventRecordArray Empty()
        {
            var eventRecordArray = new EventRecordArray { eventRecords = new List<EventRecord>() };
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
                    var eventRecord = new EventRecord();
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
