
using SuchByte.MacroDeck.GUI.CustomControls;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class GenericStateConfigView
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
            radioStart = new RadioButton();
            radioStop = new RadioButton();
            radioToggle = new RadioButton();
            connectionSelector1 = new ConnectionSelector();
            label1 = new Label();
            SuspendLayout();
            // 
            // radioStart
            // 
            radioStart.Cursor = Cursors.Hand;
            radioStart.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioStart.Location = new System.Drawing.Point(267, 161);
            radioStart.Name = "radioStart";
            radioStart.Size = new System.Drawing.Size(245, 22);
            radioStart.TabIndex = 0;
            radioStart.Text = "Start";
            radioStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioStart.UseVisualStyleBackColor = true;
            // 
            // radioStop
            // 
            radioStop.Cursor = Cursors.Hand;
            radioStop.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioStop.Location = new System.Drawing.Point(267, 202);
            radioStop.Name = "radioStop";
            radioStop.Size = new System.Drawing.Size(245, 22);
            radioStop.TabIndex = 1;
            radioStop.Text = "Stop";
            radioStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioStop.UseVisualStyleBackColor = true;
            // 
            // radioToggle
            // 
            radioToggle.Checked = true;
            radioToggle.Cursor = Cursors.Hand;
            radioToggle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioToggle.Location = new System.Drawing.Point(267, 241);
            radioToggle.Name = "radioToggle";
            radioToggle.Size = new System.Drawing.Size(245, 22);
            radioToggle.TabIndex = 2;
            radioToggle.TabStop = true;
            radioToggle.Text = "Toggle";
            radioToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioToggle.UseVisualStyleBackColor = true;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(267, 112);
            connectionSelector1.Margin = new Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(338, 26);
            connectionSelector1.TabIndex = 3;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(136, 112);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(128, 26);
            label1.TabIndex = 4;
            label1.Text = "Connection:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GenericStateConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(connectionSelector1);
            Controls.Add(radioToggle);
            Controls.Add(radioStop);
            Controls.Add(radioStart);
            Name = "GenericStateConfigView";
            ResumeLayout(false);
        }

        #endregion

        private RadioButton radioStart;
        private RadioButton radioStop;
        private RadioButton radioToggle;
        private ConnectionSelector connectionSelector1;
        private Label label1;
    }
}
