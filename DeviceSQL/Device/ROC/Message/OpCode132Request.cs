#region Imported Types

using System;
using System.Diagnostics;
using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode132Request : RocMessage, IRocRequestMessage
    {

        #region Properties

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
                return BitConverter.GetBytes(NumberOfAuditLogRecordsToClear).Union(BitConverter.GetBytes(StartingAuditLogPointer)).ToArray();
            }
        }

        public byte NumberOfAuditLogRecordsToClear
        {
            get;
            set;
        }

        public ushort StartingAuditLogPointer
        {
            get;
            set;
        }

        #endregion

        #region Constructor(s)

        public OpCode132Request()
        {
        }

        public OpCode132Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup, byte numberOfAuditLogRecordsToClear, ushort startingAuditLogPointer)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode132)
        {
            NumberOfAuditLogRecordsToClear = numberOfAuditLogRecordsToClear;
            StartingAuditLogPointer = startingAuditLogPointer;
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IRocResponseMessage response)
        {
            var opCode132Response = response as OpCode132Response;
            Debug.Assert(opCode132Response != null, "Argument response should be of type OpCode132Response.");
        }

        #endregion

    }
}
