namespace DeviceSQL.Device.ROC.FST.Arguments
{
    public abstract class ArgumentBase
    {

        #region Fields

        protected byte[] argumentData;
        protected ArgumentType argumentType;

        #endregion

        #region  Properties

        public abstract ArgumentType ArgumentType
        {
            get;
        }

        public abstract byte[] ArgumentData
        {
            get;
            set;
        }

        #endregion

    }
}
