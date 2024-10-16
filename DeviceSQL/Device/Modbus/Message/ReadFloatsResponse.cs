#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadFloatsResponse : ModbusMessage, IModbusResponseMessage
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

        public List<FloatRegister> FloatRegisters
        {
            get;
            set;
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

            var readFloatsRequest = requestMessage as ReadFloatsRequest;

            if (readFloatsRequest != null)
            {
                this.FloatRegisters = readFloatsRequest.FloatRegisters;

                data = new byte[dataLength + 1];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, dataLength + 1);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, dataLength + 1);
                }

                var floatRegisters = new List<FloatRegister>();

                for (int i = 1; i < data.Length; i += 4)
                {
                    var floatData = new byte[] { data[i], data[i + 1], data[i + 2], data[i + 3] };
                    ((IModbusRegisterData)this.FloatRegisters[((i - 1) / 4)]).Data = floatData;
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
