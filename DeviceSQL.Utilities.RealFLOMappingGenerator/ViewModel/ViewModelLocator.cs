#region Imported Types

using GalaSoft.MvvmLight;
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
                if (!ViewModelBase.IsInDesignModeStatic)
                {
                    return ServiceLocator.Current.GetInstance<ExportMapWizardViewModel>();
                }
                else
                {
                    return null;
                }

            }
        }

        public NewMapWizardViewModel NewMapWizard
        {
            get
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                {
                    return ServiceLocator.Current.GetInstance<NewMapWizardViewModel>();
                }
                else
                {
                    return null;
                }
            }
        }

        public MainViewModel Main
        {
            get
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                {
                    return ServiceLocator.Current.GetInstance<MainViewModel>();
                }
                else
                {
                    return null;
                }
            }
        }
        
        public static void Cleanup()
        {

        }
    }
}