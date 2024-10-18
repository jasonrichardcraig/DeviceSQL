namespace DeviceSQL.Device.Modbus
{
    public interface IModbusDevice : IDevice
    {
        new IModbusTransport Transport { get; set; }
    }
}
