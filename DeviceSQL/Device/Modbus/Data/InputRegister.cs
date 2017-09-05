#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Data
{
    public class InputRegister : MODBUSRegister<ushort>
    {

        #region Constructor(s)

        public InputRegister()
            : base()
        {
            Value = 0;
        }

        public InputRegister(MODBUSAddress address)
            : base(address)
        {
            Value = 0;
        }

        public InputRegister(MODBUSAddress address, bool byteSwap)
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
            private set;
        }

        public override System.UInt16 Value
        {
            get
            {
                var word = ushort.MinValue;

                if (!ByteSwap)
                {
                    var words = MODBUSConverter.NetworkBytesToHostUInt16(base.Data);
                    word = words[0];
                }
                else
                {
                    word = BitConverter.ToUInt16(base.Data, 0);
                }

                return Convert.ToUInt16(word);

            }
            set
            {
                var valueBytes = BitConverter.GetBytes(value);

                var word = ushort.MinValue;

                if (!ByteSwap)
                {
                    var words = MODBUSConverter.NetworkBytesToHostUInt16(valueBytes);
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
