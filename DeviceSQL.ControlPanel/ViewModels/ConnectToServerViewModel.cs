#region Imported Types

using GalaSoft.MvvmLight;

#endregion

namespace DeviceSQL.ControlPanel.ViewModels
{
    public class ConnectToServerViewModel : ViewModelBase
    {

        #region Fields

        private string serverName = ".\\SQLEXPRESS";
        private bool useSqlAuthentication = false;
        private string userName = System.Environment.UserName;
        private string password = "";

        #endregion

        #region Properties

        public string ServerName
        {
            get
            {
                return serverName;
            }
            set
            {
                serverName = value;
                RaisePropertyChanged(() => ServerName);
            }
        }

        public bool UseSqlAuthentication
        {
            get
            {
                return useSqlAuthentication;
            }
            set
            {
                useSqlAuthentication = value;
                RaisePropertyChanged(() => UseSqlAuthentication);
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        #endregion

    }
}
