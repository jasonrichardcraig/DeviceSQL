#region Imported Types

using DeviceSQL.Device.Roc.Data;
using DeviceSQL.Registries;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

#endregion

namespace DeviceSQL.Functions
{
    public partial class RocMaster
    {
        [SqlFunction]
        public static SqlBoolean RocMaster_SetStringValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlByte stringLength, SqlString value)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);

            if (10 >= stringLength)
            {
                var ac10Parameter = new Ac10Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
                (device as Device.Roc.RocMaster).WriteParameter<Ac10Parameter>(null, null, null, null, ac10Parameter);
                return true;
            }
            else if (12 >= stringLength)
            {
                var ac12Parameter = new Ac12Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
                (device as Device.Roc.RocMaster).WriteParameter<Ac12Parameter>(null, null, null, null, ac12Parameter);
                return true;
            }
            else if (20 >= stringLength)
            {
                var ac20Parameter = new Ac20Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
                (device as Device.Roc.RocMaster).WriteParameter<Ac20Parameter>(null, null, null, null, ac20Parameter);
                return true;
            }
            else
            {
                var ac30Parameter = new Ac30Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value)) { Value = value.Value };
                (device as Device.Roc.RocMaster).WriteParameter<Ac30Parameter>(null, null, null, null, ac30Parameter);
                return true;
            }
        }
    }
}
