#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class BooleanRegister : ModbusRegister<bool>
    {

        #region Constructor(s)

        public BooleanRegister()
            : base()
        {
            Value = false;
        }

        public BooleanRegister(ModbusAddress address)
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
