namespace DeviceSQL.DatabaseSetupUtility
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.asymmetricKeyPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openAssymetricKeyFolderButton = new System.Windows.Forms.Button();
            this.asymmetricKeyTextBox = new System.Windows.Forms.TextBox();
            this.asymmetricKeyNoteLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.installButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.authenticationTypePanel = new System.Windows.Forms.Panel();
            this.sqlAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
            this.windowsAutenticationRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.databaseInstanceComboBox = new System.Windows.Forms.ComboBox();
            this.asymmetricKeyFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.asymmetricKeyPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.authenticationTypePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // asymmetricKeyPanel
            // 
            this.asymmetricKeyPanel.Controls.Add(this.groupBox1);
            this.asymmetricKeyPanel.Controls.Add(this.asymmetricKeyNoteLabel);
            this.asymmetricKeyPanel.Location = new System.Drawing.Point(99, 131);
            this.asymmetricKeyPanel.Name = "asymmetricKeyPanel";
            this.asymmetricKeyPanel.Size = new System.Drawing.Size(380, 79);
            this.asymmetricKeyPanel.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.openAssymetricKeyFolderButton);
            this.groupBox1.Controls.Add(this.asymmetricKeyTextBox);
            this.groupBox1.Location = new System.Drawing.Point(21, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Asymmetric Key Folder";
            // 
            // openAssymetricKeyFolderButton
            // 
            this.openAssymetricKeyFolderButton.Location = new System.Drawing.Point(284, 19);
            this.openAssymetricKeyFolderButton.Name = "openAssymetricKeyFolderButton";
            this.openAssymetricKeyFolderButton.Size = new System.Drawing.Size(40, 20);
            this.openAssymetricKeyFolderButton.TabIndex = 3;
            this.openAssymetricKeyFolderButton.Text = "···";
            this.openAssymetricKeyFolderButton.UseVisualStyleBackColor = true;
            this.openAssymetricKeyFolderButton.Click += new System.EventHandler(this.openAssymetricKeyFolderButton_Click);
            // 
            // asymmetricKeyTextBox
            // 
            this.asymmetricKeyTextBox.Location = new System.Drawing.Point(6, 19);
            this.asymmetricKeyTextBox.Name = "asymmetricKeyTextBox";
            this.asymmetricKeyTextBox.ReadOnly = true;
            this.asymmetricKeyTextBox.Size = new System.Drawing.Size(272, 20);
            this.asymmetricKeyTextBox.TabIndex = 2;
            // 
            // asymmetricKeyNoteLabel
            // 
            this.asymmetricKeyNoteLabel.AutoSize = true;
            this.asymmetricKeyNoteLabel.Location = new System.Drawing.Point(24, 57);
            this.asymmetricKeyNoteLabel.Name = "asymmetricKeyNoteLabel";
            this.asymmetricKeyNoteLabel.Size = new System.Drawing.Size(289, 13);
            this.asymmetricKeyNoteLabel.TabIndex = 0;
            this.asymmetricKeyNoteLabel.Text = "* This folder must be accessible by the SQL Server instance";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.installButton);
            this.buttonPanel.Controls.Add(this.testButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Location = new System.Drawing.Point(147, 221);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(246, 29);
            this.buttonPanel.TabIndex = 19;
            // 
            // installButton
            // 
            this.installButton.Enabled = false;
            this.installButton.Location = new System.Drawing.Point(165, 3);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(75, 23);
            this.installButton.TabIndex = 3;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(3, 3);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 2;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(127, 74);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(272, 20);
            this.passwordTextBox.TabIndex = 18;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(27, 74);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 17;
            this.passwordLabel.Text = "Password:";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(127, 48);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.ReadOnly = true;
            this.userNameTextBox.Size = new System.Drawing.Size(272, 20);
            this.userNameTextBox.TabIndex = 16;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(27, 48);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(63, 13);
            this.userNameLabel.TabIndex = 15;
            this.userNameLabel.Text = "User Name:";
            // 
            // authenticationTypePanel
            // 
            this.authenticationTypePanel.Controls.Add(this.sqlAuthenticationRadioButton);
            this.authenticationTypePanel.Controls.Add(this.windowsAutenticationRadioButton);
            this.authenticationTypePanel.Location = new System.Drawing.Point(127, 100);
            this.authenticationTypePanel.Name = "authenticationTypePanel";
            this.authenticationTypePanel.Size = new System.Drawing.Size(272, 25);
            this.authenticationTypePanel.TabIndex = 14;
            // 
            // sqlAuthenticationRadioButton
            // 
            this.sqlAuthenticationRadioButton.AutoSize = true;
            this.sqlAuthenticationRadioButton.Location = new System.Drawing.Point(149, 3);
            this.sqlAuthenticationRadioButton.Name = "sqlAuthenticationRadioButton";
            this.sqlAuthenticationRadioButton.Size = new System.Drawing.Size(117, 17);
            this.sqlAuthenticationRadioButton.TabIndex = 1;
            this.sqlAuthenticationRadioButton.Text = "SQL Authentication";
            this.sqlAuthenticationRadioButton.UseVisualStyleBackColor = true;
            this.sqlAuthenticationRadioButton.CheckedChanged += new System.EventHandler(this.sqlAuthenticationRadioButton_CheckedChanged);
            // 
            // windowsAutenticationRadioButton
            // 
            this.windowsAutenticationRadioButton.AutoSize = true;
            this.windowsAutenticationRadioButton.Checked = true;
            this.windowsAutenticationRadioButton.Location = new System.Drawing.Point(3, 3);
            this.windowsAutenticationRadioButton.Name = "windowsAutenticationRadioButton";
            this.windowsAutenticationRadioButton.Size = new System.Drawing.Size(140, 17);
            this.windowsAutenticationRadioButton.TabIndex = 0;
            this.windowsAutenticationRadioButton.TabStop = true;
            this.windowsAutenticationRadioButton.Text = "Windows Authentication";
            this.windowsAutenticationRadioButton.UseVisualStyleBackColor = true;
            this.windowsAutenticationRadioButton.CheckedChanged += new System.EventHandler(this.windowsAutenticationRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Database Instance";
            // 
            // databaseInstanceComboBox
            // 
            this.databaseInstanceComboBox.FormattingEnabled = true;
            this.databaseInstanceComboBox.Location = new System.Drawing.Point(127, 17);
            this.databaseInstanceComboBox.Name = "databaseInstanceComboBox";
            this.databaseInstanceComboBox.Size = new System.Drawing.Size(272, 21);
            this.databaseInstanceComboBox.TabIndex = 12;
            this.databaseInstanceComboBox.DropDown += new System.EventHandler(this.databaseInstanceComboBox_DropDown);
            // 
            // asymmetricKeyFolderBrowserDialog
            // 
            this.asymmetricKeyFolderBrowserDialog.Description = "Select Asymmetric Key Folder";
            this.asymmetricKeyFolderBrowserDialog.SelectedPath = "C:\\DLLTemp";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(521, 273);
            this.Controls.Add(this.asymmetricKeyPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.authenticationTypePanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.databaseInstanceComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeviceSQL Database Setup";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.asymmetricKeyPanel.ResumeLayout(false);
            this.asymmetricKeyPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.authenticationTypePanel.ResumeLayout(false);
            this.authenticationTypePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel asymmetricKeyPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openAssymetricKeyFolderButton;
        private System.Windows.Forms.TextBox asymmetricKeyTextBox;
        private System.Windows.Forms.Label asymmetricKeyNoteLabel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Panel authenticationTypePanel;
        private System.Windows.Forms.RadioButton sqlAuthenticationRadioButton;
        private System.Windows.Forms.RadioButton windowsAutenticationRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox databaseInstanceComboBox;
        private System.Windows.Forms.FolderBrowserDialog asymmetricKeyFolderBrowserDialog;
    }
}

