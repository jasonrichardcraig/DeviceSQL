#region Imported Types

using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

#endregion

namespace DeviceSQL.Installer.Dialogs
{
    public partial class SelectDatabaseServerDialog : Form
    {

        #region Constructor

        public SelectDatabaseServerDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Base Class Events

        private void SelectDatabaseServerDialog_Load(object sender, EventArgs e)
        {
            try
            {
                var instance = SqlDataSourceEnumerator.Instance;
                var databaseSourcesDataTable = instance.GetDataSources();

                foreach (DataRow row in databaseSourcesDataTable.Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["InstanceName"].ToString()))
                    {
                        databaseInstanceComboBox.Items.Add(row["ServerName"].ToString() + "\\" + row["InstanceName"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading database server list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            windowsAutenticationRadioButton.Checked = true;

        }

        #endregion

        #region Form Control Events

        private void installButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(asymmetricKeyTextBox.Text))
            {
                MessageBox.Show($"Asymmetric key folder does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    File.WriteAllBytes($"{asymmetricKeyTextBox.Text}\\DeviceSQL.dll", Properties.Resources.DeviceSQL);
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error copying Asymmetric key file to specified path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                
            }

            try
            {
                using (var sqlConnection = new SqlConnection(BuildConnectionString()))
                {
                    var securityScriptText = Properties.Resources.DeviceSQL_Install_Script_01.Replace("##_ASYMMETRIC_KEY_EXECUTABLE_FILE#", $"{asymmetricKeyTextBox.Text}\\DeviceSQL.dll");
                    var objectsScriptText = Properties.Resources.DeviceSQL_Install_Script_02;

                    sqlConnection.Open();

                    var sqlTransaction = sqlConnection.BeginTransaction();

                    

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error installing database objects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void testButton_Click(object sender, EventArgs e)
        {
            installButton.Enabled = BuildConnectionString() != null;
        }

        private void sqlAuthenticationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sqlAuthenticationRadioButton.Checked)
            {
                userNameTextBox.ReadOnly = false;
                userNameTextBox.Text = "sa";
                passwordTextBox.ReadOnly = false;
                passwordTextBox.Text = "";
            }
        }

        private void windowsAutenticationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (windowsAutenticationRadioButton.Checked)
            {
                userNameTextBox.ReadOnly = true;
                userNameTextBox.Text = Environment.UserDomainName;
                passwordTextBox.ReadOnly = true;
                passwordTextBox.Text = "";
            }
        }

        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (windowsAutenticationRadioButton.Checked && databaseInstanceComboBox.Text?.Length > 0)
            {
                testButton.Enabled = true;
            }
            else if (sqlAuthenticationRadioButton.Checked && databaseInstanceComboBox.Text?.Length > 0)
            {
                testButton.Enabled = true;
            }
            else
            {
                testButton.Enabled = false;
            }
        }

        #endregion

        #region Helper Methods

        public string BuildConnectionString()
        {
            try
            {
                using (var sqlConnection = new SqlConnection())
                {
                    var sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

                    sqlConnectionStringBuilder.InitialCatalog = "master";
                    sqlConnectionStringBuilder.DataSource = databaseInstanceComboBox.Text;

                    if (windowsAutenticationRadioButton.Checked)
                    {
                        sqlConnectionStringBuilder.IntegratedSecurity = true;
                        sqlConnectionStringBuilder.Authentication = SqlAuthenticationMethod.NotSpecified;
                    }
                    else
                    {
                        sqlConnectionStringBuilder.IntegratedSecurity = false;
                        sqlConnectionStringBuilder.Authentication = SqlAuthenticationMethod.SqlPassword;
                        sqlConnectionStringBuilder.UserID = userNameTextBox.Text;
                        sqlConnectionStringBuilder.Password = passwordTextBox.Text;
                    }

                    sqlConnection.ConnectionString = sqlConnectionStringBuilder.ToString();

                    sqlConnection.Open();

                    return sqlConnection.ConnectionString;

                }
            }
            catch (Exception ex)
            {
                installButton.Enabled = false;
                MessageBox.Show($"Error testing database connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        #endregion

    }
}

