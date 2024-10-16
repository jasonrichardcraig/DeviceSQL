namespace DeviceSQL.Device.Roc.Message
{
    internal class OpCode181Response : RocMessage, IRocResponseMessage
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

        void IRocResponseMessage.Initialize(byte[] frame)
        {
            base.Initialize(frame);
        }

        void IRocResponseMessage.Initialize(byte[] frame, IRocRequestMessage requestMessage)
        {
            this.Initialize(frame);
        }

        #endregion

    }
}
