#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Message

{
    internal class ReadHistoryArchiveResponse : MODBUSMessage, IMODBUSResponseMessage
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return (IsExtendedUnitId ? 8 : 7);
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public byte Length
        {
            get
            {
                return data[0];
            }
        }

        public HistoryArchiveRecord HistoryArchiveRecord
        {
            get;
            private set;
        }

        #endregion

        #region Helper Methods

        void IMODBUSResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId)
        {
            base.Initialize(frame, isExtendedUnitId);
        }

        void IMODBUSResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId, IMODBUSRequestMessage requestMessage)
        {
            base.Initialize(frame, isExtendedUnitId);

            var dataLength = isExtendedUnitId ? frame[3] : frame[2];

            var readHistoryArchiveRequest = requestMessage as ReadHistoryArchiveRequest;

            if (readHistoryArchiveRequest != null)
            {
                data = new byte[dataLength + 1];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, dataLength + 1);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, dataLength + 1);
                }

                var historyData = new byte[dataLength];

                Buffer.BlockCopy(data, 1, historyData, 0, dataLength);

                this.HistoryArchiveRecord = new HistoryArchiveRecord(readHistoryArchiveRequest.Index, historyData);

            }
            else
            {
                throw new FormatException("Unable to create response message (request message null).");
            }
        }

        #endregion

    }
}
