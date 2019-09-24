#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public class TlpParameter : Parameter<Tlp>
    {

        #region Constructor(s)

        public TlpParameter()
            : base()
        {
            Value = new Tlp(0, 0, 0);
        }

        public TlpParameter(Tlp tlp)
            : base(tlp)
        {
            Value = new Tlp(0, 0, 0);
        }

        #endregion

        #region Properties

        public override Tlp Value
        {
            get
            {
                return new Tlp(base.Data[0], base.Data[1], base.Data[2]);
            }
            set
            {
                base.Data = new byte[] { value.PointType, value.LogicalNumber, value.Parameter };
            }
        }

        #endregion

    }
}
