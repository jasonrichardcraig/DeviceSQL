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
        public static SqlString RocMaster_GetStringValue(SqlString deviceName, SqlByte pointType, SqlByte logicalNumber, SqlByte parameterNumber, SqlByte stringLength)
        {
            var device = ServiceRegistry.GetDevice(deviceName.Value);

            if (10 >= stringLength)
            {
                var ac10Parameter = new Ac10Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
                (device as Device.Roc.RocMaster).ReadParameter<Ac10Parameter>(null, null, null, null, ref ac10Parameter);
                return ac10Parameter.Value;
            }
            else if (12 >= stringLength)
            {
                var ac12Parameter = new Ac12Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
                (device as Device.Roc.RocMaster).ReadParameter<Ac12Parameter>(null, null, null, null, ref ac12Parameter);
                return ac12Parameter.Value;
            }
            else if (20 >= stringLength)
            {
                var ac20Parameter = new Ac20Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
                (device as Device.Roc.RocMaster).ReadParameter<Ac20Parameter>(null, null, null, null, ref ac20Parameter);
                return ac20Parameter.Value;
            }
            else
            {
                var ac30Parameter = new Ac30Parameter(new Tlp(pointType.Value, logicalNumber.Value, parameterNumber.Value));
                (device as Device.Roc.RocMaster).ReadParameter<Ac30Parameter>(null, null, null, null, ref ac30Parameter);
                return ac30Parameter.Value;
            }

        }
    }
}
