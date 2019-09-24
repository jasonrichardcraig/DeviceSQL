namespace DeviceSQL.Device.ROC.Message
{
    internal class OpCode181Response : ROCMessage, IROCResponseMessage
    {

        #region Properties

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
                return null;
            }
        }

        #endregion

        #region Helper Methods

        void IROCResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IROCResponseMessage.Initialize(byte[] frame, IROCRequestMessage requestMessage)
        {
            this.Initialize(frame);
        }

        #endregion

    }
}
