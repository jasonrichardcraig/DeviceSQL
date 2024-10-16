#region Imported Types

using DeviceSQL.IO.Channels.Transport;

#endregion

namespace DeviceSQL.Device
{
    public interface IDevice
    {
        ITransport Transport { get; set; }
        string Name { get; set; }
        string Address { get; }
    }
}
