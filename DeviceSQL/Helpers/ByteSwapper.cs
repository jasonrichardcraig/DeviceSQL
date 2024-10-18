using System;

namespace DeviceSQL.Helpers
{
    namespace DeviceSQL.Helpers
    {
        public static class ByteSwapper
        {
            // Swap bytes within each word (2 bytes at a time for 16-bit words)
            public static void SwapBytes(byte[] data, int wordSize)
            {
                if (wordSize % 2 != 0)
                    throw new ArgumentException("Word size must be divisible by 2.");

                for (int i = 0; i < data.Length; i += wordSize)
                {
                    for (int j = 0; j < wordSize / 2; j++)
                    {
                        byte temp = data[i + j];
                        data[i + j] = data[i + wordSize - 1 - j];
                        data[i + wordSize - 1 - j] = temp;
                    }
                }
            }

            // Swap words within data: supports swapping 16-bit, 32-bit, and 64-bit words
            public static void SwapWords(byte[] data, int wordSize)
            {
                if (wordSize % 2 != 0)
                    throw new ArgumentException("Word size must be divisible by 2.");

                for (int i = 0; i < data.Length; i += wordSize * 2)
                {
                    for (int j = 0; j < wordSize; j++)
                    {
                        byte temp = data[i + j];
                        data[i + j] = data[i + wordSize + j];
                        data[i + wordSize + j] = temp;
                    }
                }
            }

            // Full swap method that handles both byte and word swaps based on the word size
            public static byte[] ApplySwaps(byte[] data, bool byteSwap, bool wordSwap, int wordSize)
            {
                byte[] result = (byte[])data.Clone(); // Work on a cloned copy of data

                if (wordSwap)
                {
                    SwapWords(result, wordSize);
                }

                if (!byteSwap)
                {
                    SwapBytes(result, wordSize);
                }

                return result;
            }
        }
    }

}
