#region Imported Types

using System.Linq;

#endregion


#if SQLTYPES
namespace DeviceSQL.SQLTypes.ROC.IO.Channels
#else
namespace DeviceSQL.IO.Channels
#endif
{
    public static class HexConverter
    {
        public static string ToHexString(byte[] bytes)
        {
            string hexString = "";

            bytes.ToList().ForEach(item =>
            {
                hexString += " ";
                hexString += item.ToString("X").PadLeft(2, char.Parse("0"));
            });

            return hexString;
        }
    }
}
