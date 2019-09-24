namespace DeviceSQL.Device.ROC.FST.Arguments
{
    public enum ArgumentDataType :byte
    {
        Null = 0x0,
        ConstantChar = 0x20,
        ConstantInt8 = 0x21,
        ConstantUInt8 = 0x22,
        ConstantInt16 = 0x23,
        ConstantUInt16 = 0x24,
        ConstantInt32 = 0x25,
        ConstantUInt32 = 0x26,
        ConstantFloat = 0x27,
        ConstantLabel = 0x34,
        DatabaseUnsupported = 0x40,
        DatabaseInt8 = 0x41,
        DatabaseUInt8 = 0x42,
        DatabaseInt16 = 0x43,
        DatabaseUInt16 = 0x44,
        DatabaseUInt32 = 0x46,
        DatabaseFloat = 0x47,
        DatabaseInt32 = 0x48
    }
}
