#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode119Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return 10;
            }
        }

        public byte NumberOfEvents
        {
            get
            {
                return data[0];
            }
        }

        public ushort StartingEventLogIndex
        {
            get
            {
                return BitConverter.ToUInt16(data, 1);
            }
        }

        public ushort CurrentEventLogIndex
        {
            get
            {
                return BitConverter.ToUInt16(data, 3);
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public List<RocPlusEventRecord> RocPlusEvents
        {
            get
            {
                byte alarmCount = NumberOfEvents;
                ushort startIndex = StartingEventLogIndex;

                var RocPlusEvents = new List<RocPlusEventRecord>(alarmCount);

                for (ushort eventIndex = 5; eventIndex < Data.Length; eventIndex += 22)
                {
                    ushort relativeEventIndex = (ushort)(startIndex + ((eventIndex - 5) / 22));

                    var eventBytes = Data.ToList().GetRange(eventIndex, 22).ToArray();

                    RocPlusEvents.Add(new RocPlusEventRecord(relativeEventIndex, eventBytes));
                }

                return RocPlusEvents;
            }
        }

        #endregion

        #region Helper Methods

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {
            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
