#region Imported Types

using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using DeviceSQL.Utilities.RealFLOMappingGenerator.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Fields

        private string mainWebBrowserPanelHeaderText = "about:blank";
        private object mainWebBrowserObjectForScripting;
        private Map map;

        #endregion

        #region Properties

        public DataService DataService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DataService>();
            }
        }

        public List<Enron.RegisterViewModel> EnronRegisterViewModels
        {
            get
            {

            }
        }

        public string Version
        {
            get
            {
                return DataService.GetVersion();
            }
        }

        public object MainWebBrowserObjectForScripting
        {
            get
            {
                return mainWebBrowserObjectForScripting;
            }
            set
            {
                mainWebBrowserObjectForScripting = value;
                RaisePropertyChanged("MainWebBrowserObjectForScripting");
            }
        }

        public RelayCommand NewCommand
        {
            get;
            set;
        }

        public RelayCommand OpenCommand
        {
            get;
            set;
        }

        public RelayCommand SaveCommand
        {
            get;
            set;
        }

        public RelayCommand ExportCommand
        {
            get;
            set;
        }

        public RelayCommand CloseCommand
        {
            get;
            set;
        }

        public string MainWebBrowserPanelHeaderText
        {
            get
            {
                return mainWebBrowserPanelHeaderText;
            }
            set
            {
                mainWebBrowserPanelHeaderText = value;
                RaisePropertyChanged("MainWebBrowserPanelHeaderText");
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            NewCommand = new RelayCommand(New);
            OpenCommand = new RelayCommand(Open);
            SaveCommand = new RelayCommand(Save, CanSave);
            ExportCommand = new RelayCommand(Export, CanExport);
            CloseCommand = new RelayCommand(Close, CanClose);
        }

        #endregion

        #region Command Methods

        private void New()
        {
            
        }

        private void Open()
        {



        }
        private bool CanSave()
        {
            return false;
        }

        private void Save()
        {
            
        }

        private bool CanExport()
        {
            return false;
        }

        private void Export()
        {
            
        }

        private bool CanClose()
        {
            return false;
        }

        private void Close()
        {

        }

        #endregion

    }
}