#region Imported Types

using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode180Request : ROCMessage, IROCRequestMessage
    {

        #region Fields

        private List<Parameter> parameters = new List<Parameter>();

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
                return 12;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<Byte>(new byte[] { Convert.ToByte(parameters.Count) });

                foreach (Parameter p in parameters)
                {
                    data.AddRange(p.Tlp.ToArray());
                }

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode180Request()
        {
        }

        public OpCode180Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, List<Parameter> parameters)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode180)
        {
            this.parameters = parameters;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode180Response = response as OpCode180Response;
            Debug.Assert(opCode180Response != null, "Argument response should be of type OpCode180Response.");

            var expectedByteCount = (parameters.Sum(p => p.Data.Length) + parameters.Sum(t => t.Tlp.ToArray().Length)) + 1;

            if (expectedByteCount != opCode180Response.Data.Length)
            {
                throw new FormatException(String.Format(CultureInfo.InvariantCulture,
                    "Unexpected byte count. Expected {0}, received {1}.",
                    expectedByteCount,
                    opCode180Response.Data.Length));
            }
        }	

        #endregion

    }
}
