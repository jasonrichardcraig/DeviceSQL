#region Imported Types

using DeviceSQL.Device.MODBUS.Data;
using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Message

{
    internal class WriteFloatsResponse : MODBUSMessage, IMODBUSResponseMessage
    {

        #region Fields

        private byte[] data;
        private bool isZeroBased;

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

        public MODBUSAddress StartingAddress
        {
            get
            {
                var absoluteStartingAddress = BitConverter.ToUInt16(new byte[] { data[1], data[0] }, 0);
                var relativeStartingAddress = !this.isZeroBased ? absoluteStartingAddress : (ushort)(absoluteStartingAddress + (ushort)1);
                return new MODBUSAddress(relativeStartingAddress, this.isZeroBased);
            }
        }

        public ushort Count
        {
            get
            {
                return BitConverter.ToUInt16(new byte[] { data[3], data[2] }, 0);
            }
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

            var writeFloatsRequest = requestMessage as WriteFloatsRequest;

            if (writeFloatsRequest != null)
            {
                data = new byte[4];

                if (isExtendedUnitId)
                {
                    Buffer.BlockCopy(frame, 3, data, 0, 4);
                }
                else
                {
                    Buffer.BlockCopy(frame, 2, data, 0, 4);
                }

                this.isZeroBased = writeFloatsRequest.FloatRegisters[0].Address.IsZeroBased;
            }
            else
            {
                throw new FormatException("Unable to create response message (request message null).");
            }
        }

        #endregion

    }
}
