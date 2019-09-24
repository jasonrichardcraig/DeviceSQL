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
