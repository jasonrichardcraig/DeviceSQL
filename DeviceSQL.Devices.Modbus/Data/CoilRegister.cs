#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Data
{
    public class CoilRegister : MODBUSRegister<bool>
    {

        #region Constructor(s)

        public CoilRegister()
            : base()
        {
            Value = false;
        }

        public CoilRegister(MODBUSAddress address)
            : base(address)
        {
            Value = false;
        }

        #endregion

        #region Properties

        public override bool Value
        {
            get
            {
                return Convert.ToBoolean(Data[0]);
            }
            set
            {
                base.Data = BitConverter.GetBytes(value);
            }
        }


        #endregion

    }
}
