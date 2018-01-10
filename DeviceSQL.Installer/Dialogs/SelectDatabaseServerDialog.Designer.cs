namespace DeviceSQL.Installer.Dialogs
{
    partial class SelectDatabaseServerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDatabaseServerDialog));
            this.databaseInstanceComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.installButton = new System.Windows.Forms.Button();
            this.authenticationTypePanel = new System.Windows.Forms.Panel();
            this.windowsAutenticationRadioButton = new System.Windows.Forms.RadioButton();
            this.sqlAuthenticationRadioButton = new System.Windows.Forms.RadioButton();
            this.cancelButton = new System.Windows.Forms.Button();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.authenticationTypePanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // databaseInstanceComboBox
            // 
            this.databaseInstanceComboBox.FormattingEnabled = true;
            this.databaseInstanceComboBox.Location = new System.Drawing.Point(122, 20);
            this.databaseInstanceComboBox.Name = "databaseInstanceComboBox";
            this.databaseInstanceComboBox.Size = new System.Drawing.Size(272, 21);
            this.databaseInstanceComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database Instance";
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
            // installButton
            // 
            this.installButton.Location = new System.Drawing.Point(165, 3);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(75, 23);
            this.installButton.TabIndex = 3;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // authenticationTypePanel
            // 
            this.authenticationTypePanel.Controls.Add(this.sqlAuthenticationRadioButton);
            this.authenticationTypePanel.Controls.Add(this.windowsAutenticationRadioButton);
            this.authenticationTypePanel.Location = new System.Drawing.Point(122, 103);
            this.authenticationTypePanel.Name = "authenticationTypePanel";
            this.authenticationTypePanel.Size = new System.Drawing.Size(272, 25);
            this.authenticationTypePanel.TabIndex = 4;
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
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(84, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(22, 51);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(63, 13);
            this.userNameLabel.TabIndex = 6;
            this.userNameLabel.Text = "User Name:";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(122, 51);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.ReadOnly = true;
            this.userNameTextBox.Size = new System.Drawing.Size(272, 20);
            this.userNameTextBox.TabIndex = 7;
            this.userNameTextBox.TextChanged += new System.EventHandler(this.userNameTextBox_TextChanged);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(122, 77);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.ReadOnly = true;
            this.passwordTextBox.Size = new System.Drawing.Size(272, 20);
            this.passwordTextBox.TabIndex = 9;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(22, 77);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 8;
            this.passwordLabel.Text = "Password:";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.installButton);
            this.buttonPanel.Controls.Add(this.testButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Location = new System.Drawing.Point(95, 156);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(246, 29);
            this.buttonPanel.TabIndex = 10;
            // 
            // SelectDatabaseServerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 212);
            this.ControlBox = false;
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
            this.Name = "SelectDatabaseServerDialog";
            this.Text = "Select Database Server";
            this.Load += new System.EventHandler(this.SelectDatabaseServerDialog_Load);
            this.authenticationTypePanel.ResumeLayout(false);
            this.authenticationTypePanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox databaseInstanceComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Panel authenticationTypePanel;
        private System.Windows.Forms.RadioButton sqlAuthenticationRadioButton;
        private System.Windows.Forms.RadioButton windowsAutenticationRadioButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Panel buttonPanel;
    }
}