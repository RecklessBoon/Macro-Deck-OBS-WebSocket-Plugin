
namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class AudioSourceSelector
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
            this.radioToggle = new SuchByte.MacroDeck.GUI.CustomControls.TabRadioButton();
            this.radioUnmute = new SuchByte.MacroDeck.GUI.CustomControls.TabRadioButton();
            this.radioMute = new SuchByte.MacroDeck.GUI.CustomControls.TabRadioButton();
            this.btnReloadSources = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.sourcesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioToggle
            // 
            this.radioToggle.AutoSize = true;
            this.radioToggle.Checked = true;
            this.radioToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioToggle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioToggle.Location = new System.Drawing.Point(424, 70);
            this.radioToggle.Name = "radioToggle";
            this.radioToggle.Size = new System.Drawing.Size(70, 22);
            this.radioToggle.TabIndex = 5;
            this.radioToggle.TabStop = true;
            this.radioToggle.Text = "Toggle";
            this.radioToggle.UseVisualStyleBackColor = true;
            // 
            // radioUnmute
            // 
            this.radioUnmute.AutoSize = true;
            this.radioUnmute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioUnmute.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioUnmute.Location = new System.Drawing.Point(313, 70);
            this.radioUnmute.Name = "radioUnmute";
            this.radioUnmute.Size = new System.Drawing.Size(78, 22);
            this.radioUnmute.TabIndex = 4;
            this.radioUnmute.Text = "Unmute";
            this.radioUnmute.UseVisualStyleBackColor = true;
            // 
            // radioMute
            // 
            this.radioMute.AutoSize = true;
            this.radioMute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioMute.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioMute.Location = new System.Drawing.Point(221, 70);
            this.radioMute.Name = "radioMute";
            this.radioMute.Size = new System.Drawing.Size(59, 22);
            this.radioMute.TabIndex = 3;
            this.radioMute.Text = "Mute";
            this.radioMute.UseVisualStyleBackColor = true;
            // 
            // btnReloadSources
            // 
            this.btnReloadSources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnReloadSources.BorderRadius = 8;
            this.btnReloadSources.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadSources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadSources.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadSources.ForeColor = System.Drawing.Color.White;
            this.btnReloadSources.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadSources.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadSources.Location = new System.Drawing.Point(540, 120);
            this.btnReloadSources.Name = "btnReloadSources";
            this.btnReloadSources.Progress = 0;
            this.btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadSources.Size = new System.Drawing.Size(30, 30);
            this.btnReloadSources.TabIndex = 8;
            this.btnReloadSources.UseVisualStyleBackColor = true;
            this.btnReloadSources.Click += new System.EventHandler(this.BtnReloadSources_Click);
            // 
            // sourcesBox
            // 
            this.sourcesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sourcesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sourcesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourcesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sourcesBox.Icon = null;
            this.sourcesBox.Location = new System.Drawing.Point(233, 120);
            this.sourcesBox.Name = "sourcesBox";
            this.sourcesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.sourcesBox.SelectedIndex = -1;
            this.sourcesBox.SelectedItem = null;
            this.sourcesBox.Size = new System.Drawing.Size(301, 30);
            this.sourcesBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(144, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Source:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AudioSourceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReloadSources);
            this.Controls.Add(this.sourcesBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioToggle);
            this.Controls.Add(this.radioUnmute);
            this.Controls.Add(this.radioMute);
            this.Name = "AudioSourceSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MacroDeck.GUI.CustomControls.TabRadioButton radioToggle;
        private MacroDeck.GUI.CustomControls.TabRadioButton radioUnmute;
        private MacroDeck.GUI.CustomControls.TabRadioButton radioMute;
        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadSources;
        private MacroDeck.GUI.CustomControls.RoundedComboBox sourcesBox;
        private System.Windows.Forms.Label label1;
    }
}
