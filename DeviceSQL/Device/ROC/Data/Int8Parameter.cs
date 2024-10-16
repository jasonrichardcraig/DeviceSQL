#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class Int8Parameter : Parameter<System.SByte>
    {

        #region Constructor(s)

        public Int8Parameter()
            : base()
        {
            Value = 0;
        }

        public Int8Parameter(Tlp tlp)
            : base(tlp)
        {
            Value = 0;
        }

        #endregion

        #region Properties

        public override System.SByte Value
        {
            get
            {
                System.SByte value = (System.SByte)base.Data[0];
                return value;
            }
            set
            {
                var dataList = new byte[] { (byte)value };
                base.Data = dataList;
            }
        }

        #endregion

    }
}
