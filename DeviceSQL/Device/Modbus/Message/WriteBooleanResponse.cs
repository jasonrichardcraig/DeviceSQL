#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class WriteBooleanResponse : ModbusMessage, IModbusResponseMessage
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return (IsExtendedUnitId ? 6 : 5);
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

        #endregion

        #region Helper Methods

        void IModbusResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId)
        {
            base.Initialize(frame, isExtendedUnitId);
        }

        void IModbusResponseMessage.Initialize(byte[] frame, bool isExtendedUnitId, IModbusRequestMessage requestMessage)
        {
            base.Initialize(frame, isExtendedUnitId);

            var dataLength = isExtendedUnitId ? 5 : 4;

            var writeBooleanRequest = requestMessage as WriteBooleanRequest;

            if (writeBooleanRequest != null)
            {
                data = new byte[dataLength];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, dataLength);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, dataLength);
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
