#region Imported Types

using System;
using System.Data;
using System.Data.Sql;
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

        }

        private void testButton_Click(object sender, EventArgs e)
        {

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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (windowsAutenticationRadioButton.Checked && databaseInstanceComboBox.Text?.Length > 0)
            {
                testButton.Enabled = true;
            }
            else if(sqlAuthenticationRadioButton.Checked && databaseInstanceComboBox.Text?.Length > 0)
            {
                testButton.Enabled = true;
            }
            else
            {
                testButton.Enabled = false;
            }
        }

        #endregion

    }
}

