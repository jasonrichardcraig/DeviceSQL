#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.ROC.Data
{
    public class Ac7Parameter : Parameter<string>
    {

        #region Constructor(s)

        public Ac7Parameter()
            : base()
        {
            Value = "";
        }

        public Ac7Parameter(Tlp tlp)
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
                base.Data = System.Text.ASCIIEncoding.Default.GetBytes(value.PadLeft(7).Substring(0, 7));
            }
        }

        #endregion

    }
}
