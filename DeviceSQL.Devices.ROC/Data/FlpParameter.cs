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
    public class FlpParameter : Parameter<System.Single>
    {

        #region Constructor(s)

        public FlpParameter()
            : base()
        {
            Value = 0;
        }

        public FlpParameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.Single Value
        {
            get
            {
                return System.BitConverter.ToSingle(base.Data.ToArray(), 0);
            }
            set
            {
                base.Data = System.BitConverter.GetBytes(value);
            }
        }

        public System.Single? NullableValue
        {
            get
            {
                var value = System.BitConverter.ToSingle(base.Data.ToArray(), 0);

                if (Single.IsNaN(value) || Single.IsInfinity(value))
                {
                    return null;
                }
                else
                {
                    return System.BitConverter.ToSingle(base.Data.ToArray(), 0);
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
