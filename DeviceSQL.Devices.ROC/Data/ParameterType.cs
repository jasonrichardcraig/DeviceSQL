using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
{
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
}
