#region Imported Types

using DeviceSQL.IO.Channels.Transport;

#endregion

namespace DeviceSQL.Device
{
    public interface IMaster
    {
        ITransport Transport { get; set; }
        string Name { get; set; }
        string Address { get; }
    }
}
