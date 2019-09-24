﻿#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode122Response : ROCMessage, IROCResponseMessage
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

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public List<EventRecord> MeterEvents
        {
            get
            {
                byte eventCount = Data[0];
                ushort startIndex = BitConverter.ToUInt16(Data, 1);

                var meterEvents = new List<EventRecord>(eventCount);

                for (ushort eventIndex = 5; eventIndex < Data.Length; eventIndex += 22)
                {
                    ushort relativeEventIndex = (ushort)(startIndex + ((eventIndex - 5) / 22));

                    var eventBytes = Data.ToList().GetRange(eventIndex, 22).ToArray();

                    meterEvents.Add(new EventRecord(relativeEventIndex, eventBytes));
                }

                return meterEvents;
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
