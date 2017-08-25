#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode008Request : ROCMessage, IROCRequestMessage
    {

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
                var dateBytes = new List<byte>();
                dateBytes.Add(Convert.ToByte(DateTime.Second));
                dateBytes.Add(Convert.ToByte(DateTime.Minute));
                dateBytes.Add(Convert.ToByte(DateTime.Hour));
                dateBytes.Add(Convert.ToByte(DateTime.Day));
                dateBytes.Add(Convert.ToByte(DateTime.Month));
                dateBytes.Add(Convert.ToByte(DateTime.Year - 2000));
                return dateBytes.ToArray();
            }
        }

        public DateTime DateTime
        {
            get;
            private set;
        }


        #endregion

        #region Constructor(s)

        public OpCode008Request()
        {
        }

        public OpCode008Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, DateTime dateTime)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode8)
        {
            this.DateTime = dateTime;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode008Response = response as OpCode008Response;
            Debug.Assert(opCode008Response != null, "Argument response should be of type OpCode008Response.");
        }

        #endregion

    }
}
