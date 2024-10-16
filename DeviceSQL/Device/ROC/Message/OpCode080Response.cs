#region Imported Types

using System;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode080Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        private OpCode080Function function;
        private byte[] data;

        #endregion

        #region Properties

        public override int MinimumFrameSize
        {
            get
            {
                return 8;
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        public OpCode080Function Function
        {
            get
            {
                return function;
            }
        }

        public string FstVersion
        {
            get
            {
                if(Function == OpCode080Function.ReadFst0HeaderInformation || Function == OpCode080Function.ReadFst1HeaderInformation)
                {
                    return System.Text.ASCIIEncoding.Default.GetString(Data.Take(8).ToArray()).Trim();
                }
                else
                {
                    return null;
                }
            }
        }

        public string FstDescription
        {
            get
            {
                if (Function == OpCode080Function.ReadFst0HeaderInformation || Function == OpCode080Function.ReadFst1HeaderInformation)
                {
                    return System.Text.ASCIIEncoding.Default.GetString(Data.Skip(8).Take(40).ToArray()).Trim();
                }
                else
                {
                    return null;
                }
            }
        }

        public byte? Offset
        {
            get;
            private set;
        }

        public byte? Length
        {
            get;
            private set;
        }

        #endregion

        #region Helper Methods

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {

            var opCode080Request = requestMessage as OpCode080Request;

            function = opCode080Request.Function;

            Offset = opCode080Request.Offset;

            Length = opCode080Request.Length;

            base.Initialize(frame);

            var dataLength = frame[5];

            data = new byte[dataLength];

            Buffer.BlockCopy(frame, 6, data, 0, dataLength);

        }

        #endregion

    }
}
