#region Imported Types

using System.Linq;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class UInt32Parameter : Parameter<System.UInt32>
    {

        #region Constructor(s)

        public UInt32Parameter()
            : base()
        {
            Value = 0;
        }

        public UInt32Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.UInt32 Value
        {
            get
            {
                System.UInt32 value = System.BitConverter.ToUInt32(base.Data.ToArray(), 0);
                return value;
            }
            set
            {
                var dataList = System.BitConverter.GetBytes(value);
                base.Data = dataList;
            }
        }

        #endregion

    }
}
