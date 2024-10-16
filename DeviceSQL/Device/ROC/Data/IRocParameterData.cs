#region Imported Types

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public interface IRocParameterData
    {
        byte[] Data
        { get; set; }
    }
}
