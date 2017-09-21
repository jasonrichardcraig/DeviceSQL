#region Imported Types

using System;

#endregion

namespace DeviceSQL.IO.Channels
{
    public interface IChannel : IDisposable
    {
        string Name { get; set; }
        bool TracingEnabled { get; set; }
        string ConnectionString { get; }
        int NumberOfBytesAvailable { get; }
        int ReadTimeout { get; set; }
        int WriteTimeout { get; set; }
        int Read(ref byte[] buffer, int offset, int count, int sequence);
        void Write(ref byte[] buffer, int offset, int count);
    }

}
