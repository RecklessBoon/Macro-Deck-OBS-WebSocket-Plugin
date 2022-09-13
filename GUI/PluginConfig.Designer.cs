
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
            this.components = new System.ComponentModel.Container();
            this.password = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.host = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnOk = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.lblVersion = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.versionType_Auto = new SuchByte.MacroDeck.GUI.CustomControls.ButtonRadioButton();
            this.versionType_WS5 = new SuchByte.MacroDeck.GUI.CustomControls.ButtonRadioButton();
            this.versionType_WS4 = new SuchByte.MacroDeck.GUI.CustomControls.ButtonRadioButton();
            this.hlpVersion = new System.Windows.Forms.LinkLabel();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.password.Cursor = System.Windows.Forms.Cursors.Hand;
            this.password.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.password.Icon = null;
            this.password.Location = new System.Drawing.Point(107, 80);
            this.password.MaxCharacters = 32767;
            this.password.Multiline = false;
            this.password.Name = "password";
            this.password.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.password.PasswordChar = true;
            this.password.PlaceHolderColor = System.Drawing.Color.Gray;
            this.password.PlaceHolderText = "";
            this.password.ReadOnly = false;
            this.password.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.password.SelectionStart = 0;
            this.password.Size = new System.Drawing.Size(295, 29);
            this.password.TabIndex = 2;
            this.password.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // host
            // 
            this.host.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.host.Cursor = System.Windows.Forms.Cursors.Hand;
            this.host.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.host.Icon = null;
            this.host.Location = new System.Drawing.Point(107, 30);
            this.host.MaxCharacters = 32767;
            this.host.Multiline = false;
            this.host.Name = "host";
            this.host.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.host.PasswordChar = false;
            this.host.PlaceHolderColor = System.Drawing.Color.Gray;
            this.host.PlaceHolderText = "";
            this.host.ReadOnly = false;
            this.host.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.host.SelectionStart = 0;
            this.host.Size = new System.Drawing.Size(295, 29);
            this.host.TabIndex = 3;
            this.host.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblHost
            // 
            this.lblHost.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHost.Location = new System.Drawing.Point(4, 30);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(97, 29);
            this.lblHost.TabIndex = 4;
            this.lblHost.Text = "Host:";
            this.lblHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPassword.Location = new System.Drawing.Point(4, 80);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(97, 29);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            this.btnOk.BorderRadius = 8;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnOk.Icon = null;
            this.btnOk.Location = new System.Drawing.Point(327, 230);
            this.btnOk.Name = "btnOk";
            this.btnOk.Progress = 0;
            this.btnOk.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.UseWindowsAccentColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersion.Location = new System.Drawing.Point(4, 130);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(97, 29);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "Version:";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.versionType_Auto);
            this.flowLayoutPanel1.Controls.Add(this.versionType_WS5);
            this.flowLayoutPanel1.Controls.Add(this.versionType_WS4);
            this.flowLayoutPanel1.Controls.Add(this.hlpVersion);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(107, 130);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(295, 29);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // versionType_Auto
            // 
            this.versionType_Auto.AutoSize = true;
            this.versionType_Auto.BorderRadius = 8;
            this.versionType_Auto.Checked = true;
            this.versionType_Auto.Icon = null;
            this.versionType_Auto.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.versionType_Auto.Location = new System.Drawing.Point(3, 3);
            this.versionType_Auto.Name = "versionType_Auto";
            this.versionType_Auto.Size = new System.Drawing.Size(51, 20);
            this.versionType_Auto.TabIndex = 11;
            this.versionType_Auto.TabStop = true;
            this.versionType_Auto.Text = "Auto";
            this.versionType_Auto.UseVisualStyleBackColor = true;
            this.versionType_Auto.CheckedChanged += new System.EventHandler(this.OnVersionType_AutoCheckedChanged);
            // 
            // versionType_WS5
            // 
            this.versionType_WS5.AutoSize = true;
            this.versionType_WS5.BorderRadius = 8;
            this.versionType_WS5.Icon = null;
            this.versionType_WS5.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.versionType_WS5.Location = new System.Drawing.Point(60, 3);
            this.versionType_WS5.Name = "versionType_WS5";
            this.versionType_WS5.Size = new System.Drawing.Size(100, 20);
            this.versionType_WS5.TabIndex = 12;
            this.versionType_WS5.Text = "WebSocket 5";
            this.versionType_WS5.UseVisualStyleBackColor = true;
            // 
            // versionType_WS4
            // 
            this.versionType_WS4.AutoSize = true;
            this.versionType_WS4.BorderRadius = 8;
            this.versionType_WS4.Icon = null;
            this.versionType_WS4.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.versionType_WS4.Location = new System.Drawing.Point(166, 3);
            this.versionType_WS4.Name = "versionType_WS4";
            this.versionType_WS4.Size = new System.Drawing.Size(100, 20);
            this.versionType_WS4.TabIndex = 13;
            this.versionType_WS4.Text = "WebSocket 4";
            this.versionType_WS4.UseVisualStyleBackColor = true;
            // 
            // hlpVersion
            // 
            this.hlpVersion.ActiveLinkColor = System.Drawing.Color.CornflowerBlue;
            this.hlpVersion.AutoSize = true;
            this.hlpVersion.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.hlpVersion.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.hlpVersion.Location = new System.Drawing.Point(272, 4);
            this.hlpVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.hlpVersion.Name = "hlpVersion";
            this.hlpVersion.Size = new System.Drawing.Size(13, 16);
            this.hlpVersion.TabIndex = 12;
            this.hlpVersion.TabStop = true;
            this.hlpVersion.Text = "?";
            this.hlpVersion.VisitedLinkColor = System.Drawing.Color.CornflowerBlue;
            // 
            // lblTimeout
            // 
            this.lblTimeout.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTimeout.Location = new System.Drawing.Point(4, 175);
            this.lblTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(97, 29);
            this.lblTimeout.TabIndex = 10;
            this.lblTimeout.Text = "Timeout:";
            this.lblTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numTimeout
            // 
            this.numTimeout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.numTimeout.ForeColor = System.Drawing.Color.White;
            this.numTimeout.Location = new System.Drawing.Point(107, 180);
            this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(295, 23);
            this.numTimeout.TabIndex = 11;
            this.numTimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            this.toolTip1.AutoPopDelay = 2147483647;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 0;
            // 
            // PluginConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 267);
            this.Controls.Add(this.numTimeout);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblHost);
            this.Controls.Add(this.host);
            this.Controls.Add(this.password);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PluginConfig";
            this.Text = "PluginConfiguration";
            this.Controls.SetChildIndex(this.password, 0);
            this.Controls.SetChildIndex(this.host, 0);
            this.Controls.SetChildIndex(this.lblHost, 0);
            this.Controls.SetChildIndex(this.lblPassword, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.lblTimeout, 0);
            this.Controls.SetChildIndex(this.numTimeout, 0);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedTextBox password;
        private RoundedTextBox host;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblPassword;
        private ButtonPrimary btnOk;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ButtonRadioButton versionType_Auto;
        private ButtonRadioButton versionType_WS5;
        private ButtonRadioButton versionType_WS4;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.LinkLabel hlpVersion;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}