
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetProfileConfigView
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            lblProfile = new System.Windows.Forms.Label();
            profilesBox = new RoundedComboBox();
            btnReloadProfiles = new ButtonPrimary();
            connectionSelector1 = new ConnectionSelector();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // lblProfile
            // 
            lblProfile.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblProfile.Location = new System.Drawing.Point(162, 107);
            lblProfile.Name = "lblProfile";
            lblProfile.Size = new System.Drawing.Size(99, 30);
            lblProfile.TabIndex = 0;
            lblProfile.Text = "Profile:";
            lblProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // profilesBox
            // 
            profilesBox.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            profilesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            profilesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            profilesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            profilesBox.Icon = null;
            profilesBox.Location = new System.Drawing.Point(267, 107);
            profilesBox.Name = "profilesBox";
            profilesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            profilesBox.SelectedIndex = -1;
            profilesBox.SelectedItem = null;
            profilesBox.Size = new System.Drawing.Size(301, 30);
            profilesBox.TabIndex = 1;
            // 
            // btnReloadProfiles
            // 
            btnReloadProfiles.BorderRadius = 8;
            btnReloadProfiles.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReloadProfiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReloadProfiles.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnReloadProfiles.ForeColor = System.Drawing.Color.White;
            btnReloadProfiles.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnReloadProfiles.Icon = Properties.Resources.reload;
            btnReloadProfiles.Location = new System.Drawing.Point(574, 107);
            btnReloadProfiles.Name = "btnReloadProfiles";
            btnReloadProfiles.Progress = 0;
            btnReloadProfiles.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadProfiles.Size = new System.Drawing.Size(30, 30);
            btnReloadProfiles.TabIndex = 2;
            btnReloadProfiles.UseVisualStyleBackColor = true;
            btnReloadProfiles.UseWindowsAccentColor = true;
            btnReloadProfiles.Click += BtnReloadProfiles_Click;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(267, 78);
            connectionSelector1.Margin = new System.Windows.Forms.Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 3;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(162, 74);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(99, 30);
            label1.TabIndex = 4;
            label1.Text = "Connection:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetProfileConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(connectionSelector1);
            Controls.Add(btnReloadProfiles);
            Controls.Add(profilesBox);
            Controls.Add(lblProfile);
            Name = "SetProfileConfigView";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblProfile;
        private RoundedComboBox profilesBox;
        private ButtonPrimary btnReloadProfiles;
        private ConnectionSelector connectionSelector1;
        private System.Windows.Forms.Label label1;
    }
}
