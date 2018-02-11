#region Imported Types

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace DeviceSQL.IO.Channels
{
    internal class ChannelTraceEventArgs : EventArgs
    {
        internal DateTime MessageDateTimeStamp { get; set; }
        internal string Name { get; set; }
        internal DateTime StartTime { get; set; }
        internal string Operation { get; set; }
        internal double Duration { get; set; }
        internal int Sequence { get; set; }
        internal int Count { get; set; }
        internal string Data { get; set; }
        internal string ChannelType { get; set; }
    }
}
