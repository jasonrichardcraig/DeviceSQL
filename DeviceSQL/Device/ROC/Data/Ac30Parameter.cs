#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class Ac30Parameter : Parameter<string>
    {

        #region Constructor(s)

        public Ac30Parameter()
            : base()
        {
            Value = "";
        }

        public Ac30Parameter(Tlp tlp)
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
                base.Data = System.Text.ASCIIEncoding.Default.GetBytes(value.PadLeft(30).Substring(0, 30));
            }
        }

        #endregion

    }
}
