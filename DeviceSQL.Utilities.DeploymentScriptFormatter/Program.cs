#region Imported Types

using System;
using System.IO;

#endregion

namespace DeviceSQL.Utilities.DeploymentScriptFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // This utility requires the user to copy the SQL publish output to "|>GitHub\DeviceSQL\Version 1.0\Install Script.sql"
                // If only Microsoft would add a new SQLCLR Attribute to define which schema the object belongs to (the project is bound to a single schema for the CLR objects).

                var installScriptFolderName = @"..\..\..\Version 1.0\";
                var installScriptFileName = @"..\..\..\Version 1.0\Install Script.sql";
                var installScriptText = "";
                using (var streamReader = File.OpenText(installScriptFileName))
                {
                    installScriptText = streamReader.ReadToEnd();
                }

                installScriptText = $"CREATE DATABASE [DeviceSQL] \r\n" +
                                     "\r\n" +
                                     "GO \r\n" +
                                     "USE [DeviceSQL] \r\n" +
                                     "\r\n" +
                                     $"{installScriptText.Substring(installScriptText.IndexOf("PRINT N'Creating [ChannelManager]...';"))}";

                installScriptText = installScriptText.Substring(0, installScriptText.IndexOf("DECLARE @VarDecimalSupported AS BIT;"));

                installScriptText = installScriptText.Replace("[dbo].[Watchdog_", "[Watchdog].[").Replace("[dbo].[ChannelManager_", "[ChannelManager].[").Replace("[dbo].[DeviceManager_", "[DeviceManager].[").Replace("[dbo].[MODBUSMaster_", "[MODBUSMaster].[").Replace("[dbo].[ROCMaster_", "[ROCMaster].[");

                File.WriteAllText(installScriptFolderName + "Install Script.sql", installScriptText);

                Console.WriteLine("Formatting Completed");

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Formatting Error: {ex.Message}");

                Console.ReadKey();
            }
        }
    }
}
