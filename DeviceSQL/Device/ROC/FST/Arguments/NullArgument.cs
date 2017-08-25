namespace DeviceSQL.Device.ROC.FST.Arguments
{
    public class NullArgument : ArgumentBase
    {

        #region Properties

        public override byte[] ArgumentData
        {
            get
            {
                return base.argumentData;
            }

            set
            {
                base.argumentData = value;
            }
        }

        public override ArgumentType ArgumentType
        {
            get
            {
                return base.argumentType;
            }
        }

        #endregion

        #region Constructor

        public NullArgument()
        {
            base.argumentType = ArgumentType.None;
            base.argumentData = new byte[0];
        }

        #endregion

    }
}
