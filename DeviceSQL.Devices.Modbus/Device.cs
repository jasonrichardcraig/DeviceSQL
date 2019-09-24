﻿namespace DeviceSQL.Device.MODBUS
{
    internal static class Device
    {

        #region Enums

        internal enum DataType
        {
            Boolean,
            Short,
            UShort,
            Long,
            Float,
            String,
            HistoryArchive,
            EventArchive
        }

        #endregion

        #region Constants

        public const int DefaultRetries = 3;
        public const int DefaultWaitToRetryMilliseconds = 250;
        public const int MinimumFrameSize = 3;
        public const int MinimumFrameSizeWithExtendedUnitId = 4;

        // MODBUS Functions
        public const byte ReadCoils = 1;
        public const byte ReadDiscreteInputs = 2;
        public const byte ReadHoldingRegisters = 3;
        public const byte ReadInputRegisters = 4;
        public const byte WriteSingleCoil = 5;
        public const byte WriteMultipleRegisters = 16;

        // MODBUS slave exception offset that is added to the function code, to flag an exception
        public const byte ExceptionOffset = 128;

        // MODBUS slave exception codes
        public const byte IllegalFunction = 1;
        public const byte IllegalDataAddress = 2;
        public const byte IllegalDataValue = 3;
        public const byte SlaveDeviceFailure = 4;
        public const byte Acknowledge = 5;
        public const byte SlaveDeviceBusy = 6;
        public const byte NegativeAcknowlegde = 7;
        public const byte MemoryParityError = 8;                               

        #endregion

    }
}
