#region Imported Types

using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using DeviceSQL.Utilities.RealFLOMappingGenerator.Model;
using Microsoft.Practices.ServiceLocation;
using System.IO;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel
{
    public class NewMapWizardViewModel : ViewModelBase
    {

        #region Fields

        private string fileName;
        private string chmFileName = @"C:\Program Files (x86)\Schneider Electric\Realflo\Help\Realflo Reference Manual.chm";

        #endregion

        #region Properties

        public DataService DataService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DataService>();
            }
        }

        public DialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DialogService>();
            }
        }

        public RelayCommand SelectRealFLOHelpFileCommand
        {
            get;
            set;
        }

        public RelayCommand SelectMapFileNameCommand
        {
            get;
            set;
        }

        public bool FileExists
        {
            get
            {
                return File.Exists(FileName);
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                RaisePropertyChanged("FileName");
                RaisePropertyChanged("FileExists");
            }
        }

        public bool CHMFileExists
        {
            get
            {
                return File.Exists(CHMFileName);
            }
        }

        public string CHMFileName
        {
            get
            {
                return chmFileName;
            }
            set
            {
                chmFileName = value;
                RaisePropertyChanged("CHMFileName");
                RaisePropertyChanged("CHMFileExists");
            }
        }

        #endregion

        #region Constructor

        public NewMapWizardViewModel()
        {
            SelectRealFLOHelpFileCommand = new RelayCommand(SelectRealFLOHelpFile);
            SelectMapFileNameCommand = new RelayCommand(SelectMapFileName);
        }

        #endregion

        #region Command Methods

        private void SelectRealFLOHelpFile()
        {
            var selectedCHMFile = DialogService.OpenSelectRealFLOHelpFileDialog(CHMFileName);
            if(selectedCHMFile!= null)
            {
                CHMFileName = selectedCHMFile;
            }
        }

       private void SelectMapFileName()
        {
            var selectedMapFileName = DialogService.OpenCreateMapFileDialog();

            if(selectedMapFileName != null)
            {
                FileName = selectedMapFileName;
            }
            
        }

        #endregion

    }
}
