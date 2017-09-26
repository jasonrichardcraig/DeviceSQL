#region Imported Types

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

        }

        public ExportMapWizardViewModel ExportMapWizard
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExportMapWizardViewModel>();
            }
        }

        public NewMapWizardViewModel NewMapWizard
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewMapWizardViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {

        }
    }
}