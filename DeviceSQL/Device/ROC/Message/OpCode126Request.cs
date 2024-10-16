#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode126Request : RocMessage, IRocRequestMessage
    {

        #region Properties

        public byte HistoryPointNumber
        {
            get;
            set;
        }

        public override int MinimumFrameSize
        {
            get
            {
                return 11;
            }
        }

        public override byte[] Data
        {
            get
            {
                var data = new List<byte>();

                data.Add(HistoryPointNumber);

                return data.ToArray();
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode126Request()
        {
        }

        public OpCode126Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte historyPointNumber)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode126)
        {
            this.HistoryPointNumber = historyPointNumber;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode126Response = response as OpCode126Response;
            Debug.Assert(opCode126Response != null, "Argument response should be of type OpCode126Response.");

            var meterHistory = opCode126Response.MeterHistory;

            if (opCode126Response.HistoryPointNumber != this.HistoryPointNumber)
            {
                throw new Exception("History point recieved not the same as history point requested");
            }

        }

        #endregion

    }
}
