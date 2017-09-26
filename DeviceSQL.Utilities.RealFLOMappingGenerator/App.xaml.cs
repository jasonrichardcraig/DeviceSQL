#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model;
using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<DataService>();
            SimpleIoc.Default.Register<MainViewModel>();
        }
    }
}
