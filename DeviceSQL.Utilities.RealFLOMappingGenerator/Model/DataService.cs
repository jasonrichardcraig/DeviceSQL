#region Imported Types



#endregion

using System.Reflection;

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Model
{
    public class DataService
    {
        
        public string GetVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

    }
}
