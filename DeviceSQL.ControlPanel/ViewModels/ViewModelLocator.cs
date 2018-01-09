#region Imported Types

using DeviceSQL.ControlPanel.Model;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace DeviceSQL.ControlPanel.ViewModels
{
    public class ViewModelLocator
    {

        #region Properties

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        #endregion

        #region Constructor

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);


            SimpleIoc.Default.Register<DataService>();


            SimpleIoc.Default.Register<MainViewModel>();
        }

        #endregion

        #region Cleanup Methods

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        #endregion

    }
}