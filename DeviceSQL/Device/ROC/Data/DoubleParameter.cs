#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.ROC.Data
{
    public class DoubleParameter : Parameter<System.Double>
    {

        #region Constructor(s)

        public DoubleParameter()
            : base()
        {
            Value = 0;
        }

        public DoubleParameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.Double Value
        {
            get
            {
                return System.BitConverter.ToDouble(base.Data.ToArray(), 0);
            }
            set
            {
                base.Data = System.BitConverter.GetBytes(value);
            }
        }

        public System.Double? NullableValue
        {
            get
            {
                var value = System.BitConverter.ToDouble(base.Data.ToArray(), 0);

                if (Double.IsNaN(value) || Double.IsInfinity(value))
                {
                    return null;
                }
                else
                {
                    return System.BitConverter.ToDouble(base.Data.ToArray(), 0);
                }
            }
            set
            {
                if (value.HasValue)
                {
                    base.Data = System.BitConverter.GetBytes(value.Value);
                }
                else
                {
                    base.Data = System.BitConverter.GetBytes(Single.NaN);
                }
            }
        }

        #endregion

    }
}
