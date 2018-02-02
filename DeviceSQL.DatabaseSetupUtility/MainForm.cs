#region Imported Types

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#endregion

namespace DeviceSQL.DatabaseSetupUtility
{
    public partial class MainForm : Form
    {

        #region Fields

        private bool hasRetrievedDataSources;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Base Class Events

        private void MainForm_Load(object sender, EventArgs e)
        {

            windowsAutenticationRadioButton.Checked = true;
            userNameTextBox.Text = Environment.UserName;

        }

        #endregion

        #region Form Control Events

        private void databaseInstanceComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (!hasRetrievedDataSources)
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
                    hasRetrievedDataSources = true;
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading database server list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                catch (Exception ex)
                {
                    MessageBox.Show($"Error copying asymmetric key file to specified path: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                using (var sqlConnection = new SqlConnection(BuildConnectionString()))
                {
                    sqlConnection.Open();

                    var sqlTransaction = (SqlTransaction)null;

                    try
                    {

                        foreach (var sqlStatment in SplitSqlStatements(Properties.Resources.DeviceSQL_Install_Script_01.Replace("##_ASYMMETRIC_KEY_EXECUTABLE_FILE#", $"{asymmetricKeyTextBox.Text}\\DeviceSQL.dll")))
                        {
                            using (var sqlcommand = new SqlCommand(sqlStatment, sqlConnection))
                            {
                                try
                                {
                                    sqlcommand.ExecuteNonQuery();
                                }
                                catch(Exception ex)
                                {
                                    if(MessageBox.Show($"Error executing SQL statement: {ex.Message}.\r\nContinue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                                    {
                                        throw ex;
                                    }
                                }
                            }
                        }

                        sqlConnection.ChangeDatabase("DeviceSQL");

                        sqlTransaction = sqlConnection.BeginTransaction();

                        foreach (var sqlStatment in SplitSqlStatements(Properties.Resources.DeviceSQL_Install_Script_02))
                        {
                            using (var sqlcommand = new SqlCommand(sqlStatment, sqlConnection, sqlTransaction))
                            {
                                try
                                {
                                    sqlcommand.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    if (MessageBox.Show($"Error executing SQL statement: {ex.Message}.\r\nContinue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                                    {
                                        throw ex;
                                    }
                                }
                            }
                        }

                        sqlTransaction.Commit();

                        DialogResult = DialogResult.OK;

                        MessageBox.Show("Installation Complete");

                        Close();

                    }
                    catch (Exception ex)
                    {
                        sqlTransaction.Rollback();
                        throw ex;
                    }
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

            if (installButton.Enabled)
            {
                MessageBox.Show("Test Succeeded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
                userNameTextBox.Text = Environment.UserName;
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

        private void openAssymetricKeyFolderButton_Click(object sender, EventArgs e)
        {
            if (asymmetricKeyFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                asymmetricKeyTextBox.Text = asymmetricKeyFolderBrowserDialog.SelectedPath;
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

        private static IEnumerable<string> SplitSqlStatements(string sqlScript)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                    sqlScript,
                    @"^[\t\r\n]*GO[\t\r\n]*\d*[\t\r\n]*(?:--.*)?$",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));
        }

        #endregion

    }
}
