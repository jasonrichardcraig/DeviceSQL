namespace DeviceSQL.Device.ROC.Data
{
    public class UInt8Parameter : Parameter<System.Byte>
    {

        #region Constructor(s)

        public UInt8Parameter()
            : base()
        {
            Value = 0;
        }

        public UInt8Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.Byte Value
        {
            get
            {
                return base.Data[0];
            }
            set
            {
                base.Data = new byte[] { value };
            }
        }

        #endregion

    }
}
