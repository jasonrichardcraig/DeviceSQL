﻿#region Imported Types

using DeviceSQL.Device.Modbus.Data;
using System;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.Modbus.Message

{
    internal class ReadShortsResponse : ModbusMessage, IModbusResponseMessage
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

        public List<ShortRegister> ShortRegisters
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

            var readShortsRequest = requestMessage as ReadShortsRequest;

            if (readShortsRequest != null)
            {
                this.ShortRegisters = readShortsRequest.ShortRegisters;

                data = new byte[dataLength + 1];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, dataLength + 1);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, dataLength + 1);
                }

                var shortRegisters = new List<ShortRegister>();

                for (int i = 1; i < data.Length; i += 2)
                {
                    var shortData = new byte[] { data[i + 1], data[i] };
                    ((IModbusRegisterData)this.ShortRegisters[((i - 1) / 2)]).Data = shortData;
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
