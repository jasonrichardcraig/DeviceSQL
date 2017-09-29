#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model.Enron;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel.Enron
{
    public class ArchiveViewModel : TrackableViewModelBase
    {
        #region Fields

        private Archive archive;

        #endregion

        #region Properties

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
