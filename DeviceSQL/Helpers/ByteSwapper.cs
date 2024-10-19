using System;

namespace DeviceSQL.Helpers
{
    public static class ByteSwapper
    {
        // Swap bytes within each word (no loops, explicit swaps)
        public static byte[] SwapBytes(byte[] data, int wordSize)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (wordSize % 2 != 0)
                throw new ArgumentException("Word size must be divisible by 2.");

            // If the data length is not a multiple of the word size, return the original data
            if (data.Length % wordSize != 0)
            {
                return data; // Nothing to swap if the data length is not a multiple of the word size
            }

            // Create a copy of the array to work with
            byte[] swappedData = new byte[data.Length];
            Array.Copy(data, swappedData, data.Length);

            // Explicit swaps based on word size
            switch (wordSize)
            {
                case 1:
                    // Swap bytes for one 16-bit word (2 bytes)
                    swappedData[0] = data[1];
                    swappedData[1] = data[0];
                    break;

                case 2:
                    // Swap bytes for one 32-bit word (4 bytes)
                    swappedData[0] = data[1];
                    swappedData[1] = data[0];
                    swappedData[2] = data[3];
                    swappedData[3] = data[2];
                    break;
                default:
                    throw new ArgumentException("Unsupported word size.");
            }

            return swappedData;
        }

        // Swap two words within data (no loops, explicit swaps)
        public static byte[] SwapWords(byte[] data, int wordSize)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (wordSize % 2 != 0)
                throw new ArgumentException("Word size must be divisible by 2.");

            // If there is only one word or the data length is not twice the word size, return the original data
            if (data.Length != wordSize * 2)
            {
                return data; // Nothing to swap if we don't have exactly two words
            }

            // Create a copy of the array to work with
            byte[] swappedData = new byte[data.Length];
            Array.Copy(data, swappedData, data.Length);

            // Explicit swaps based on word size
            switch (wordSize)
            {
                case 2:
                    // Swap two 32-bit words (4 bytes each, total of 8 bytes)
                    swappedData[0] = data[2];
                    swappedData[1] = data[3];
                    swappedData[2] = data[0];
                    swappedData[3] = data[1];
                    break;

                default:
                    throw new ArgumentException("Unsupported word size.");
            }

            return swappedData;
        }

        // Full swap method that handles both byte and word swaps (no loops)
        public static byte[] ApplySwaps(byte[] data, bool byteSwap, bool wordSwap, int wordSize)
        {
            byte[] result = (byte[])data.Clone(); // Work on a cloned copy of data

            if (wordSwap)
            {
                result = SwapWords(result, wordSize);
            }

            if (byteSwap)
            {
                result = SwapBytes(result, wordSize);
            }

            return result;
        }
    }
}
