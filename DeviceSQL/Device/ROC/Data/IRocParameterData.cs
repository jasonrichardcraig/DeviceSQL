#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.ROC.Data
{
    public interface IROCParameterData
    {
        byte[] Data
        { get; set; }
    }
}
