﻿#region Imported Types

using System;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public class ModbusAddress
    {

        #region Properties

        public ushort AbsoluteAddress
        {
            get
            {
                if (this.IsZeroBased)
                {
                    return (ushort)(this.RelativeAddress - 1);
                }
                else
                {
                    return (ushort)(this.RelativeAddress);
                }
            }
        }

        public ushort RelativeAddress
        {
            get;
            set;
        }

        public bool IsZeroBased
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public ModbusAddress()
        {
        }

        public ModbusAddress(ushort relativeAddress, bool isZeroBased)
        {
            RelativeAddress = relativeAddress;
            IsZeroBased = isZeroBased;
        }

        #endregion

        #region Conversion Methods

        public byte[] ToArray()
        {
            if (this.IsZeroBased)
            {
                return BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)(this.RelativeAddress - 1)));
            }
            else
            {
                return BitConverter.GetBytes(IPAddress.NetworkToHostOrder((short)this.RelativeAddress));
            }
        }

        #endregion

    }
}
