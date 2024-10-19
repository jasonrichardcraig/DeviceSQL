#region Imported Types

using System;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class HoldingRegister : ModbusRegister<ushort>
    {

        #region Constructor(s)

        public HoldingRegister()
            : base()
        {
            Value = 0;
        }

        public HoldingRegister(ModbusAddress address)
            : base(address)
        {
            Value = 0;
        }

        public HoldingRegister(ModbusAddress address, bool byteSwap)
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
                return Convert.ToUInt16(BitConverter.ToInt16(Data, 0));
            }
            set
            {
                Data = BitConverter.GetBytes(value);
            }
        }

        #endregion

    }
}
