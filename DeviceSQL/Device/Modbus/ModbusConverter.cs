#region Imported Types

using System;
using System.Collections.Generic;
using System.Net;

#endregion

namespace DeviceSQL.Device.MODBUS
{
    public class MODBUSConverter
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

        public static float ToFloat(ushort lowWord, ushort highWord)
        {
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(lowWord));
            bytes.AddRange(BitConverter.GetBytes(highWord));
            return BitConverter.ToSingle(bytes.ToArray(), 0);
        }
    }
}
