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
    internal class OpCode167Request : ROCMessage, IROCRequestMessage
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
                return 10;
            }
        }

        public override byte[] Data
        {
            get
            {
                var paramQuery = from p in parameters
                                 orderby p.Tlp.Parameter
                                 select p;

                var data = new List<Byte>(new byte[] {paramQuery.First().Tlp.PointType, paramQuery.First().Tlp.LogicalNumber, Convert.ToByte(paramQuery.Count()), paramQuery.First().Tlp.Parameter });

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode167Request()
        {
        }

        public OpCode167Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, List<Parameter> parameters)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode167)
        {
            this.parameters = parameters;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode167Response = response as OpCode167Response;
            Debug.Assert(opCode167Response != null, "Argument response should be of type OpCode167Response.");

            var expectedByteCount = (parameters.Sum(p => p.Data.Length) + 4);

            if (expectedByteCount != opCode167Response.Data.Length)
            {
                throw new FormatException(String.Format(CultureInfo.InvariantCulture,
                    "Unexpected byte count. Expected {0}, received {1}.",
                    expectedByteCount,
                    opCode167Response.Data.Length));
            }

        }

        #endregion

    }
}
