using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSQL.TestHarness
{
    class TcpChannelTest
    {
        public static void Test()
        {



            //DeviceSQL.Functions.ChannelManager.ChannelManager_RegisterTcpChannel("tcp://127.0.0.1:502", "127.0.0.1", 502, 5, 5000, 5000, 5000);

            //DeviceSQL.Functions.DeviceManager.DeviceManager_RegisterModbusMaster("tcp://127.0.0.1:502", "ModRSsim2", true, false, 1, 3, 0, 0, 0);



            var holdingRegister = Types.ModbusMaster.ModbusMaster_HoldingRegister.Parse("True;1,False,0");




            //var coilRegisterAddress = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 1, IsZeroBased = true };
            //var coilRegisterAddress2 = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 2, IsZeroBased = true };
            //var coilRegister = new Types.ModbusMaster.ModbusMaster_CoilRegister() { Address = coilRegisterAddress };
            //var coilRegister2 = new Types.ModbusMaster.ModbusMaster_CoilRegister() { Address = coilRegisterAddress2 };

            //var coilRegisters = new Types.ModbusMaster.ModbusMaster_CoilRegisterArray();

            //coilRegisters = coilRegisters.AddCoilRegister(coilRegister);
            //coilRegisters = coilRegisters.AddCoilRegister(coilRegister2);


            //var index = 0;

            //while (1000 > index)
            //{
            //    var coils = DeviceSQL.Functions.ModbusMaster.ModbusMaster_ReadCoils("ModRSsim2", coilRegisters);

            //    var coil = coils.GetCoilRegister(0);
            //    var coil2 = coils.GetCoilRegister(1);

            //    Console.WriteLine(coil);
            //    index++;
            //}

            //var index = 0;
            ////var data = new byte[] { 0x00, 0x00 };
            //var holdingRegisterAddress = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 1, IsZeroBased = true };
            //var holdingRegisterAddress2 = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 2, IsZeroBased = true };
            //var holdingRegisterAddress3 = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 3, IsZeroBased = true };
            //var holdingRegisterAddress4 = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 4, IsZeroBased = true };
            //var holdingRegister = new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = holdingRegisterAddress, ByteSwap = false };
            //var holdingRegister2 = new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = holdingRegisterAddress2, ByteSwap = false };
            //var holdingRegister3 = new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = holdingRegisterAddress3, ByteSwap = false };
            //var holdingRegister4 = new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = holdingRegisterAddress4, ByteSwap = false };
            //var holdingRegisters = new Types.ModbusMaster.ModbusMaster_HoldingRegisterArray();

            //holdingRegisters = holdingRegisters.AddHoldingRegister(holdingRegister);
            //holdingRegisters = holdingRegisters.AddHoldingRegister(holdingRegister2);
            //holdingRegisters = holdingRegisters.AddHoldingRegister(holdingRegister3);
            //holdingRegisters = holdingRegisters.AddHoldingRegister(holdingRegister4);

            //while (1000 > index)
            //{
            //    var Holdings = DeviceSQL.Functions.ModbusMaster.ModbusMaster_ReadHoldings("ModRSsim2", holdingRegisters);

            //    //var longgy = Holdings.GetLong(0, false, false);
            //    //var floaty = Holdings.GetFloat(0, true, true);
            //    var stringy = Holdings.GetString(0, 5);

            //    Console.WriteLine(stringy);
            //    index++;
            //}

            //var index = 0;
            //var data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            //var longRegisterAddress = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 1, IsZeroBased = true };
            //var longRegister = new Types.ModbusMaster.ModbusMaster_LongRegister() { Address = longRegisterAddress, ByteSwap = false, WordSwap = false, Data = new SqlBinary(data) };
            //var longRegisters = new Types.ModbusMaster.ModbusMaster_LongRegisterArray();

            //longRegisters = longRegisters.AddLongRegister(longRegister);

            //var longRegisterz = new Types.ModbusMaster.ModbusMaster_LongRegister() { Address = longRegisterAddress, ByteSwap = false, WordSwap = false, Value = 69 };


            //while (1000 > index)
            //{
            //    var Holdings = DeviceSQL.Functions.ModbusMaster.ModbusMaster_ReadLongs("ModRSsim2", longRegisters);
            //    Console.WriteLine(Holdings.GetLongRegister(0).Value);
            //}



            DeviceSQL.Functions.DeviceManager.DeviceManager_UnregisterDevice("ModRSsim2");

            DeviceSQL.Functions.ChannelManager.ChannelManager_UnregisterChannel("tcp://127.0.0.1:502");

            //index++;
        }
    }
}
