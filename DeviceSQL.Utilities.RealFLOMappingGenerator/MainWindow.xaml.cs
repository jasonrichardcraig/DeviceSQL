#region Imported Types

using DeviceSQL.Utilities.RealFLOMappingGenerator.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Navigation;
using System;

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

        #region Base Class Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            mainViewModel.NavigateHelpDocumentWebBrowserCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<string>(NavigateHelpDocumentWebBrowser);
            mainViewModel.NavigateMainWebBrowserCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand<string>(NavigateMainWebBrowser);
            MainWebBrowser.Navigate("about:blank");
            HelpDocumentWebBrowser.Navigate("about:blank");
        }

        #endregion

        #region Web Browser Events

        private void MainWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void MainWebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            if (mainViewModel.MainWebBrowserObjectForScripting != MainWebBrowser.ObjectForScripting)
            {
                MainWebBrowser.ObjectForScripting = mainViewModel.MainWebBrowserObjectForScripting;
            }
        }

        private void HelpDocumentWebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void HelpDocumentWebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            if (mainViewModel.HelpDocumentWebBrowserObjectForScripting != HelpDocumentWebBrowser.ObjectForScripting)
            {
                HelpDocumentWebBrowser.ObjectForScripting = mainViewModel.HelpDocumentWebBrowserObjectForScripting;
            }
        }

        #endregion

        #region Navigation Methods

        private void NavigateMainWebBrowser(string source)
        {
            MainWebBrowser.Navigate(source);
        }

        private void NavigateHelpDocumentWebBrowser(string source)
        {
            HelpDocumentWebBrowser.Navigate(source);
        }

        #endregion

    }

}

