#region Imported Types

using DeviceSQL.ControlPanel.ViewModels;
using System.Windows;

#endregion

namespace DeviceSQL.ControlPanel.Dialogs
{
    public partial class ConnectToServerDialog : Window
    {

        #region Fields

        

        #endregion

        #region Constructor

        public ConnectToServerDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Control Events

        private void MainPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is ConnectToServerViewModel)
            {
                var connectToServerViewModel = DataContext as ConnectToServerViewModel;
                connectToServerViewModel.Password = MainPasswordBox.Password;
            }
        }

        #endregion

    }
}
