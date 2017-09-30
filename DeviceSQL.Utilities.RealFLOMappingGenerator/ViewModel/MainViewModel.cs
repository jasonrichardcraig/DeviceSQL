#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Fields

        private Guid id;
        private byte[] helpFileBytes;
        private string currentMapFileName;
        private string currentCHMFileName;
        private string currentCHMFolderName;
        private string mainWebBrowserPanelHeaderText = "about:blank";
        private Interop.Main mainWebBrowserObjectForScripting;
        private Interop.HelpDocument helpDocumentWebBrowserObjectForScripting;
        private ObservableCollection<Enron.RegisterViewModel> enronRegisterViewModels;
        private ObservableCollection<Enron.ArchiveViewModel> enronArchiveViewModels;
        private ObservableCollection<Enron.EventViewModel> enronEventViewModels;
        private ObservableCollection<TeleBUS.RegisterViewModel> teleBUSRegisterViewModels;
        private ObservableCollection<TeleBUS.ArchiveViewModel> teleBUSArchiveViewModels;
        private ObservableCollection<TeleBUS.EventViewModel> teleBUSEventViewModels;
        private bool hasChanged;

        #endregion

        #region Properties

        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public byte[] HelpFileBytes
        {
            get
            {
                return helpFileBytes;
            }
            set
            {
                helpFileBytes = value;
                RaisePropertyChanged("HelpFileBytes");
            }
        }

        public DialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DialogService>();
            }
        }

        public DataService DataService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DataService>();
            }
        }

        public ObservableCollection<Enron.RegisterViewModel> EnronRegisterViewModels
        {
            get
            {
                return enronRegisterViewModels;
            }
            set
            {
                if (enronRegisterViewModels != null)
                {
                    enronRegisterViewModels.CollectionChanged -= EnronRegisterViewModels_CollectionChanged;
                }

                enronRegisterViewModels = value;

                if (enronRegisterViewModels != null)
                {
                    enronRegisterViewModels.CollectionChanged += EnronRegisterViewModels_CollectionChanged;
                }
            }
        }

        public ObservableCollection<Enron.ArchiveViewModel> EnronArchiveViewModels
        {
            get
            {
                return enronArchiveViewModels;
            }
            set
            {
                if (enronArchiveViewModels != null)
                {
                    enronArchiveViewModels.CollectionChanged -= EnronArchiveViewModels_CollectionChanged;
                }

                enronArchiveViewModels = value;

                if (enronArchiveViewModels != null)
                {
                    enronArchiveViewModels.CollectionChanged += EnronArchiveViewModels_CollectionChanged;
                }
            }
        }

        public ObservableCollection<Enron.EventViewModel> EnronEventViewModels
        {
            get
            {
                return enronEventViewModels;
            }
            set
            {
                if (enronEventViewModels != null)
                {
                    enronEventViewModels.CollectionChanged -= EnronEventViewModels_CollectionChanged;
                }

                enronEventViewModels = value;

                if (enronEventViewModels != null)
                {
                    enronEventViewModels.CollectionChanged += EnronEventViewModels_CollectionChanged;
                }
            }
        }

        public ObservableCollection<TeleBUS.RegisterViewModel> TeleBUSRegisterViewModels
        {
            get
            {
                return teleBUSRegisterViewModels;
            }
            set
            {
                if (teleBUSRegisterViewModels != null)
                {
                    teleBUSRegisterViewModels.CollectionChanged -= TeleBUSRegisterViewModels_CollectionChanged;
                }

                teleBUSRegisterViewModels = value;

                if (teleBUSRegisterViewModels != null)
                {
                    teleBUSRegisterViewModels.CollectionChanged += TeleBUSRegisterViewModels_CollectionChanged;
                }
            }
        }

        public ObservableCollection<TeleBUS.ArchiveViewModel> TeleBUSArchiveViewModels
        {
            get
            {
                return teleBUSArchiveViewModels;
            }
            set
            {
                if (teleBUSArchiveViewModels != null)
                {
                    teleBUSArchiveViewModels.CollectionChanged -= TeleBUSArchiveViewModels_CollectionChanged;
                }

                teleBUSArchiveViewModels = value;

                if (teleBUSArchiveViewModels != null)
                {
                    teleBUSArchiveViewModels.CollectionChanged += TeleBUSArchiveViewModels_CollectionChanged;
                }
            }
        }

        public ObservableCollection<TeleBUS.EventViewModel> TeleBUSEventViewModels
        {
            get
            {
                return teleBUSEventViewModels;
            }
            set
            {
                if (teleBUSEventViewModels != null)
                {
                    teleBUSEventViewModels.CollectionChanged -= TeleBUSEventViewModels_CollectionChanged;
                }

                teleBUSEventViewModels = value;

                if (teleBUSEventViewModels != null)
                {
                    teleBUSEventViewModels.CollectionChanged += TeleBUSEventViewModels_CollectionChanged;
                }
            }
        }

        public string Version
        {
            get
            {
                return DataService.GetVersion();
            }
        }

        public Interop.Main MainWebBrowserObjectForScripting
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

        public Interop.HelpDocument HelpDocumentWebBrowserObjectForScripting
        {
            get
            {
                return helpDocumentWebBrowserObjectForScripting;
            }
            set
            {
                helpDocumentWebBrowserObjectForScripting = value;
                RaisePropertyChanged("HelpDocumentWebBrowserObjectForScripting");
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

        public RelayCommand<string> NavigateHelpDocumentWebBrowserCommand
        {
            get;
            set;
        }

        public RelayCommand<string> NavigateMainWebBrowserCommand
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

        public bool CurrentMapFileExists
        {
            get
            {
                return File.Exists(CurrentCHMFileName);
            }
        }

        public string CurrentMapFileName
        {
            get
            {
                return currentMapFileName;
            }
            set
            {
                currentMapFileName = value;
                RaisePropertyChanged("CurrentMapFileName");
            }
        }

        public string CurrentCHMFileName
        {
            get
            {
                return currentCHMFileName;
            }
            set
            {
                currentCHMFileName = value;
                RaisePropertyChanged("CurrentCHMFileName");
            }
        }

        public string CurrentCHMFolderName
        {
            get
            {
                return currentCHMFolderName;
            }
            set
            {
                currentCHMFolderName = value;
                RaisePropertyChanged("CurrentCHMFolderName");
            }
        }

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
            if (CanSave())
            {
                var showSaveBeforeProceedingDialogResult = DialogService.ShowSaveBeforeProceedingDialog();
                if (showSaveBeforeProceedingDialogResult.HasValue)
                {
                    if (showSaveBeforeProceedingDialogResult.Value)
                    {
                        Save();
                    }
                }
                else
                {
                    return;
                }
            }

            var map = DialogService.OpenNewMapWizardDialog();

            if (map != null)
            {
                LoadMap(map);
            }
        }

        private void Open()
        {
            var map = DialogService.OpenMapFileDialog();
            if (map != null)
            {
                LoadMap(map);
            }
        }
        private bool CanSave()
        {
            return HasChanged;
        }

        private void Save()
        {
            try
            {
                var map = new Map()
                {
                    Id = Id,
                    HelpFileBytes = HelpFileBytes,
                    EnronRegisters = EnronRegisterViewModels?.Select(enronRegisterViewModel => enronRegisterViewModel.Register).ToList(),
                    EnronArchives = EnronArchiveViewModels?.Select(enronArchiveViewModel => enronArchiveViewModel.Archive).ToList(),
                    EnronEvents = EnronEventViewModels?.Select(enronEventViewModel => enronEventViewModel.Event).ToList(),
                    TeleBUSRegisters = TeleBUSRegisterViewModels?.Select(teleBUSRegisterViewModel => teleBUSRegisterViewModel.Register).ToList(),
                    TeleBUSArchives = teleBUSArchiveViewModels?.Select(teleBUSArchiveViewModel => teleBUSArchiveViewModel.Archive).ToList(),
                    TeleBUSEvents = TeleBUSEventViewModels?.Select(teleBUSEventViewModel => teleBUSEventViewModel.Event).ToList()
                };
                DataService.SaveMap(map, CurrentMapFileName);
            }
            catch (Exception ex)
            {
                DialogService.ShowErrorMessage($"Error saving map: {ex.Message}");
            }
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
            return !Id.Equals(Guid.Empty);
        }

        private void Close()
        {
            if (CanSave())
            {
                var showSaveBeforeProceedingDialogResult = DialogService.ShowSaveBeforeProceedingDialog();
                if (showSaveBeforeProceedingDialogResult.HasValue)
                {
                    if (showSaveBeforeProceedingDialogResult.Value)
                    {
                        Save();
                    }
                }
                else
                {
                    return;
                }
            }
            Id = Guid.Empty;
            CurrentMapFileName = null;
            CurrentCHMFileName = null;
            HelpFileBytes = new byte[] { };
            EnronArchiveViewModels = new ObservableCollection<Enron.ArchiveViewModel>();
            EnronRegisterViewModels = new ObservableCollection<Enron.RegisterViewModel>();
            EnronEventViewModels = new ObservableCollection<Enron.EventViewModel>();
            TeleBUSArchiveViewModels = new ObservableCollection<TeleBUS.ArchiveViewModel>();
            TeleBUSRegisterViewModels = new ObservableCollection<TeleBUS.RegisterViewModel>();
            TeleBUSEventViewModels = new ObservableCollection<TeleBUS.EventViewModel>();
        }

        #endregion

        #region Observable Collection Events

        private void EnronRegisterViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void EnronArchiveViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void EnronEventViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void TeleBUSRegisterViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void TeleBUSArchiveViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void TeleBUSEventViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        #endregion

        #region Helper Methods

        private void LoadMap(Map map)
        {
            if (map != null)
            {
                try
                {
                    Id = map.Id;
                    DataService.ExtractCHMFile(map, out currentCHMFileName, out currentCHMFolderName);
                    HelpFileBytes = map.HelpFileBytes;
                    EnronArchiveViewModels = new ObservableCollection<Enron.ArchiveViewModel>(map.EnronArchives.Select(enronArchive => new Enron.ArchiveViewModel() { Archive = enronArchive }));
                    EnronRegisterViewModels = new ObservableCollection<Enron.RegisterViewModel>(map.EnronRegisters.Select(enronArchive => new Enron.RegisterViewModel() { Register = enronArchive }));
                    EnronEventViewModels = new ObservableCollection<Enron.EventViewModel>(map.EnronEvents.Select(enronEvent => new Enron.EventViewModel() { Event = enronEvent }));
                    TeleBUSArchiveViewModels = new ObservableCollection<TeleBUS.ArchiveViewModel>(map.TeleBUSArchives.Select(teleBUSArchive => new TeleBUS.ArchiveViewModel() { Archive = teleBUSArchive }));
                    TeleBUSRegisterViewModels = new ObservableCollection<TeleBUS.RegisterViewModel>(map.TeleBUSRegisters.Select(teleBUSArchive => new TeleBUS.RegisterViewModel() { Register = teleBUSArchive }));
                    TeleBUSEventViewModels = new ObservableCollection<TeleBUS.EventViewModel>(map.TeleBUSEvents.Select(teleBUSEvent => new TeleBUS.EventViewModel() { Event = teleBUSEvent }));

                    var indexHTMLDocumentFileName = DataService.CreateIndexHTMLDocument(currentCHMFolderName);

                    if (indexHTMLDocumentFileName != null)
                    {   
                        NavigateHelpDocumentWebBrowserCommand.Execute(indexHTMLDocumentFileName);
                    }
                    else
                    {
                        DialogService.ShowErrorMessage("Unable to create index HTML document");
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    DialogService.ShowErrorMessage($"Error loading map: {ex.Message}");
                }
            }
        }

        #endregion

    }
}