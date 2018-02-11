#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode130Response : ROCMessage, IROCResponseMessage
    {

        #region Fields

        private byte[] data;
        private ushort startIndex;

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

        public List<HistoryRecord> MeterHistory
        {
            get
            {
                byte historicalRamArea = Data[0];
                byte historyPointNumber = Data[1];
                byte historyRecordCount = Data[2];

                var meterHistory = new List<HistoryRecord>(historyRecordCount);

                for (ushort historyIndex = 3; historyIndex < Data.Length; historyIndex += 4)
                {
                    ushort relativeHistoryIndex = (ushort)(startIndex + ((historyIndex - 3) / 4));

                    var historyBytes = Data.ToList().GetRange(historyIndex, 4).ToArray();

                    meterHistory.Add(new HistoryRecord() { Index = relativeHistoryIndex, HistoryPointNumber = historyPointNumber, Value = historyBytes });
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

            var opCode130RequestMessage = requestMessage as OpCode130Request;

            if (opCode130RequestMessage != null)
            {
                startIndex = opCode130RequestMessage.StartingHistoryIndexPointer;
            }

            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
