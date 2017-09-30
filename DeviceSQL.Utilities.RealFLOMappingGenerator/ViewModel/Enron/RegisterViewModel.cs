#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model.Enron;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel.Enron
{
    public class RegisterViewModel : TrackableViewModelBase
    {

        #region Fields

        private Register register;

        #endregion

        #region Imported Types

        public Register Register
        {
            get
            {
                return register;
            }
            set
            {
                register = value;
                RaisePropertyChanged("Register");
            }
        }

        #endregion

    }
}
