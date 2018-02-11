using DeviceSQL.Devices.Device.ROC.Data;

namespace DeviceSQL.Device.ROC.Data.FST.Arguments
{
    public class DatabasePoint
    {

        #region Fields

        public readonly Tlp Tlp;
        public readonly ParameterType ParameterType;

        #endregion

        #region Constructor

        DatabasePoint(byte pointType, byte logicalNumber, byte parameter, ParameterType parameterType) 
        {
            Tlp = new Tlp(pointType, logicalNumber, parameter);
            ParameterType = parameterType;
        }

        DatabasePoint(Tlp tlp, ParameterType parameterType)
        {
            Tlp = tlp;
            ParameterType = parameterType;
        }

        #endregion

    }
}
