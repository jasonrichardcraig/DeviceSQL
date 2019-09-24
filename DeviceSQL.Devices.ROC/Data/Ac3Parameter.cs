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
    public class Ac3Parameter : Parameter<string>
    {

        #region Constructor(s)

        public Ac3Parameter()
            : base()
        {
            Value = "";
        }

        public Ac3Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = "";
        }

        #endregion

        #region Properties

        public override string Value
        {
            get
            {
                return System.Text.ASCIIEncoding.Default.GetString(base.Data.ToArray()).Trim();
            }
            set
            {
                base.Data = System.Text.ASCIIEncoding.Default.GetBytes(value.PadLeft(3).Substring(0, 3));
            }
        }

        #endregion

    }
}
