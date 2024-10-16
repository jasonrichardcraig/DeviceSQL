#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode180Response : RocMessage, IRocResponseMessage
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

            var opCode180RequestMessage = requestMessage as OpCode180Request;

            if (opCode180RequestMessage != null)
            {
                try
                {
                    var dataLength = frame[5];

                    data = new byte[dataLength];

                    Buffer.BlockCopy(frame, 6, data, 0, dataLength);

                    parameters = opCode180RequestMessage.Parameters;

                    var valueIndex = 1;

                    parameters.ToList().ForEach((parameter) =>
                    {
                        // Check TLP info
                        if (parameter.Tlp.PointType != data[valueIndex])
                        {
                            throw new FormatException("Point type does not match point type requested");
                        }
                        valueIndex++;
                        if (parameter.Tlp.LogicalNumber != data[valueIndex])
                        {
                            throw new FormatException("Logical number does not match logical number requested");
                        }
                        valueIndex++;
                        if (parameter.Tlp.Parameter != data[valueIndex])
                        {
                            throw new FormatException("Parameter number does not match parameter number requested");
                        }
                        valueIndex++;

                        try
                        {
                            Buffer.BlockCopy(data, valueIndex, parameter.Data, 0, parameter.Data.Length);
                        }
                        catch
                        {
                            throw new FormatException("Unable to decode opcode 180 data payload.");
                        }
                        valueIndex += (parameter.Data.Length);
                    });
                }
                catch(Exception ex)
                {
                    throw new FormatException("Unable to parse Opcode 180 response.", ex);
                }
            }

        }

        #endregion

    }
}
