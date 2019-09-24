#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.Modbus
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct EventArchiveRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<EventArchiveRecord> eventArchiveRecords;

        #endregion

        #region Properties

        internal EventArchiveRecord this[int index]
        {
            get
            {
                return EventArchiveRecords[index];
            }
            set
            {
                EventArchiveRecords[index] = value;
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
                return EventArchiveRecords.Count;
            }
        }

        #endregion

        #region Helper Methods

        private List<EventArchiveRecord> EventArchiveRecords
        {
            get
            {
                if (eventArchiveRecords == null)
                {
                    eventArchiveRecords = new List<EventArchiveRecord>();
                }
                return eventArchiveRecords;
            }
        }

        public static EventArchiveRecordArray Null
        {
            get
            {
                return (new EventArchiveRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", EventArchiveRecords.Select(eventArchiveRecord => eventArchiveRecord.ToString()));
        }

        public EventArchiveRecordArray AddEventArchiveRecord(EventArchiveRecord eventArchiveRecord)
        {
            EventArchiveRecords.Add(eventArchiveRecord);
            return this;
        }

        public static EventArchiveRecordArray Parse(SqlString eventArchiveToParse)
        {
            if (eventArchiveToParse.IsNull)
            {
                return Null;
            }

            var parsedEventArchiveRecordArray = new EventArchiveRecordArray()
            {
                eventArchiveRecords = new List<EventArchiveRecord>()
            };

            var parsedEventArchive = eventArchiveToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedEventArchive.Length > i; i++)
            {
                parsedEventArchiveRecordArray.EventArchiveRecords.Add(EventArchiveRecord.Parse(parsedEventArchive[i]));
            }

            return parsedEventArchiveRecordArray;
        }

        public EventArchiveRecord GetEventArchiveRecord(SqlInt32 index)
        {
            return EventArchiveRecords[index.Value];
        }

        public static EventArchiveRecordArray Empty()
        {
            var eventArchiveRecordArray = new EventArchiveRecordArray() { eventArchiveRecords = new List<EventArchiveRecord>() };
            return eventArchiveRecordArray;
        }

        #endregion

        #region Serialization Methods

        public void Read(BinaryReader binaryReader)
        {
            EventArchiveRecords.Clear();
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
                    var eventArchiveRecord = new EventArchiveRecord();
                    eventArchiveRecord.Read(binaryReader);
                    EventArchiveRecords.Add(eventArchiveRecord);
                }
            }

        }

        public void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(IsNull);
            binaryWriter.Write(Length);

            if (Length > 0)
            {
                for (var i = 0; EventArchiveRecords.Count > i; i++)
                {
                    EventArchiveRecords[i].Write(binaryWriter);
                }
            }
        }

        #endregion

    }
}