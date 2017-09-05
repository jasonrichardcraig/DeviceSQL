#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Data
{
    public class LongRegister : MODBUSRegister<int>
    {

        #region Constructor(s)

        public LongRegister()
            : base()
        {
            Value = 0;
        }

        public LongRegister(MODBUSAddress address)
            : base(address)
        {
            Value = 0;
        }

        public LongRegister(MODBUSAddress address, bool byteSwap, bool wordSwap)
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

        public override System.Int32 Value
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
                    return BitConverter.ToInt32(new byte[] { highWordBytes[0], highWordBytes[1], lowWordBytes[0], lowWordBytes[1] }, 0);
                }
                else
                {
                    return BitConverter.ToInt32(new byte[] { lowWordBytes[0], lowWordBytes[1], highWordBytes[0], highWordBytes[1] }, 0);
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

        #endregion

    }
}
