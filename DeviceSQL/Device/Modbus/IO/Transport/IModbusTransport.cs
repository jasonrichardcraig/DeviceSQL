using DeviceSQL.Device.Modbus.Message;
using DeviceSQL.IO.Channels.Transport;

public interface IModbusTransport : ITransport
{
    TResponseMessage UnicastMessage<TResponseMessage>(IModbusRequestMessage requestMessage)
    where TResponseMessage : IModbusResponseMessage, new();
}