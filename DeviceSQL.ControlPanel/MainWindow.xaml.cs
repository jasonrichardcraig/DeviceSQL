#region Imported Types

using DeviceSQL.ControlPanel.Dialogs;
using DeviceSQL.ControlPanel.ViewModels;
using System;
using System.Windows;

#endregion

namespace DeviceSQL.ControlPanel
{

    public partial class MainWindow : Window
    {

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Control Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var connectToServerDialog = new ConnectToServerDialog();
                var dialogResult = connectToServerDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening dialog: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
