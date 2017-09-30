#region Imported Types

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void ExtractCHMFile(Map map, out string chmFileName, out string decompiledCHMFolderName)
        {
            var formattedMapId = map.Id.ToString().Replace("-", "");
            var chmFolderName = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RealFLOMappingGenerator";
            chmFileName = $"{chmFolderName}\\rfm.{formattedMapId}.chm";
            decompiledCHMFolderName = $"{chmFolderName}\\rfm.{formattedMapId}.decompiled";

            if (!Directory.Exists(chmFolderName))
            {
                Directory.CreateDirectory(chmFolderName);
            }

            if (!Directory.Exists(decompiledCHMFolderName))
            {
                Directory.CreateDirectory(decompiledCHMFolderName);
            }

            File.WriteAllBytes(chmFileName, map.HelpFileBytes);
            using (var process = Process.Start("hh.exe", $" -decompile {decompiledCHMFolderName} {chmFileName}"))
            {
                if (!process.WaitForExit(30000))
                {
                    throw new TimeoutException("HTML help decompiler timed out");
                }
            }
        }

    }
}
