#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode136Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        private byte[] data;
        private byte startingHistoryPoint;
        private byte numberOfHistoryPoints;
        private byte numberOfTimePeriods;

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

        public byte HistorySegment
        {
            get
            {
                return Data[0];
            }
        }

        public ushort HistoryIndex
        {
            get
            {
                return BitConverter.ToUInt16(Data, 1);
            }
        }

        public ushort CurrentIndex
        {
            get
            {
                return BitConverter.ToUInt16(Data, 3);
            }
        }

        public byte NumberOfDataElements
        {
            get
            {
                return Data[5];
            }
        }

        public List<RocPlusHistoryRecord> MeterHistory
        {
            get
            {

                var recordCount = (numberOfTimePeriods * numberOfHistoryPoints);
                var recordIndex = HistoryIndex;

                var meterHistory = new List<RocPlusHistoryRecord>(recordCount);

                for (var timeStampOffset = 6; (timeStampOffset + (numberOfHistoryPoints * (4))) < Data.Length; timeStampOffset += (4 + (numberOfHistoryPoints * (4))))
                {
                    var dataOffset = timeStampOffset + 4;

                    for (var historyPoint = startingHistoryPoint; historyPoint < (startingHistoryPoint + numberOfHistoryPoints); historyPoint++)
                    {
                        meterHistory.Add(new RocPlusHistoryRecord() { HistorySegment = HistorySegment, HistoryPointNumber = historyPoint, Index = recordIndex, Value = new byte[] { Data[timeStampOffset], Data[timeStampOffset + 1], Data[timeStampOffset + 2], Data[timeStampOffset + 3], Data[dataOffset + 0], Data[dataOffset + 1], Data[dataOffset + 2], Data[dataOffset + 3] } });
                        dataOffset += 4;
                    }
                    recordIndex++;
                }

                return meterHistory;
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

            var opCode136RequestMessage = requestMessage as OpCode136Request;

            if (opCode136RequestMessage != null)
            {
                startingHistoryPoint = opCode136RequestMessage.StartingHistoryPoint;
                numberOfHistoryPoints = opCode136RequestMessage.NumberOfHistoryPoints;
                numberOfTimePeriods = opCode136RequestMessage.NumberOfTimePeriods;
            }

            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
