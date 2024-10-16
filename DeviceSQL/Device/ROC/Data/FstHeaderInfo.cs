namespace DeviceSQL.Device.Roc.Data
{
    public class FstHeaderInfo
    {

        #region Properties

        public byte FstNumber
        {
            get;
            private set;
        }

        public string Version
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;                
        }

        #endregion

        #region Constructor

        internal FstHeaderInfo(byte fstNumber, string version, string Description)
        {
            this.FstNumber = fstNumber;
            this.Version = version;
            this.Description = Description;
        }
        
        #endregion  

    }
}
