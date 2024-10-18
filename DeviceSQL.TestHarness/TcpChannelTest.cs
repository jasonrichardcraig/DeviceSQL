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

            DeviceSQL.Functions.ChannelManager.ChannelManager_RegisterTcpChannel("tcp://127.0.0.1:502", "127.0.0.1", 502, 5, 5000, 5000, 5000);

            DeviceSQL.Functions.DeviceManager.DeviceManager_RegisterModbusMaster("tcp://127.0.0.1:502", "ModRSsim2", true, false, 1, 3, 200, 0, 0);

            //var index = 0;
            //var data = new byte[] { 0x00, 0x00 };
            //var holdingRegisterAddress = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 1, IsZeroBased = true };
            //var holdingRegister = new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = holdingRegisterAddress, ByteSwap = false, Data = new SqlBinary(data) };
            //var holdingRegisters = new Types.ModbusMaster.ModbusMaster_HoldingRegisterArray();

            //holdingRegisters = holdingRegisters.AddHoldingRegister(holdingRegister);

            //var holdingRegisterz = new Types.ModbusMaster.ModbusMaster_HoldingRegister() { Address = holdingRegisterAddress, ByteSwap = false, Value = 69 };


            //while (1000 > index)
            //{
            //    var Holdings = DeviceSQL.Functions.ModbusMaster.ModbusMaster_ReadHoldings("ModRSsim2", holdingRegisters);
            //    Console.WriteLine(Holdings.GetHoldingRegister(0).Value);
            //}

            var index = 0;
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var longRegisterAddress = new Types.ModbusMaster.ModbusMaster_ModbusAddress() { RelativeAddress = 1, IsZeroBased = true };
            var longRegister = new Types.ModbusMaster.ModbusMaster_LongRegister() { Address = longRegisterAddress, ByteSwap = false, Data = new SqlBinary(data) };
            var longRegisters = new Types.ModbusMaster.ModbusMaster_LongRegisterArray();

            longRegisters = longRegisters.AddLongRegister(longRegister);

            var longRegisterz = new Types.ModbusMaster.ModbusMaster_LongRegister() { Address = longRegisterAddress, ByteSwap = false, WordSwap= false, Value = 69 };


            while (1000 > index)
            {
                var Holdings = DeviceSQL.Functions.ModbusMaster.ModbusMaster_ReadLongs("ModRSsim2", longRegisters);
                Console.WriteLine(Holdings.GetLongRegister(0).Value);
            }



            DeviceSQL.Functions.DeviceManager.DeviceManager_UnregisterDevice("ModRSsim2");

            DeviceSQL.Functions.ChannelManager.ChannelManager_UnregisterChannel("tcp://127.0.0.1:502");

            index++;
        }
    }
}
