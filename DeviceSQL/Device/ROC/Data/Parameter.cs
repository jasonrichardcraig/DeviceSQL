﻿#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{

    #region Enumerations

    public enum ParameterType : byte
    {
        BIN = 0,
        INT8 = 1,
        INT16 = 2,
        INT32 = 3,
        UINT8 = 4,
        UINT16 = 5,
        UINT32 = 6,
        FL = 7,
        TLP = 8,
        AC3 = 9,
        AC7 = 10,
        AC10 = 11,
        AC12 = 12,
        AC20 = 13,
        AC30 = 14,
        AC40 = 15,
        DOUBLE = 16,
        TIME = 17
    }

    #endregion

    public abstract class Parameter : IRocParameterData
    {

        #region Fields

        private Tlp _tlp;
        private byte[] _data;

        #endregion

        #region Properties

        public Tlp Tlp
        {
            get { return _tlp; }
            set { _tlp = value; }
        }

        public byte[] Data
        {
            get
            {
                return ((IRocParameterData)this).Data;
            }
            set
            {
                ((IRocParameterData)this).Data = value;
            }
        }

        #endregion

        #region Constructor

        public Parameter()
            : this(new Tlp(0, 0, 0))
        {
        }

        internal Parameter(Tlp tlp)
        {
            _tlp = tlp;
            _data = null;
        }

        #endregion

        #region IRocParameterData Members

        byte[] IRocParameterData.Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        #endregion

        #region Conversion Methods

        public T ConvertTo<T>() where T : Parameter, new()
        {
            return (T)this;
        }

        #endregion

    }

    public class Parameter<T> : Parameter
    {

        #region Constructor(s)

        public Parameter()
            : base()
        {
        }

        public Parameter(Tlp tlp)
            : base(tlp)
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

        public static implicit operator T(Parameter<T> p)
        {
            return p.Value;
        }

        #endregion

    }
}
