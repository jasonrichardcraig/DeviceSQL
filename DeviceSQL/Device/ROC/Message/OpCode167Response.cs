#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode167Response : RocMessage, IRocResponseMessage
    {

        #region Fields

        private List<Parameter> parameters = new List<Parameter>();
        private byte[] data;

        #endregion

        #region Properties

        public List<Parameter> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 10;
            }
        }

        public override byte[] Data
        {
            get
            {
                return data;
            }
        }

        #endregion

        #region Helper Methods

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {
            base.Initialize(frame);
            if (frame.Length < MinimumFrameSize)
                throw new FormatException("Message frame does not contain enough bytes.");

            var opCode167RequestMessage = requestMessage as OpCode167Request;

            if (opCode167RequestMessage != null)
            {

                var dataLength = frame[5];

                data = new byte[dataLength];

                Buffer.BlockCopy(frame, 6, data, 0, dataLength);

                parameters = opCode167RequestMessage.Parameters;

                var paramQuery = from p in parameters
                                 orderby p.Tlp.Parameter
                                 select p;

                var dataOffset = 4;

                paramQuery.ToList().ForEach((parameter) =>
                {
                    Buffer.BlockCopy(data, dataOffset, parameter.Data, 0, parameter.Data.Length);

                    dataOffset += parameter.Data.Length;
                });
            }

        }

        #endregion

    }
}
