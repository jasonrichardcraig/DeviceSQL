#region Imported Types

using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode007Request : ROCMessage, IROCRequestMessage
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
                return null;
            }
        }

        #endregion

        #region Constructor(s)

        public OpCode007Request()
        {
        }

        public OpCode007Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode7)
        {
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode007Response = response as OpCode007Response;
            Debug.Assert(opCode007Response != null, "Argument response should be of type OpCode007Response.");
        }

        #endregion

    }
}
