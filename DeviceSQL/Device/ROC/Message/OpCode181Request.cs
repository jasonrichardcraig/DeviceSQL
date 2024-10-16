#region Imported Types

using DeviceSQL.Device.Roc.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode181Request : RocMessage, IRocRequestMessage
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
                    data.AddRange(p.Data);
                }

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode181Request()
        {
        }

        public OpCode181Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, List<Parameter> parameters)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode181)
        {
            this.parameters = parameters;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode181Response = response as OpCode181Response;
            Debug.Assert(opCode181Response != null, "Argument response should be of type OpCode181Response.");
        }	

        #endregion

    }
}
