#region Imported Types

using DeviceSQL.Installer.Dialogs;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;

#endregion

namespace DeviceSQL.Installer
{
    [RunInstaller(true)]
    public class MainInstaller : System.Configuration.Install.Installer
    {
        public MainInstaller() : base()
        {
            this.Committed += new InstallEventHandler(DeviceSQLInstaller_Committed);
            this.Committing += new InstallEventHandler(DeviceSQLInstaller_Committing);
        }

        private void DeviceSQLInstaller_Committing(object sender, InstallEventArgs e)
        {

        }

        private void DeviceSQLInstaller_Committed(object sender, InstallEventArgs e)
        {

        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
            try
            {
                var selectDatabaseServerDialog = new SelectDatabaseServerDialog();

                if(selectDatabaseServerDialog.ShowDialog() != DialogResult.OK)
                {
                    throw new OperationCanceledException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error installing database objects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Rollback(savedState);
            }
        }
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public static void Main()
        {

        }
    }
}
