#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadStringResponse : ModbusMessage, IModbusResponseMessage
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return (IsExtendedUnitId ? 7 : 6);
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public StringRegister StringRegister
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

            var readStringRequest = requestMessage as ReadStringRequest;

            if (readStringRequest != null)
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

                this.StringRegister = readStringRequest.StringRegister;

                var stringData = new byte[StringRegister.Length];

                Buffer.BlockCopy(data, 1, stringData, 0, dataLength);

                this.StringRegister.Data = stringData;

            }
            else
            {
                throw new FormatException("Unable to create response message (request message null).");
            }
        }

        #endregion

    }
}
