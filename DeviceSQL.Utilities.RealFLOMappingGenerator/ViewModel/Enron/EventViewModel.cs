#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model.Enron;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel.Enron
{
    public class EventViewModel : ViewModelBase
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
