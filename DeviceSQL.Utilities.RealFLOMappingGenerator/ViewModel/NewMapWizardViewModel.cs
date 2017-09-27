#region Imported Types

using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class NewMapWizardViewModel : ViewModelBase
    {

        #region Fields

        private string fileName;
        private string chmFileName;

        #endregion

        #region Properties

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                RaisePropertyChanged("FileName");
            }
        }

        public string CHMFileName
        {
            get
            {
                return chmFileName;
            }
            set
            {
                chmFileName = value;
                RaisePropertyChanged("CHMFileName");
            }
        }

        #endregion

    }
}
