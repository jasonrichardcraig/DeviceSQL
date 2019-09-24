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
    public class Int16Parameter : Parameter<System.Int16>
    {

        #region Constructor(s)

        public Int16Parameter()
            : base()
        {
            Value = 0;
        }

        public Int16Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.Int16 Value
        {
            get
            {
                return System.BitConverter.ToInt16(base.Data.ToArray(), 0);
            }
            set
            {
                base.Data = System.BitConverter.GetBytes(value);
            }
        }

        #endregion

    }
}
