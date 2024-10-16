#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class CoilRegister : ModbusRegister<bool>
    {

        #region Constructor(s)

        public CoilRegister()
            : base()
        {
            Value = false;
        }

        public CoilRegister(ModbusAddress address)
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
