#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.MODBUS.Message
{
    internal class ReadDiscreteInputRegistersResponse : MODBUSMessage, IMODBUSResponseMessage
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

        public List<DiscreteInputRegister> DiscreteInputRegisters
        {
            get;
            set;
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

            var readDiscreteInputRegistersRequest = requestMessage as ReadDiscreteInputRegistersRequest;

            if (readDiscreteInputRegistersRequest != null)
            {
                this.DiscreteInputRegisters = readDiscreteInputRegistersRequest.DiscreteInputRegisters;

                data = new byte[dataLength + 1];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, dataLength + 1);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, dataLength + 1);
                }

                var discreteInputValues = new BitArray(data);

                for (int i = 0; i < this.DiscreteInputRegisters.Count; i++)
                {
                    ((IMODBUSRegisterData)this.DiscreteInputRegisters[i]).Data = BitConverter.GetBytes(discreteInputValues[i + 8]);
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
