#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class Ac20Parameter : Parameter<string>
    {

        #region Constructor(s)

        public Ac20Parameter()
            : base()
        {
            Value = "";
        }

        public Ac20Parameter(Tlp tlp)
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
                base.Data = System.Text.ASCIIEncoding.Default.GetBytes(value.PadLeft(20).Substring(0, 20));
            }
        }

        #endregion

    }
}
