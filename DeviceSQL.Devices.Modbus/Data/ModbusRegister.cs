#region Imported Types

using System;

#endregion

namespace DeviceSQL.Device.MODBUS.Data
{
    public abstract class MODBUSRegister : IMODBUSRegisterData
    {

        #region Fields

        private MODBUSAddress address;
        private byte[] data;

        #endregion

        #region Properties

        public MODBUSAddress Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public byte[] Data
        {
            get
            {
                return ((IMODBUSRegisterData)this).Data;
            }
            set
            {
                ((IMODBUSRegisterData)this).Data = value;
            }
        }

        #endregion

        #region Constructor

        public MODBUSRegister()
            : this(new MODBUSAddress())
        {
        }

        internal MODBUSRegister(MODBUSAddress address)
        {
            this.address = address;
            this.data = null;
        }

        #endregion

        #region IMODBUSRegisterData Members

        byte[] IMODBUSRegisterData.Data
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

        public T ConvertTo<T>() where T : MODBUSRegister, new()
        {
            return (T)this;
        }

        #endregion

    }

    public class MODBUSRegister<T> : MODBUSRegister
    {

        #region Constructor(s)

        public MODBUSRegister()
            : base()
        {
        }

        public MODBUSRegister(MODBUSAddress address)
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

        public static implicit operator T(MODBUSRegister<T> r)
        {
            return r.Value;
        }

        #endregion

    }
}
