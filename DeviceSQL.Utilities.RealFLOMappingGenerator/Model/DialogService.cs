#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using System;
using System.IO;
using Telerik.Windows.Controls;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Model
{
    public class DialogService
    {

        #region Properties

        public DataService DataService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DataService>();
            }
        }

        #endregion

        #region Dialog Methods

        public void ShowErrorMessage(string errorMessage)
        {
            RadWindow.Alert(errorMessage);
        }

        public string OpenCreateMapFileDialog()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Title = "Set Map File Destination",
                FileName = "RealFLO Map.rfm",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = ".chm",
                Filter = "RealFLO Map Files|*.rfm",
                OverwritePrompt = true,
                CreatePrompt = true,
                ValidateNames = true,
                CheckPathExists = true,
            };

            var dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                try
                {
                    using (var fileStream = File.Create(saveFileDialog.FileName))
                    {
                        return saveFileDialog.FileName;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error creating map file: {ex.Message}");
                    return null;
                }

            }
            else
            {
                return null;
            }

        }

        public string OpenSelectRealFLOHelpFileDialog(string fileName)
        {

            var fileInfo = new FileInfo(fileName);

            var openFileDialog = new OpenFileDialog()
            {
                Title = "Select RealFLO Help File",
                FileName = fileInfo.Name,
                InitialDirectory = fileInfo.DirectoryName,
                DefaultExt = ".chm",
                Filter = "Help Files|*.chm"
            };

            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public bool? ShowSaveBeforeProceedingDialog()
        {
            var dialogResult = (bool?)null;
            var dialogParameters = new DialogParameters()
            {
                Content = "Would you like to save before proceeding?",
                OkButtonContent = "Yes",
                CancelButtonContent = "No",
                Closed = (s, e) =>
                {
                    dialogResult = e.DialogResult;
                }
            };

            RadWindow.Confirm(dialogParameters);

            return dialogResult;

        }

        public Map OpenNewMapWizardDialog()
        {
            var newMapWizard = new Wizard.NewMapWizard()
            {
                Owner = App.Current.MainWindow
            };

            var dialogResult = newMapWizard.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                try
                {
                    var newMapWizardViewModel = ServiceLocator.Current.GetInstance<NewMapWizardViewModel>();

                    return DataService.NewMap(newMapWizardViewModel.FileName, newMapWizardViewModel.CHMFileName);

                }
                catch (Exception ex)
                {
                    var dialogParameters = new DialogParameters()
                    {
                        Content = $"Error creating new map: {ex.Message}"
                    };

                    RadWindow.Alert(dialogParameters);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Map OpenMapFileDialog()
        {

            var openFileDialog = new OpenFileDialog()
            {
                Title = "Select RealFLO Map File",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = ".rfm",
                Filter = "RealFLO Map Files|*.rfm"
            };

            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                try
                {
                    return DataService.LoadMap(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error opening map file: {ex.Message}");
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}
