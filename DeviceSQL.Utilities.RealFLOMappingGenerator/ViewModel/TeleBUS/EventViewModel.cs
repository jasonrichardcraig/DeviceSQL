#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model.TeleBUS;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel.TeleBUS
{
    public class EventViewModel : TrackableViewModelBase
    {
        #region Fields

        private Event _event;

        #endregion

        #region Imported Types

        public Event Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                RaisePropertyChanged("Event");
            }
        }

        #endregion
    }
}
