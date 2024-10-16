#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadEventArchiveResponse : ModbusMessage, IModbusResponseMessage
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

        public List<EventArchiveRecord> EventArchiveRecords
        {
            get;
            private set;
        }

        #endregion

        #region Helper Methods

        void IModbusResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId)
        {
            base.Initialize(frame, isExtendedUnitId);
        }

        void IModbusResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId, IModbusRequestMessage requestMessage)
        {
            base.Initialize(frame, isExtendedUnitId);

            var dataLength = isExtendedUnitId ? frame[3] : frame[2];

            var readEventArchiveRequest = requestMessage as ReadEventArchiveRequest;

            if (readEventArchiveRequest != null)
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

                this.EventArchiveRecords = new List<EventArchiveRecord>();

                for (int i = 1; i < dataLength; i += 20)
                {
                    var eventData = new byte[20];
                    Buffer.BlockCopy(data, i, eventData, 0, 20);
                    this.EventArchiveRecords.Add(new EventArchiveRecord(readEventArchiveRequest.Index, eventData));
                }
            }
            else
            {
                throw new FormatException("Unable to create response message (request message null).");
            }
        }

        #endregion

    }
}
