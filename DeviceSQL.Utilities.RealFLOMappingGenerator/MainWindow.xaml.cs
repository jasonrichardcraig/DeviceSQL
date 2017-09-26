#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Navigation;

#endregion

namespace DeviceSQL.Utilities.RealFLOMappingGenerator
{
    public partial class MainWindow : Window
    {

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Main Web Browser Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWebBrowser.Navigate("about:blank");
        }

        private void MainWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void MainWebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            if (mainViewModel.MainWebBrowserObjectForScripting != MainWebBrowser.ObjectForScripting)
            {
                mainViewModel.MainWebBrowserObjectForScripting = MainWebBrowser.ObjectForScripting;
            }
        }
    }

    #endregion

}

