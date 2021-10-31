
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class StateSelector
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
            this.radioStart = new SuchByte.MacroDeck.GUI.CustomControls.TabRadioButton();
            this.radioStop = new SuchByte.MacroDeck.GUI.CustomControls.TabRadioButton();
            this.radioToggle = new SuchByte.MacroDeck.GUI.CustomControls.TabRadioButton();
            this.SuspendLayout();
            // 
            // radioStart
            // 
            this.radioStart.AutoSize = true;
            this.radioStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioStart.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioStart.Location = new System.Drawing.Point(237, 124);
            this.radioStart.Name = "radioStart";
            this.radioStart.Size = new System.Drawing.Size(57, 22);
            this.radioStart.TabIndex = 0;
            this.radioStart.Text = "Start";
            this.radioStart.UseVisualStyleBackColor = true;
            // 
            // radioStop
            // 
            this.radioStop.AutoSize = true;
            this.radioStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioStop.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioStop.Location = new System.Drawing.Point(322, 124);
            this.radioStop.Name = "radioStop";
            this.radioStop.Size = new System.Drawing.Size(55, 22);
            this.radioStop.TabIndex = 1;
            this.radioStop.Text = "Stop";
            this.radioStop.UseVisualStyleBackColor = true;
            // 
            // radioToggle
            // 
            this.radioToggle.AutoSize = true;
            this.radioToggle.Checked = true;
            this.radioToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioToggle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioToggle.Location = new System.Drawing.Point(407, 124);
            this.radioToggle.Name = "radioToggle";
            this.radioToggle.Size = new System.Drawing.Size(70, 22);
            this.radioToggle.TabIndex = 2;
            this.radioToggle.TabStop = true;
            this.radioToggle.Text = "Toggle";
            this.radioToggle.UseVisualStyleBackColor = true;
            // 
            // SetRecordingStateConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioToggle);
            this.Controls.Add(this.radioStop);
            this.Controls.Add(this.radioStart);
            this.Name = "SetRecordingStateConfigurator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabRadioButton radioStart;
        private TabRadioButton radioStop;
        private TabRadioButton radioToggle;
    }
}
