#region Imported Types

using System;
using System.Collections.Generic;
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

        public Map NewMap(string fileName, string chmFileName)
        {
            var map = new Map()
            {
                Id = Guid.NewGuid(),
                HelpFileBytes = File.ReadAllBytes(chmFileName),
                EnronArchives = new List<Enron.Archive>(),
                EnronEvents = new List<Enron.Event>(),
                EnronRegisters = new List<Enron.Register>(),
                TeleBUSArchives = new List<TeleBUS.Archive>(),
                TeleBUSEvents = new List<TeleBUS.Event>(),
                TeleBUSRegisters = new List<TeleBUS.Register>()
            };
            return map;
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

        public void ExtractCHMFile(Map map)
        {

            File.WriteAllBytes($"rfm.{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\{map.Id.ToString("X")}.chm", map.HelpFileBytes);
        }

    }
}
