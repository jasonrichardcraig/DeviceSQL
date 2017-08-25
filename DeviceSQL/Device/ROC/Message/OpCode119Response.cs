#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode119Response : ROCMessage, IROCResponseMessage
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

        public List<ROCPlusEventRecord> ROCPlusEvents
        {
            get
            {
                byte alarmCount = NumberOfEvents;
                ushort startIndex = StartingEventLogIndex;

                var ROCPlusEvents = new List<ROCPlusEventRecord>(alarmCount);

                for (ushort eventIndex = 5; eventIndex < Data.Length; eventIndex += 22)
                {
                    ushort relativeEventIndex = (ushort)(startIndex + ((eventIndex - 5) / 22));

                    var eventBytes = Data.ToList().GetRange(eventIndex, 22).ToArray();

                    ROCPlusEvents.Add(new ROCPlusEventRecord(relativeEventIndex, eventBytes));
                }

                return ROCPlusEvents;
            }
        }

        #endregion

        #region Helper Methods

        void IROCResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IROCResponseMessage.Initialize(byte[] frame, IROCRequestMessage requestMessage)
        {
            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
