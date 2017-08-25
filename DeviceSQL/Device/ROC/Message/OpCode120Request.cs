#region Imported Types

using System.Diagnostics;

#endregion

namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode120Request : ROCMessage, IROCRequestMessage
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

        public OpCode120Request()
        {
        }

        public OpCode120Request(byte destinationUnit, byte destinationGroup, byte sourceUnit, byte sourceGroup)
            : base(destinationUnit, destinationGroup, sourceUnit, sourceGroup, Device.OpCode120)
        {
        }

        #endregion

        #region Helper Methods

        public void ValidateResponse(IROCResponseMessage response)
        {
            var opCode120Response = response as OpCode120Response;
            Debug.Assert(opCode120Response != null, "Argument response should be of type OpCode120Response.");
        }

        #endregion

    }
}
