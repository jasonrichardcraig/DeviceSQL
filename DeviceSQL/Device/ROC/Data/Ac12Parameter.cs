#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class Ac12Parameter : Parameter<string>
    {

        #region Constructor(s)

        public Ac12Parameter()
            : base()
        {
            Value = "";
        }

        public Ac12Parameter(Tlp tlp)
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
                base.Data = System.Text.ASCIIEncoding.Default.GetBytes(value.PadLeft(12).Substring(0, 12));
            }
        }

        #endregion

    }
}
