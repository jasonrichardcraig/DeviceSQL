#region Imported Types

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.SQLTypes.MODBUSMaster
{
    [Serializable()]
    [SqlUserDefinedType(Format.UserDefined, IsByteOrdered = false, IsFixedLength = false, MaxByteSize = -1)]
    public struct MODBUSMaster_EventArchiveRecordArray : INullable, IBinarySerialize
    {

        #region Fields

        internal List<MODBUSMaster_EventArchiveRecord> eventArchiveRecords;

        #endregion

        #region Properties

        internal MODBUSMaster_EventArchiveRecord this[int index]
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

        private List<MODBUSMaster_EventArchiveRecord> EventArchiveRecords
        {
            get
            {
                if (eventArchiveRecords == null)
                {
                    eventArchiveRecords = new List<MODBUSMaster_EventArchiveRecord>();
                }
                return eventArchiveRecords;
            }
        }

        public static MODBUSMaster_EventArchiveRecordArray Null
        {
            get
            {
                return (new MODBUSMaster_EventArchiveRecordArray() { IsNull = true });
            }
        }

        public override string ToString()
        {
            return string.Join("|", EventArchiveRecords.Select(eventArchiveRecord => eventArchiveRecord.ToString()));
        }

        public MODBUSMaster_EventArchiveRecordArray AddEventArchiveRecord(MODBUSMaster_EventArchiveRecord eventArchiveRecord)
        {
            EventArchiveRecords.Add(eventArchiveRecord);
            return this;
        }

        public static MODBUSMaster_EventArchiveRecordArray Parse(SqlString eventArchiveToParse)
        {
            if (eventArchiveToParse.IsNull)
            {
                return Null;
            }

            var parsedEventArchiveRecordArray = new MODBUSMaster_EventArchiveRecordArray()
            {
                eventArchiveRecords = new List<MODBUSMaster_EventArchiveRecord>()
            };

            var parsedEventArchive = eventArchiveToParse.Value.Split("|".ToCharArray());

            for (var i = 0; parsedEventArchive.Length > i; i++)
            {
                parsedEventArchiveRecordArray.EventArchiveRecords.Add(MODBUSMaster_EventArchiveRecord.Parse(parsedEventArchive[i]));
            }

            return parsedEventArchiveRecordArray;
        }

        public MODBUSMaster_EventArchiveRecord GetEventArchiveRecord(SqlInt32 index)
        {
            return EventArchiveRecords[index.Value];
        }

        public static MODBUSMaster_EventArchiveRecordArray Empty()
        {
            var eventArchiveRecordArray = new MODBUSMaster_EventArchiveRecordArray() { eventArchiveRecords = new List<MODBUSMaster_EventArchiveRecord>() };
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
                    var eventArchiveRecord = new MODBUSMaster_EventArchiveRecord();
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