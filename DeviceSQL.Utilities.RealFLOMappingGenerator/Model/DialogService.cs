#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
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

        public bool? ShowSaveBeforeProceedingDialog()
        {
            var dialogResult = (bool?)null;

            RadWindow.Confirm("Would you like to save before proceeding?", (s, e) =>
             {
                 dialogResult = e.DialogResult;
             });

            return dialogResult;

        }

        public Map OpenNewMapWizardDialog()
        {
            var newMapWizard = new Wizard.NewMapWizard();

            var dialogResult = newMapWizard.ShowDialog();

            if (dialogResult.HasValue)
            {
                if (dialogResult.Value)
                {
                    try
                    {
                        var newMapWizardViewModel = ServiceLocator.Current.GetInstance<NewMapWizardViewModel>();

                        return DataService.NewMap(newMapWizardViewModel.ToString(), newMapWizardViewModel.ToString());

                    }
                    catch (Exception ex)
                    {
                        DialogParameters dialogParameters = new DialogParameters()
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
            else
            {
                return null;
            }
        }

        #endregion

    }
}
