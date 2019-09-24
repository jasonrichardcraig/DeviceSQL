﻿#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode007Response : ROCMessage, IROCResponseMessage
    {

        #region Fields

        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return 13;
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public DateTime GetDateTime(ushort century)
        {
            return new DateTime((Data[5] + century), Data[4], Data[3], Data[2], Data[1], Data[0]);
        }

        public DateTime DateTime
        {
            get
            {
                return new DateTime(BitConverter.ToUInt16(Data, 5), Data[4], Data[3], Data[2], Data[1], Data[0]);
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
            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);
        }

        #endregion

    }
}
