#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Data
{
    public class BooleanRegister : MODBUSRegister<bool>
    {

        #region Constructor(s)

        public BooleanRegister()
            : base()
        {
            Value = false;
        }

        public BooleanRegister(MODBUSAddress address)
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
