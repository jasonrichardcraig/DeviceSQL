#region Imported Types

using System;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
    public class TimeParameter : Parameter<DateTime>
    {

        #region Constructor(s)

        public TimeParameter()
            : base()
        {
            Value = new DateTime(1970, 01, 01);
        }

        public TimeParameter(Tlp tlp)
            : base(tlp)
        {
            Value = new DateTime(1970, 01, 01);
        }

        #endregion

        #region Properties

        public override System.DateTime Value
        {
            get
            {
                return new DateTime(1970, 01, 01).AddSeconds(System.BitConverter.ToUInt32(base.Data, 0));
            }
            set
            {
                base.Data = System.BitConverter.GetBytes(System.Convert.ToUInt32(value.Subtract(new DateTime(1970, 01, 01)).TotalSeconds));
            }
        }

        #endregion

    }
}
