#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode126Response : ROCMessage, IROCResponseMessage
    {

        #region Fields

        private byte[] data;
        private byte historyPointNumber;

        #endregion

        #region Properties

        public byte CurrentMinute
        {
            get
            {
                return data[1];
            }
        }

        public byte HistoryPointNumber
        {
            get
            {
                return historyPointNumber;
            }
        }

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

        public List<HistoryRecord> MeterHistory
        {
            get
            {
                var meterHistory = new List<HistoryRecord>(60);

                for (int minuteIndex = 0; minuteIndex < 60; minuteIndex++)
                {
                    var dataOffset = 2 + (minuteIndex * 4);
                    meterHistory.Add(new HistoryRecord() { HistoryPointNumber = historyPointNumber, Index = minuteIndex, HistorySegment = 0, Value = new byte[] { data[dataOffset], data[dataOffset + 1], data[dataOffset + 2], data[dataOffset + 3] } });
                }

                return meterHistory;
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

            var opCode126RequestMessage = requestMessage as OpCode126Request;

            if (opCode126RequestMessage != null)
            {
                historyPointNumber = opCode126RequestMessage.HistoryPointNumber;
            }

            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
