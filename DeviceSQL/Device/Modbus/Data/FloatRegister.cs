#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class FloatRegister : ModbusRegister<float>
    {

        #region Constructor(s)

        public FloatRegister()
            : base()
        {
            Value = 0;
        }

        public FloatRegister(ModbusAddress address)
            : base(address)
        {
            Value = 0;
        }

        public FloatRegister(ModbusAddress address, bool byteSwap, bool wordSwap)
            : base(address)
        {
            this.ByteSwap = byteSwap;
            this.WordSwap = wordSwap;
            Value = 0;
        }

        #endregion

        #region Properties

        public bool WordSwap
        {
            get;
            internal set;
        }

        public bool ByteSwap
        {
            get;
            internal set;
        }

        public override System.Single Value
        {
            get
            {
                return BitConverter.ToSingle(Data, 0);
            }
            set
            {
                Data = BitConverter.GetBytes(value);
            }
        }

        public System.Single? NullableValue
        {
            get
            {
                var value = this.Value;
                if (Single.IsNaN(value) || Single.IsInfinity(value))
                {
                    return null;
                }
                else
                {
                    return value;
                }
            }
            set
            {
                if (value.HasValue)
                {
                    this.Value = value.Value;
                }
                else
                {
                    this.Value = Single.NaN;
                }
            }
        }

        #endregion

    }
}
