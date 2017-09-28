#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator.Wizard
{
    public partial class NewMapWizard : Window
    {

        #region Constructor(s)

        public NewMapWizard()
        {
            InitializeComponent();
        }

        #endregion

        #region Wizard Events

        private void RadWizard_Finish(object sender, Telerik.Windows.Controls.NavigationButtonsEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        #endregion

        #region Base Class Events

        private void Window_Closed(object sender, System.EventArgs e)
        {
            
        }
        
        #endregion

    }
}
