#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode166Request : RocMessage, IRocRequestMessage
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

                // Add the data
                paramQuery.ToList().ForEach((p) =>
                    {
                        data.AddRange(p.Data);
                    });

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode166Request()
        {
        }

        public OpCode166Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, List<Parameter> parameters)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode166)
        {
            this.parameters = parameters;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode166Response = response as OpCode166Response;
            Debug.Assert(opCode166Response != null, "Argument response should be of type OpCode166Response.");
        }

        #endregion

    }
}
