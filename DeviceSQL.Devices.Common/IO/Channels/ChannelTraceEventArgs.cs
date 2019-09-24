#region Imported Types

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace DeviceSQL.IO.Channels
{
    public class ChannelTraceEventArgs : EventArgs
    {
        public DateTime MessageDateTimeStamp { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public string Operation { get; set; }
        public double Duration { get; set; }
        public int Sequence { get; set; }
        public int Count { get; set; }
        public string Data { get; set; }
        public string ChannelType { get; set; }
    }
}
