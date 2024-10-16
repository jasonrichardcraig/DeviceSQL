#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class BinParameter : Parameter<System.Byte>
    {

        #region Constructor(s)

        public BinParameter()
            : base()
        {
            Value = 0;
        }

        public BinParameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.Byte Value
        {
            get
            {
                return base.Data[0];
            }
            set
            {
                base.Data = new byte[] { value };
            }
        }

        #endregion

    }
}
