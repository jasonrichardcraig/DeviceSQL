#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{

    public abstract class Parameter : IROCParameterData
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
                return ((IROCParameterData)this).Data;
            }
            set
            {
                ((IROCParameterData)this).Data = value;
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

        #region IROCParameterData Members

        byte[] IROCParameterData.Data
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
