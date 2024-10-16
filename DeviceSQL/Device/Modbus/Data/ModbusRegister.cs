#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.Modbus.Data
{
    public abstract class ModbusRegister : IModbusRegisterData
    {

        #region Fields

        private ModbusAddress address;
        private byte[] data;

        #endregion

        #region Properties

        public ModbusAddress Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        protected internal byte[] Data
        {
            get
            {
                return ((IModbusRegisterData)this).Data;
            }
            set
            {
                ((IModbusRegisterData)this).Data = value;
            }
        }

        #endregion

        #region Constructor

        public ModbusRegister()
            : this(new ModbusAddress())
        {
        }

        internal ModbusRegister(ModbusAddress address)
        {
            this.address = address;
            this.data = null;
        }

        #endregion

        #region IModbusRegisterData Members

        byte[] IModbusRegisterData.Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        #endregion

        #region Conversion Methods

        public T ConvertTo<T>() where T : ModbusRegister, new()
        {
            return (T)this;
        }

        #endregion

    }

    public class ModbusRegister<T> : ModbusRegister
    {

        #region Constructor(s)

        public ModbusRegister()
            : base()
        {
        }

        public ModbusRegister(ModbusAddress address)
            : base(address)
        {
        }

        #endregion

        #region Properties

        public virtual T Value
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion

        #region Implicit Conversions

        public static implicit operator T(ModbusRegister<T> r)
        {
            return r.Value;
        }

        #endregion

    }
}
