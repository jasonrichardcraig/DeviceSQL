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

                using (var hhcFileStream = hhcFileInfo.OpenRead())
                {
                    htmlDocument.Load(hhcFileInfo.OpenRead());

                    if (htmlDocument.DocumentNode.SelectSingleNode("//head") == null)
                    {
                        htmlDocument.DocumentNode.AppendChild(new HtmlNode(HtmlNodeType.Element, htmlDocument, 0)
                        {
                            Name = "head",
                            InnerHtml = $"<script type\"text/javascript\" src=\"jquery-3.2.1.min.js\"></script>\r\n" +
                                        $"<script type\"text/javascript\" src=\"rfm.tree.js\"></script>"
                        });
                    }

                    var sitemapTextElementIndex = 0;
                    var objectIds = new List<string>();

                    htmlDocument.DocumentNode.SelectNodes("//object").ToList().ForEach(objectHTMLNode =>
                    {
                        objectIds.Add(objectHTMLNode.Id = $"chm-object-{sitemapTextElementIndex}");

                        switch (objectHTMLNode.GetAttributeValue("type", null))
                        {
                            case "text/sitemap":
                                {
                                    var nameParamHTMLNode = objectHTMLNode.SelectSingleNode("//param[@name='Name']");
                                    var imageNumberParamHTMLNode = objectHTMLNode.SelectSingleNode("//param[@name='ImageNumber']");
                                    var localParamHTMLNode = objectHTMLNode.SelectSingleNode("//param[@name='Local']");

                                    objectHTMLNode.ParentNode.AppendChild(HtmlNode.CreateNode($"<div id=\"rfm-sitemap-text-{sitemapTextElementIndex++}\">\r\n" + "" +
                                                                                                  $"<a href=\"javascript:alert('Hello'); \">{nameParamHTMLNode.GetAttributeValue("value", "")}</a>\r\n" +
                                                                                               "</div>"));

                                }
                                break;
                        }
                    });

                    objectIds.ForEach(objectId =>
                    {
                        //htmlDocument.GetElementbyId(objectId).Remove();
                    });

                }

                var destinationHHCFileName = $"{hhcFileInfo.DirectoryName}\\rfm.index.html";

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
