
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class ProfileSelector
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
            this.label1 = new System.Windows.Forms.Label();
            this.profilesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.btnReloadProfiles = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profile:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // profilesBox
            // 
            this.profilesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.profilesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profilesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profilesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.profilesBox.Icon = null;
            this.profilesBox.Location = new System.Drawing.Point(95, 120);
            this.profilesBox.Name = "profilesBox";
            this.profilesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.profilesBox.SelectedIndex = -1;
            this.profilesBox.SelectedItem = null;
            this.profilesBox.Size = new System.Drawing.Size(301, 30);
            this.profilesBox.TabIndex = 1;
            // 
            // btnReloadProfiles
            // 
            this.btnReloadProfiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnReloadProfiles.BorderRadius = 8;
            this.btnReloadProfiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadProfiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadProfiles.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadProfiles.ForeColor = System.Drawing.Color.White;
            this.btnReloadProfiles.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadProfiles.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadProfiles.Location = new System.Drawing.Point(402, 120);
            this.btnReloadProfiles.Name = "btnReloadProfiles";
            this.btnReloadProfiles.Progress = 0;
            this.btnReloadProfiles.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadProfiles.Size = new System.Drawing.Size(30, 30);
            this.btnReloadProfiles.TabIndex = 2;
            this.btnReloadProfiles.UseVisualStyleBackColor = true;
            this.btnReloadProfiles.Click += new System.EventHandler(this.BtnReloadProfiles_Click);
            // 
            // SetProfileConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReloadProfiles);
            this.Controls.Add(this.profilesBox);
            this.Controls.Add(this.label1);
            this.Name = "SetProfileConfigurator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private RoundedComboBox profilesBox;
        private ButtonPrimary btnReloadProfiles;
    }
}
