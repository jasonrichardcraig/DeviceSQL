namespace DeviceSQL.Device.MODBUS.Message
{
    public interface IMODBUSMessage : IMessage
    {
        bool IsExtendedUnitId { get; }
        ushort UnitId { get; set; }
        byte FunctionCode { get; set; }
    }
}
