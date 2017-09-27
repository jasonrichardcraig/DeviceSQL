#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model.TeleBUS;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel.TeleBUS
{
    public class ArchiveViewModel : ViewModelBase
    {
        #region Fields

        private Archive archive;

        #endregion

        #region Imported Types

        public Archive Archive
        {
            get
            {
                return archive;
            }
            set
            {
                archive = value;
                RaisePropertyChanged("Archive");
            }
        }

        #endregion
    }
}
