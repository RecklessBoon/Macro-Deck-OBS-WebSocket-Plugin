
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetAudioMutedConfigView
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
            radioToggle = new RadioButton();
            radioUnmute = new RadioButton();
            radioMute = new RadioButton();
            btnReloadSources = new MacroDeck.GUI.CustomControls.ButtonPrimary();
            sourcesBox = new MacroDeck.GUI.CustomControls.RoundedComboBox();
            lblSource = new Label();
            label1 = new Label();
            connectionSelector1 = new ConnectionSelector();
            SuspendLayout();
            // 
            // radioToggle
            // 
            radioToggle.Checked = true;
            radioToggle.Cursor = Cursors.Hand;
            radioToggle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioToggle.Location = new System.Drawing.Point(235, 235);
            radioToggle.Name = "radioToggle";
            radioToggle.Size = new System.Drawing.Size(245, 22);
            radioToggle.TabIndex = 5;
            radioToggle.TabStop = true;
            radioToggle.Text = "Toggle";
            radioToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioToggle.UseVisualStyleBackColor = true;
            // 
            // radioUnmute
            // 
            radioUnmute.Cursor = Cursors.Hand;
            radioUnmute.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioUnmute.Location = new System.Drawing.Point(235, 196);
            radioUnmute.Name = "radioUnmute";
            radioUnmute.Size = new System.Drawing.Size(245, 22);
            radioUnmute.TabIndex = 4;
            radioUnmute.Text = "Unmute";
            radioUnmute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioUnmute.UseVisualStyleBackColor = true;
            // 
            // radioMute
            // 
            radioMute.Cursor = Cursors.Hand;
            radioMute.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioMute.Location = new System.Drawing.Point(235, 155);
            radioMute.Name = "radioMute";
            radioMute.Size = new System.Drawing.Size(245, 22);
            radioMute.TabIndex = 3;
            radioMute.Text = "Mute";
            radioMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioMute.UseVisualStyleBackColor = true;
            // 
            // btnReloadSources
            // 
            btnReloadSources.BorderRadius = 8;
            btnReloadSources.Cursor = Cursors.Hand;
            btnReloadSources.FlatStyle = FlatStyle.Flat;
            btnReloadSources.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnReloadSources.ForeColor = System.Drawing.Color.White;
            btnReloadSources.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnReloadSources.Icon = Properties.Resources.reload;
            btnReloadSources.Location = new System.Drawing.Point(555, 102);
            btnReloadSources.Name = "btnReloadSources";
            btnReloadSources.Progress = 0;
            btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadSources.Size = new System.Drawing.Size(30, 30);
            btnReloadSources.TabIndex = 8;
            btnReloadSources.UseVisualStyleBackColor = true;
            btnReloadSources.UseWindowsAccentColor = true;
            btnReloadSources.Click += BtnReloadSources_Click;
            // 
            // sourcesBox
            // 
            sourcesBox.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            sourcesBox.Cursor = Cursors.Hand;
            sourcesBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourcesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            sourcesBox.Icon = null;
            sourcesBox.Location = new System.Drawing.Point(248, 102);
            sourcesBox.Name = "sourcesBox";
            sourcesBox.Padding = new Padding(8, 2, 8, 2);
            sourcesBox.SelectedIndex = -1;
            sourcesBox.SelectedItem = null;
            sourcesBox.Size = new System.Drawing.Size(301, 30);
            sourcesBox.TabIndex = 7;
            // 
            // lblSource
            // 
            lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSource.Location = new System.Drawing.Point(129, 102);
            lblSource.Name = "lblSource";
            lblSource.Size = new System.Drawing.Size(113, 30);
            lblSource.TabIndex = 6;
            lblSource.Text = "Source:";
            lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(129, 56);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(113, 30);
            label1.TabIndex = 9;
            label1.Text = "Connection:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(248, 60);
            connectionSelector1.Margin = new Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 11;
            // 
            // AudioSourceSelector
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(connectionSelector1);
            Controls.Add(label1);
            Controls.Add(btnReloadSources);
            Controls.Add(sourcesBox);
            Controls.Add(lblSource);
            Controls.Add(radioToggle);
            Controls.Add(radioUnmute);
            Controls.Add(radioMute);
            Name = "AudioSourceSelector";
            ResumeLayout(false);
        }

        #endregion

        private RadioButton radioToggle;
        private RadioButton radioUnmute;
        private RadioButton radioMute;
        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadSources;
        private MacroDeck.GUI.CustomControls.RoundedComboBox sourcesBox;
        private Label lblSource;
        private Label label1;
        private ConnectionSelector connectionSelector1;
    }
}
