
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class PluginConfig
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
            components = new System.ComponentModel.Container();
            password = new RoundedTextBox();
            host = new RoundedTextBox();
            lblHost = new System.Windows.Forms.Label();
            lblPassword = new System.Windows.Forms.Label();
            btnOk = new ButtonPrimary();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            SuspendLayout();
            // 
            // password
            // 
            password.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            password.Cursor = System.Windows.Forms.Cursors.Hand;
            password.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            password.Icon = null;
            password.Location = new System.Drawing.Point(107, 80);
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
            password.Size = new System.Drawing.Size(295, 29);
            password.TabIndex = 2;
            password.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // host
            // 
            host.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            host.Cursor = System.Windows.Forms.Cursors.Hand;
            host.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            host.Icon = null;
            host.Location = new System.Drawing.Point(107, 30);
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
            host.Size = new System.Drawing.Size(295, 29);
            host.TabIndex = 3;
            host.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblHost
            // 
            lblHost.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblHost.Location = new System.Drawing.Point(4, 30);
            lblHost.Name = "lblHost";
            lblHost.Size = new System.Drawing.Size(97, 29);
            lblHost.TabIndex = 4;
            lblHost.Text = "Host:";
            lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPassword
            // 
            lblPassword.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblPassword.Location = new System.Drawing.Point(4, 80);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(97, 29);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password:";
            lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOk.BorderRadius = 8;
            btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOk.ForeColor = System.Drawing.Color.White;
            btnOk.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnOk.Icon = null;
            btnOk.Location = new System.Drawing.Point(327, 128);
            btnOk.Name = "btnOk";
            btnOk.Progress = 0;
            btnOk.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnOk.Size = new System.Drawing.Size(75, 25);
            btnOk.TabIndex = 6;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.UseWindowsAccentColor = true;
            btnOk.Click += BtnOk_Click;
            // 
            // toolTip1
            // 
            toolTip1.AutomaticDelay = 300;
            toolTip1.AutoPopDelay = int.MaxValue;
            toolTip1.InitialDelay = 300;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 0;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(414, 165);
            Controls.Add(btnOk);
            Controls.Add(lblPassword);
            Controls.Add(lblHost);
            Controls.Add(host);
            Controls.Add(password);
            Location = new System.Drawing.Point(0, 0);
            Name = "PluginConfig";
            Text = "PluginConfiguration";
            Controls.SetChildIndex(password, 0);
            Controls.SetChildIndex(host, 0);
            Controls.SetChildIndex(lblHost, 0);
            Controls.SetChildIndex(lblPassword, 0);
            Controls.SetChildIndex(btnOk, 0);
            ResumeLayout(false);
        }

        #endregion

        private RoundedTextBox password;
        private RoundedTextBox host;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblPassword;
        private ButtonPrimary btnOk;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}