#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class LongRegister : ModbusRegister<int>
    {

        #region Constructor(s)

        public LongRegister()
            : base()
        {
            Value = 0;
        }

        public LongRegister(ModbusAddress address)
            : base(address)
        {
            Value = 0;
        }

        public LongRegister(ModbusAddress address, bool byteSwap, bool wordSwap)
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
                return BitConverter.ToInt32(Data, 0);
            }
            set
            {
                Data = BitConverter.GetBytes(value);
            }
        }

        #endregion

    }
}
