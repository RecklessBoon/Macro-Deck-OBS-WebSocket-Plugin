namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class ConnectionConfigurator
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPassword = new System.Windows.Forms.Label();
            lblHost = new System.Windows.Forms.Label();
            host = new MacroDeck.GUI.CustomControls.RoundedTextBox();
            password = new MacroDeck.GUI.CustomControls.RoundedTextBox();
            lblName = new System.Windows.Forms.Label();
            name = new MacroDeck.GUI.CustomControls.RoundedTextBox();
            SuspendLayout();
            // 
            // lblPassword
            // 
            lblPassword.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblPassword.ForeColor = System.Drawing.Color.White;
            lblPassword.Location = new System.Drawing.Point(0, 103);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(97, 29);
            lblPassword.TabIndex = 9;
            lblPassword.Text = "Password:";
            lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHost
            // 
            lblHost.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblHost.ForeColor = System.Drawing.Color.White;
            lblHost.Location = new System.Drawing.Point(0, 53);
            lblHost.Name = "lblHost";
            lblHost.Size = new System.Drawing.Size(97, 29);
            lblHost.TabIndex = 8;
            lblHost.Text = "Host:";
            lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // host
            // 
            host.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            host.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            host.Cursor = System.Windows.Forms.Cursors.Hand;
            host.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            host.Icon = null;
            host.Location = new System.Drawing.Point(103, 53);
            host.MaxCharacters = 32767;
            host.Multiline = false;
            host.Name = "host";
            host.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            host.PasswordChar = false;
            host.PlaceHolderColor = System.Drawing.Color.Gray;
            host.PlaceHolderText = "";
            host.ReadOnly = false;
            host.ScrollBars = System.Windows.Forms.ScrollBars.None;
            host.SelectionStart = 0;
            host.Size = new System.Drawing.Size(332, 29);
            host.TabIndex = 2;
            host.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // password
            // 
            password.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            password.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            password.Cursor = System.Windows.Forms.Cursors.Hand;
            password.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            password.Icon = null;
            password.Location = new System.Drawing.Point(103, 103);
            password.MaxCharacters = 32767;
            password.Multiline = false;
            password.Name = "password";
            password.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            password.PasswordChar = true;
            password.PlaceHolderColor = System.Drawing.Color.Gray;
            password.PlaceHolderText = "";
            password.ReadOnly = false;
            password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            password.SelectionStart = 0;
            password.Size = new System.Drawing.Size(332, 29);
            password.TabIndex = 3;
            password.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblName
            // 
            lblName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblName.ForeColor = System.Drawing.Color.White;
            lblName.Location = new System.Drawing.Point(0, 3);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(97, 29);
            lblName.TabIndex = 11;
            lblName.Text = "Name:";
            lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // name
            // 
            name.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            name.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            name.Cursor = System.Windows.Forms.Cursors.Hand;
            name.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            name.Icon = null;
            name.Location = new System.Drawing.Point(103, 3);
            name.MaxCharacters = 32767;
            name.Multiline = false;
            name.Name = "name";
            name.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            name.PasswordChar = false;
            name.PlaceHolderColor = System.Drawing.Color.Gray;
            name.PlaceHolderText = "";
            name.ReadOnly = false;
            name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            name.SelectionStart = 0;
            name.Size = new System.Drawing.Size(332, 29);
            name.TabIndex = 1;
            name.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // ConnectionConfigurator
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            Controls.Add(name);
            Controls.Add(lblName);
            Controls.Add(lblPassword);
            Controls.Add(lblHost);
            Controls.Add(host);
            Controls.Add(password);
            Name = "ConnectionConfigurator";
            Size = new System.Drawing.Size(438, 135);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblHost;
        public MacroDeck.GUI.CustomControls.RoundedTextBox password;
        public MacroDeck.GUI.CustomControls.RoundedTextBox host;
        private System.Windows.Forms.Label lblName;
        public MacroDeck.GUI.CustomControls.RoundedTextBox name;
    }
}
