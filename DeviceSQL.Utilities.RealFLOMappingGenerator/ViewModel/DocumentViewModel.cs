#region Imported Types

using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class DocumentViewModel : ViewModelBase
    {

        #region Fields

        private string fileName;

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

        #endregion

    }
}
