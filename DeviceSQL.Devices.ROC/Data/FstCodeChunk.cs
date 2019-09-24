#region Imported Types

using System.Collections.Generic;

#endregion

#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.Data
#else
namespace DeviceSQL.Device.ROC.Data
#endif
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
