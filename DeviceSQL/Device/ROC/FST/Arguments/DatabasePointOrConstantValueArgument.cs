using DeviceSQL.Device.ROC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSQL.Device.ROC.FST.Arguments
{
    public class DatabasePointOrConstantValueArgument : ArgumentBase
    {

        #region Properites

        public override ArgumentType ArgumentType
        {
            get
            {
                return base.argumentType;
            }
        }

        public override byte[] ArgumentData
        {
            get
            {
                return base.argumentData;
            }

            set
            {
                base.argumentData = value;
            }
        }

        public Tlp DatabasePoint
        {
            get;
            set;
        }

        public string ConstantValue
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public DatabasePointOrConstantValueArgument()
        {
            base.argumentType = ArgumentType.DatabasePointOrConstantValue;
            base.argumentData = new byte[0];
        }

        #endregion

    }
}
