
namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetSceneConfigView
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
            btnReloadScenes = new MacroDeck.GUI.CustomControls.ButtonPrimary();
            scenesBox = new MacroDeck.GUI.CustomControls.RoundedComboBox();
            lblScene = new System.Windows.Forms.Label();
            connectionSelector1 = new ConnectionSelector();
            lblConnection = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // btnReloadScenes
            // 
            btnReloadScenes.BorderRadius = 8;
            btnReloadScenes.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReloadScenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReloadScenes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnReloadScenes.ForeColor = System.Drawing.Color.White;
            btnReloadScenes.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnReloadScenes.Icon = Properties.Resources.reload;
            btnReloadScenes.Location = new System.Drawing.Point(605, 116);
            btnReloadScenes.Name = "btnReloadScenes";
            btnReloadScenes.Progress = 0;
            btnReloadScenes.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadScenes.Size = new System.Drawing.Size(30, 30);
            btnReloadScenes.TabIndex = 5;
            btnReloadScenes.UseVisualStyleBackColor = true;
            btnReloadScenes.UseWindowsAccentColor = true;
            btnReloadScenes.Click += BtnReloadScenes_Click;
            // 
            // scenesBox
            // 
            scenesBox.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            scenesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            scenesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            scenesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            scenesBox.Icon = null;
            scenesBox.Location = new System.Drawing.Point(298, 116);
            scenesBox.Name = "scenesBox";
            scenesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            scenesBox.SelectedIndex = -1;
            scenesBox.SelectedItem = null;
            scenesBox.Size = new System.Drawing.Size(301, 30);
            scenesBox.TabIndex = 4;
            // 
            // lblScene
            // 
            lblScene.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblScene.Location = new System.Drawing.Point(161, 116);
            lblScene.Name = "lblScene";
            lblScene.Size = new System.Drawing.Size(131, 30);
            lblScene.TabIndex = 3;
            lblScene.Text = "Scene:";
            lblScene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(298, 81);
            connectionSelector1.Margin = new System.Windows.Forms.Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 6;
            connectionSelector1.Value = null;
            // 
            // lblConnection
            // 
            lblConnection.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblConnection.Location = new System.Drawing.Point(161, 81);
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new System.Drawing.Size(131, 26);
            lblConnection.TabIndex = 7;
            lblConnection.Text = "Connection:";
            lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetSceneConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lblConnection);
            Controls.Add(connectionSelector1);
            Controls.Add(btnReloadScenes);
            Controls.Add(scenesBox);
            Controls.Add(lblScene);
            Name = "SetSceneConfigView";
            ResumeLayout(false);
        }

        #endregion

        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadScenes;
        private MacroDeck.GUI.CustomControls.RoundedComboBox scenesBox;
        private System.Windows.Forms.Label lblScene;
        private ConnectionSelector connectionSelector1;
        private System.Windows.Forms.Label lblConnection;
    }
}
