#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Data
{
    public class FloatRegister : MODBUSRegister<float>
    {

        #region Constructor(s)

        public FloatRegister()
            : base()
        {
            Value = 0;
        }

        public FloatRegister(MODBUSAddress address)
            : base(address)
        {
            Value = 0;
        }

        public FloatRegister(MODBUSAddress address, bool byteSwap, bool wordSwap)
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
                var highWord = ushort.MinValue;
                var lowWord = ushort.MinValue;

                if (!ByteSwap)
                {
                    var words = MODBUSConverter.NetworkBytesToHostUInt16(base.Data);
                    highWord = words[1];
                    lowWord = words[0];
                }
                else
                {
                    highWord = BitConverter.ToUInt16(base.Data, 2);
                    lowWord = BitConverter.ToUInt16(base.Data, 0);
                }

                var highWordBytes = BitConverter.GetBytes(highWord);
                var lowWordBytes = BitConverter.GetBytes(lowWord);

                if (!WordSwap)
                {
                    return BitConverter.ToSingle(new byte[] { highWordBytes[0], highWordBytes[1], lowWordBytes[0], lowWordBytes[1] }, 0);
                }
                else
                {
                    return BitConverter.ToSingle(new byte[] { lowWordBytes[0], lowWordBytes[1], highWordBytes[0], highWordBytes[1] }, 0);
                }

            }
            set
            {
                var valueBytes = BitConverter.GetBytes(value);
                var highWord = ushort.MinValue;
                var lowWord = ushort.MinValue;

                if (!ByteSwap)
                {
                    var words = MODBUSConverter.NetworkBytesToHostUInt16(valueBytes);
                    highWord = words[1];
                    lowWord = words[0];
                }
                else
                {
                    highWord = BitConverter.ToUInt16(valueBytes, 2);
                    lowWord = BitConverter.ToUInt16(valueBytes, 0);
                }

                var highWordBytes = BitConverter.GetBytes(highWord);
                var lowWordBytes = BitConverter.GetBytes(lowWord);

                if (!WordSwap)
                {
                    base.Data = new byte[] { highWordBytes[0], highWordBytes[1], lowWordBytes[0], lowWordBytes[1] };
                }
                else
                {
                    base.Data = new byte[] { lowWordBytes[0], lowWordBytes[1], highWordBytes[0], highWordBytes[1] };
                }
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
