#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.ROC.Data
{
    public class Ac10Parameter : Parameter<string>
    {

        #region Constructor(s)

        public Ac10Parameter()
            : base()
        {
            Value = "";
        }

        public Ac10Parameter(Tlp tlp)
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
                base.Data = System.Text.ASCIIEncoding.Default.GetBytes(value.PadLeft(10).Substring(0, 10));
            }
        }

        #endregion

    }
}
