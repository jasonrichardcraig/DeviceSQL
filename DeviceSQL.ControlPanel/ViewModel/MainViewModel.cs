#region Imported Types

using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.ControlPanel.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Fields

        private object selectedExplorerTreeViewItem;

        #endregion

        #region Properties

        public object SelectedExplorerTreeViewItem
        {
            get
            {
                return selectedExplorerTreeViewItem;
            }
            set
            {
                selectedExplorerTreeViewItem = value;
                RaisePropertyChanged(() => SelectedExplorerTreeViewItem);
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                return;
            }

        }

        #endregion

    }
}