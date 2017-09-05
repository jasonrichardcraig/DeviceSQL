using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text;
using System.Linq;
using DeviceSQL.Device.ROC.Data;

namespace DeviceSQL.Functions
{
    public partial class ROCMaster
    {
        [SqlFunction]
        public static SqlBoolean ROCMaster_WriteParameters(SqlString deviceName, Types.ROCMaster.ROCMaster_ParameterArray parameterArray)
        {
            var deviceNameValue = deviceName.Value;
            var parameters = new List<Parameter>();
            var length = parameterArray.Length;

            for (var parameterIndex = 0; length > parameterIndex; parameterIndex++)
            {
                var parameter = parameterArray.GetParameter(parameterIndex);

                switch (parameter.RawType)
                {
                    case ParameterType.AC3:
                        parameters.Add(new Ac3Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.AC7:
                        parameters.Add(new Ac7Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.AC10:
                        parameters.Add(new Ac10Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.AC12:
                        parameters.Add(new Ac12Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.AC20:
                        parameters.Add(new Ac20Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.AC30:
                        parameters.Add(new Ac30Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.AC40:
                        parameters.Add(new Ac40Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.BIN:
                        parameters.Add(new BinParameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.FL:
                        parameters.Add(new FlpParameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.DOUBLE:
                        parameters.Add(new DoubleParameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.INT16:
                        parameters.Add(new Int16Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.INT32:
                        parameters.Add(new Int32Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.INT8:
                        parameters.Add(new Int8Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.TLP:
                        parameters.Add(new TlpParameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.UINT16:
                        parameters.Add(new UInt16Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.UINT32:
                        parameters.Add(new UInt32Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.TIME:
                        parameters.Add(new TimeParameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                    case ParameterType.UINT8:
                        parameters.Add(new UInt8Parameter(new Tlp(parameter.PointType, parameter.LogicalNumber, parameter.Parameter)) { Data = parameter.RawValue });
                        break;
                }

            }

    (DeviceSQL.Watchdog.Worker.Devices.First(device => (device.Name == deviceNameValue)) as Device.ROC.ROCMaster).WriteParameters(null, null, null, null, parameters);

            return true;

        }

    }
}
