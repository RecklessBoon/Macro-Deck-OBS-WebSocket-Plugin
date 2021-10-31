
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
            this._main.OBS.Connected -= OBS_Connected;
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
            this.password = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.host = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.password.Cursor = System.Windows.Forms.Cursors.Hand;
            this.password.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.password.Icon = null;
            this.password.Location = new System.Drawing.Point(107, 80);
            this.password.Multiline = false;
            this.password.Name = "password";
            this.password.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.password.PasswordChar = true;
            this.password.PlaceHolderColor = System.Drawing.Color.Gray;
            this.password.PlaceHolderText = "";
            this.password.ReadOnly = false;
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
            this.host.Multiline = false;
            this.host.Name = "host";
            this.host.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.host.PasswordChar = false;
            this.host.PlaceHolderColor = System.Drawing.Color.Gray;
            this.host.PlaceHolderText = "";
            this.host.ReadOnly = false;
            this.host.SelectionStart = 0;
            this.host.Size = new System.Drawing.Size(295, 29);
            this.host.TabIndex = 3;
            this.host.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(4, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Host:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(4, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnOk.BorderRadius = 8;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnOk.Icon = null;
            this.btnOk.Location = new System.Drawing.Point(327, 115);
            this.btnOk.Name = "btnOk";
            this.btnOk.Progress = 0;
            this.btnOk.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnOk.Size = new System.Drawing.Size(75, 25);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // PluginConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 150);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.host);
            this.Controls.Add(this.password);
            this.Name = "PluginConfig";
            this.Text = "PluginConfiguration";
            this.Controls.SetChildIndex(this.password, 0);
            this.Controls.SetChildIndex(this.host, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedTextBox password;
        private RoundedTextBox host;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ButtonPrimary btnOk;
    }
}