#region Imported Types

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

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
                if (process.WaitForExit(30000))
                {
                    using (var styleFileStream = File.Create($"{decompiledCHMFolderName}\\rfm.style.css"))
                    using (var treeFileStream = File.Create($"{decompiledCHMFolderName}\\rfm.index.js"))
                    {
                        Application.GetResourceStream(new Uri("Resources/Styles/rfm.style.css", UriKind.RelativeOrAbsolute)).Stream.CopyTo(styleFileStream);
                        Application.GetResourceStream(new Uri("Resources/Scripts/rfm.index.js", UriKind.RelativeOrAbsolute)).Stream.CopyTo(treeFileStream);
                    }

                    foreach (var htmFileInfo in new DirectoryInfo(decompiledCHMFolderName).GetFiles("*.htm", SearchOption.AllDirectories))
                    {
                        var chmHTMLDocument = new HtmlDocument();

                        chmHTMLDocument.Load(htmFileInfo.FullName);
                        File.WriteAllText(htmFileInfo.FullName, $"<!-- saved from url=(0016)http://localhost -->\r\n{ chmHTMLDocument.DocumentNode.InnerHtml }", System.Text.Encoding.UTF8);
                    }
                }
                else
                {
                    throw new TimeoutException("HTML help decompiler timed out");
                }
            }
        }

        public string CreateIndexHTMLDocument(string chmFolderName)
        {
            var hhcFileInfo = new DirectoryInfo(chmFolderName).EnumerateFiles("*.hhc").FirstOrDefault();

            if (hhcFileInfo != null)
            {
                var hhcHTMLDocument = new HtmlDocument();
                var destinationHHCFileName = $"{hhcFileInfo.DirectoryName}\\rfm.index.html";
                var defaultSource = "";

                //file:///C:/Users/jason/Desktop/Realflo%20Reference%20Manual.htm

                hhcHTMLDocument.OptionUseIdAttribute = true;
                hhcHTMLDocument.OptionOutputAsXml = true;
                hhcHTMLDocument.Load(hhcFileInfo.FullName);

                hhcHTMLDocument.DocumentNode.ChildNodes.Where(node => node.Name == "#comment" && node.InnerText.StartsWith("<!DOCTYPE")).FirstOrDefault()?.Remove();

                if (hhcHTMLDocument.DocumentNode.SelectSingleNode("//head") == null)
                {
                    var htmlNode = hhcHTMLDocument.DocumentNode.SelectSingleNode("html");

                    htmlNode.InsertBefore(HtmlNode.CreateNode("<head></head>"), htmlNode.FirstChild);
                }

                hhcHTMLDocument.DocumentNode.SelectSingleNode("//head").AppendChild(HtmlNode.CreateNode("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">"));
                hhcHTMLDocument.DocumentNode.SelectSingleNode("//head").AppendChild(HtmlNode.CreateNode("<link type=\"text/css\" href=\"rfm.style.css\" rel=\"stylesheet\">"));
                hhcHTMLDocument.DocumentNode.SelectSingleNode("//head").AppendChild(HtmlNode.CreateNode("<script src=\"jquery.js\"></script>"));
                hhcHTMLDocument.DocumentNode.SelectSingleNode("//head").AppendChild(HtmlNode.CreateNode("<script src=\"rfm.index.js\"></script>"));

                foreach (var objectHTMLNode in hhcHTMLDocument.DocumentNode.SelectNodes("//object"))
                {
                    switch (objectHTMLNode.GetAttributeValue("type", null))
                    {
                        case "text/sitemap":
                            {
                                var name = objectHTMLNode.ChildNodes.FirstOrDefault(htmlNode => htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name == "param" && htmlNode.GetAttributeValue("name", "") == "Name")?.GetAttributeValue("value", "");
                                var imageNumber = objectHTMLNode.ChildNodes.FirstOrDefault(htmlNode => htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name == "param" && htmlNode.GetAttributeValue("name", "") == "ImageNumber")?.GetAttributeValue("value", "");
                                var local = objectHTMLNode.ChildNodes.FirstOrDefault(htmlNode => htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name == "param" && htmlNode.GetAttributeValue("name", "") == "Local")?.GetAttributeValue("value", "");

                                objectHTMLNode.Name = "a";
                                objectHTMLNode.InnerHtml = name;
                                objectHTMLNode.Attributes.Add("href", $"#_{local}_");
                                objectHTMLNode.Attributes.Add("onclick", $"javascript: window.external.NavigateMain('{chmFolderName.Replace("\\","\\\\")}\\\\{local}');");

                                if(defaultSource == "")
                                {
                                    defaultSource = $"{chmFolderName}\\{local}";
                                }
                            }
                            break;
                        default:
                            {
                                objectHTMLNode.RemoveAllChildren();
                                objectHTMLNode.Remove();
                            }
                            break;
                    }
                }

                hhcHTMLDocument.DocumentNode.SelectSingleNode("//head").AppendChild(HtmlNode.CreateNode($"<script>var defaultSource = \"{defaultSource.Replace("\\","\\\\")}\";</script>"));

                File.WriteAllText(destinationHHCFileName, $"<!-- saved from url=(0016)http://localhost -->\r\n<!DOCTYPE html>\r\n{ hhcHTMLDocument.DocumentNode.InnerHtml }", System.Text.Encoding.UTF8);

                return destinationHHCFileName;
            }
            else
            {
                return null;
            }
        }
    }
}
