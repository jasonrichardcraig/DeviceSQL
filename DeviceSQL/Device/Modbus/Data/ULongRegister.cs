#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class ULongRegister : ModbusRegister<uint>
    {

        #region Constructor(s)

        public ULongRegister()
            : base()
        {
            Value = 0;
        }

        public ULongRegister(ModbusAddress address)
            : base(address)
        {
            Value = 0;
        }

        public ULongRegister(ModbusAddress address, bool byteSwap, bool wordSwap)
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

        public override System.UInt32 Value
        {
            get
            {
                return BitConverter.ToUInt32(Data, 0);
            }
            set
            {
                Data = BitConverter.GetBytes(value);
            }
        }

        #endregion

    }
}
