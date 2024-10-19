#region Imported Types

using System;
using System.Collections.Generic;
using System.Net;

#endregion

namespace DeviceSQL.Device.Modbus
{
    public class ModbusConverter
    {
        public static ushort[] NetworkBytesToHostUInt16(byte[] networkBytes)
        {
            if (networkBytes == null)
                throw new ArgumentNullException("networkBytes");

            if (networkBytes.Length % 2 != 0)
                throw new FormatException("Array networkBytes must contain an even number of bytes.");

            ushort[] result = new ushort[networkBytes.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(networkBytes, i * 2));
            }

            return result;
        }

        public static byte[] HostUInt16ToNetworkBytes(ushort[] hostUInt16Array)
        {
            if (hostUInt16Array == null)
                throw new ArgumentNullException(nameof(hostUInt16Array));

            // Each ushort is 2 bytes, so the byte array will be twice the length of the ushort array
            byte[] networkBytes = new byte[hostUInt16Array.Length * 2];

            for (int i = 0; i < hostUInt16Array.Length; i++)
            {
                // Convert the host ushort to network byte order (big-endian)
                byte[] hostBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)hostUInt16Array[i]));

                // Copy the converted bytes into the result array
                Buffer.BlockCopy(hostBytes, 0, networkBytes, i * 2, 2);
            }

            return networkBytes;
        }


        public static float ToFloat(ushort lowWord, ushort highWord)
        {
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(lowWord));
            bytes.AddRange(BitConverter.GetBytes(highWord));
            return BitConverter.ToSingle(bytes.ToArray(), 0);
        }
    }
}
