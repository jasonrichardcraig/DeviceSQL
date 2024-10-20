﻿namespace DeviceSQL.Device.Roc.FST.Instructions
{
    public enum InstructionType : byte
    {
        ADD = 0x1,
        SUB = 0x2,
        MUL = 0x3,
        DIV = 0x4,
        PWR = 0x5,
        ABS = 0x6,
        EXP = 0x7,
        INT = 0x8,
        LOG = 0x9,
        LN = 0xA,
        SQR = 0xB,
        P3 = 0xC,
        NOT = 0xD,
        AND = 0xE,
        OR = 0xF,
        XOR = 0x10,
        EQ = 0x11,
        NEQ = 0x12,
        LES = 0x13,
        LEQ = 0x14,
        GTN = 0x15,
        GEQ = 0x16,
        ST = 0x17,
        CT = 0x18,
        WT = 0x19,
        AO = 0x1A,
        DO = 0x1B,
        VAL = 0x1C,
        SAV = 0x1D,
        GO = 0x1E,
        MSG = 0x1F,
        END = 0x20,
        BRK = 0x21,
        ALM = 0x22,
        EVT = 0x23,
        DWK = 0x25,
        MND = 0x26,
        TDO = 0x27,
        RDB = 0x28,
        WDB = 0x29,
        WTM = 0x2A,
        MS2 = 0x2B,
        DHV = 0x2C,
        DHT = 0x2D,
        PHV = 0x2E,
        PHT = 0x2F,
        MHV = 0x30,
        DIS = 0x31,
        DIN = 0x32,
        PIS = 0x33,
        PIN = 0x34,
        GTE = 0x35
    }
}
