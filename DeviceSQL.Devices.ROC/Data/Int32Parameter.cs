#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public class Int32Parameter : Parameter<System.Int32>
    {

        #region Constructor(s)

        public Int32Parameter()
            : base()
        {
            Value = 0;
        }

        public Int32Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.Int32 Value
        {
            get
            {
                return System.BitConverter.ToInt32(base.Data, 0);
            }
            set
            {
                base.Data = System.BitConverter.GetBytes(value);
            }
        }

        #endregion

    }
}
