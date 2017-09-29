#region Imported Types

using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class TrackableViewModelBase : ViewModelBase
    {

        #region Fields

        private bool hasChanged;
        private TrackableViewModelBase parent;

        #endregion

        #region Properties

        public bool HasChanged
        {
            get
            {
                return hasChanged;
            }
            set
            {
                hasChanged = value;
                RaisePropertyChanged("HasChanged");
            }
        }

        public TrackableViewModelBase Parent
        {
            get
            {
                return parent;
            }
        }

        #endregion

        #region Constructor(s)

        public TrackableViewModelBase()
        {
        }

        public TrackableViewModelBase(TrackableViewModelBase parent)
        {
            this.parent = parent;
        }

        #endregion

        #region Overrides 

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            HasChanged = true;
            if (parent != null)
            {
                parent.HasChanged = true;
            }
            base.RaisePropertyChanged(propertyName);
        }

        #endregion


    }
}
