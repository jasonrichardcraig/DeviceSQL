#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model;
using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class MapViewModel : ViewModelBase
    {

        #region Fields

        private Map map;

        #endregion

        #region Properties

        public Map Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
                RaisePropertyChanged("Map");
            }
        }

        #endregion

    }
}
