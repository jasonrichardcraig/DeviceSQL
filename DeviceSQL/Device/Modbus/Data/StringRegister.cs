﻿#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class StringRegister : ModbusRegister<string>
    {

        #region Constructor(s)

        public StringRegister()
            : base()
        {
            Value = "";
        }

        public StringRegister(ModbusAddress address, byte length)
            : base(address)
        {
            Length = length;
            Value = "";
        }

        public StringRegister(ModbusAddress address, bool byteSwap, bool wordSwap, byte length)
            : base(address)
        {
            Length = length;
            this.ByteSwap = byteSwap;
            this.WordSwap = wordSwap;
            Value = "";
        }

        #endregion

        #region Properties

        public byte Length
        {
            get;
            set;
        }

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

        public override string Value
        {
            get
            {
                return System.Text.ASCIIEncoding.Default.GetString(this.Data).Trim("\0".ToCharArray());
            }
            set
            {
                var valueBytes = System.Text.ASCIIEncoding.Default.GetBytes(value);
                var valueLength = (valueBytes.Length > Length) ? Length : valueBytes.Length;
                var valueBuffer = new byte[Length];

                Buffer.BlockCopy(valueBytes, 0, valueBuffer, 0, valueLength);

                this.Data = valueBuffer;
            }
        }

        #endregion

    }
}
