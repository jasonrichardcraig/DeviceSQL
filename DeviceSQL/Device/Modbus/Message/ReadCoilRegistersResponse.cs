#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.Modbus.Message
{
    internal class ReadCoilRegistersResponse : ModbusMessage, IModbusResponseMessage
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

        public List<CoilRegister> CoilRegisters
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

            var readCoilRegistersRequest = requestMessage as ReadCoilRegistersRequest;

            if (readCoilRegistersRequest != null)
            {
                this.CoilRegisters = readCoilRegistersRequest.CoilRegisters;

                data = new byte[dataLength + 1];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, dataLength + 1);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, dataLength + 1);
                }

                var coilValues = new BitArray(data);

                for (int i = 0; i < this.CoilRegisters.Count; i++)
                {
                    ((IModbusRegisterData)this.CoilRegisters[i]).Data = BitConverter.GetBytes(coilValues[i + 8]);
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
