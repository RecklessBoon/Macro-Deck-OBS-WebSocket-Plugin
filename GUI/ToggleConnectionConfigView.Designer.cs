
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class ToggleConnectionConfigView
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
            connectionSelector1 = new ConnectionSelector();
            rbAllConnections = new RadioButton();
            rbSingleConnection = new RadioButton();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(289, 116);
            connectionSelector1.Margin = new Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 11;
            // 
            // rbAllConnections
            // 
            rbAllConnections.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rbAllConnections.Location = new System.Drawing.Point(3, 3);
            rbAllConnections.Name = "rbAllConnections";
            rbAllConnections.Size = new System.Drawing.Size(134, 19);
            rbAllConnections.TabIndex = 12;
            rbAllConnections.TabStop = true;
            rbAllConnections.Text = "All Connections";
            rbAllConnections.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            rbAllConnections.UseVisualStyleBackColor = true;
            // 
            // rbSingleConnection
            // 
            rbSingleConnection.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            rbSingleConnection.Location = new System.Drawing.Point(3, 28);
            rbSingleConnection.Name = "rbSingleConnection";
            rbSingleConnection.Size = new System.Drawing.Size(134, 22);
            rbSingleConnection.TabIndex = 13;
            rbSingleConnection.TabStop = true;
            rbSingleConnection.Text = "Connection:";
            rbSingleConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            rbSingleConnection.UseVisualStyleBackColor = true;
            rbSingleConnection.CheckedChanged += rbSingleConnection_CheckedChanged;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(rbAllConnections);
            panel1.Controls.Add(rbSingleConnection);
            panel1.Location = new System.Drawing.Point(146, 89);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(140, 53);
            panel1.TabIndex = 14;
            // 
            // ToggleConnectionConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(connectionSelector1);
            Name = "ToggleConnectionConfigView";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ConnectionSelector connectionSelector1;
        private RadioButton rbAllConnections;
        private RadioButton rbSingleConnection;
        private Panel panel1;
    }
}
