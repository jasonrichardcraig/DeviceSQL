#region Imported Types

using System.Linq;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public class UInt16Parameter : Parameter<System.UInt16>
    {

        #region Constructor(s)

        public UInt16Parameter()
            : base()
        {
            Value = 0;
        }

        public UInt16Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.UInt16 Value
        {
            get
            {
                return System.BitConverter.ToUInt16(base.Data.ToArray(), 0);
            }
            set
            {
                base.Data = System.BitConverter.GetBytes(value);
            }
        }

        #endregion

    }
}
