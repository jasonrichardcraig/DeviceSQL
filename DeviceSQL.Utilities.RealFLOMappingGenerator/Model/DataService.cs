#region Imported Types

using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Model
{
    public class DataService
    {

        public string GetVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public Map NewMap(string fileName, string chmPath)
        {
            return null;
        }

        public Map LoadMap(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                return new BinaryFormatter().Deserialize(fileStream) as Map;
            }
        }

        public void SaveMap(Map map, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(fileStream, map);
            }
        }

    }
}
