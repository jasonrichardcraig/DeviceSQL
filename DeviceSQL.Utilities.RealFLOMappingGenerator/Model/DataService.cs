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
using System.Xml;

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
                    using (var jqueryFileStream = File.OpenWrite($"{decompiledCHMFolderName}\\jquery-3.2.1.min.js"))
                    using (var treeFileStream = File.OpenWrite($"{decompiledCHMFolderName}\\rfm.tree.js"))
                    {
                        Application.GetResourceStream(new Uri("Resources/Scripts/jquery-3.2.1.min.js", UriKind.RelativeOrAbsolute)).Stream.CopyTo(jqueryFileStream);
                        Application.GetResourceStream(new Uri("Resources/Scripts/rfm.tree.js", UriKind.RelativeOrAbsolute)).Stream.CopyTo(treeFileStream);
                    }

                    foreach (var htmFileInfo in new DirectoryInfo(decompiledCHMFolderName).GetFiles("*.htm", SearchOption.AllDirectories))
                    {
                        using (var htmFileStream = htmFileInfo.Open(FileMode.Open))
                        {
                            var chmHTMLDocument = new HtmlDocument();

                            chmHTMLDocument.Load(htmFileStream, System.Text.Encoding.ASCII);
                            chmHTMLDocument.DocumentNode.InsertBefore(HtmlNode.CreateNode("<!-- saved from url=(0016)http://localhost -->"), chmHTMLDocument.DocumentNode.FirstChild);
                            htmFileStream.Position = 0;
                            chmHTMLDocument.Save(htmFileStream);
                        }
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
                var htmlDocument = new HtmlDocument();
                var destinationHHCFileName = $"{hhcFileInfo.DirectoryName}\\rfm.index.html";

                using (var hhcFileStream = hhcFileInfo.Open(FileMode.Open))
                using (var hhcStreamReader = new StreamReader(hhcFileStream))
                using (var hhcStreamWriter = new StreamWriter(hhcFileStream))
                {

                    var hhcFileContents = $"<!-- saved from url=(0016)http://localhost -->\r\n{hhcStreamReader.ReadToEnd()}";
                    hhcFileStream.Position = 0;
                    hhcStreamWriter.Write(hhcFileContents);
                    hhcStreamWriter.Flush();
                }

                htmlDocument.Load(hhcFileInfo.Open(FileMode.Open, FileAccess.ReadWrite), System.Text.Encoding.ASCII);

                //if (htmlDocument.DocumentNode.SelectSingleNode("//head") == null)
                //{
                //    htmlDocument.DocumentNode.SelectSingleNode("//html").InsertBefore(htmlDocument.DocumentNode.SelectSingleNode("//html") //.AppendChild(new HtmlNode(HtmlNodeType.Element, htmlDocument, 0)
                //    {
                //        Name = "head",
                //        InnerHtml = $"<script type\"text/javascript\" src=\"jquery-3.2.1.min.js\"></script>\r\n" +
                //                    $"<script type\"text/javascript\" src=\"rfm.tree.js\"></script>"
                //    });
                //}

                htmlDocument.DocumentNode.SelectNodes("//object").ToList().ForEach(objectHTMLNode =>
                {
                    switch (objectHTMLNode.GetAttributeValue("type", null))
                    {
                        case "text/sitemap":
                            {
                                var name = objectHTMLNode.ChildNodes.FirstOrDefault(htmlNode => htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name == "param" && htmlNode.GetAttributeValue("name", "") == "Name")?.GetAttributeValue("value", "");
                                var imageNumber = objectHTMLNode.ChildNodes.FirstOrDefault(htmlNode => htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name == "param" && htmlNode.GetAttributeValue("name", "") == "ImageNumber")?.GetAttributeValue("value", "");
                                var local = objectHTMLNode.ChildNodes.FirstOrDefault(htmlNode => htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name == "param" && htmlNode.GetAttributeValue("name", "") == "Local")?.GetAttributeValue("value", "");

                                objectHTMLNode.Name = "a";
                                objectHTMLNode.Attributes.Add("href", $"javascript:window.alert(window.external);");
                                objectHTMLNode.InnerHtml = name;

                            }
                            break;
                        default:
                            {
                                objectHTMLNode.RemoveAllChildren();
                                objectHTMLNode.Remove();
                            }
                            break;
                    }
                });

                htmlDocument.Save(destinationHHCFileName);

                return destinationHHCFileName;
            }
            else
            {
                return null;
            }
        }
    }
}
