#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Runtime.InteropServices;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Interop
{
    [ComVisible(true)]
    public class HelpDocument
    {
        public void NavigateMain(string source)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().NavigateMainWebBrowserCommand.Execute(source);
        }
    }
}
