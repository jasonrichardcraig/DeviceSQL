#region Imported Types

using System.Collections.Generic;

#endregion

namespace DeviceSQL.Device.Roc.Data
{
    public class FstCodeChunk
    {

        #region Properties

        public byte Offset
        {
            get;
            private set;
        }

        public byte Length
        {
            get;
            private set;
        }

        public List<byte> CodeBytes
        {
            get;
            private set;
        }

        #endregion

        #region Constructor

        internal FstCodeChunk(byte offset, byte length, List<byte> codeBytes)
        {
            Offset = offset;
            Length = length;
            CodeBytes = codeBytes;
        }

        #endregion

    }

}
