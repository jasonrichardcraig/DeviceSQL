#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class ShortRegister : ModbusRegister<short>
    {

        #region Constructor(s)

        public ShortRegister()
            : base()
        {
            Value = 0;
        }

        public ShortRegister(ModbusAddress address)
            : base(address)
        {
            Value = 0;
        }

        public ShortRegister(ModbusAddress address, bool byteSwap)
            : base(address)
        {
            this.ByteSwap = byteSwap;
            Value = 0;
        }

        #endregion

        #region Properties

        public bool ByteSwap
        {
            get;
            internal set;
        }

        public override System.Int16 Value
        {
            get
            {
                var word = ushort.MinValue;

                if (!ByteSwap)
                {
                    var words = ModbusConverter.NetworkBytesToHostUInt16(base.Data);
                    word = words[0];
                }
                else
                {
                    word = BitConverter.ToUInt16(base.Data, 0);
                }

                return Convert.ToInt16(word);

            }
            set
            {
                var valueBytes = BitConverter.GetBytes(value);

                var word = ushort.MinValue;

                if (!ByteSwap)
                {
                    var words = ModbusConverter.NetworkBytesToHostUInt16(valueBytes);
                    word = words[0];
                }
                else
                {
                    word = BitConverter.ToUInt16(valueBytes, 0);
                }

                base.Data = BitConverter.GetBytes(word);
            }
        }

        #endregion

    }
}
