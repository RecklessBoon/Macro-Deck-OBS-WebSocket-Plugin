
namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SceneSelector
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
            this.btnReloadScenes = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.scenesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnReloadScenes
            // 
            this.btnReloadScenes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnReloadScenes.BorderRadius = 8;
            this.btnReloadScenes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadScenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadScenes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadScenes.ForeColor = System.Drawing.Color.White;
            this.btnReloadScenes.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadScenes.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadScenes.Location = new System.Drawing.Point(402, 120);
            this.btnReloadScenes.Name = "btnReloadScenes";
            this.btnReloadScenes.Progress = 0;
            this.btnReloadScenes.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadScenes.Size = new System.Drawing.Size(30, 30);
            this.btnReloadScenes.TabIndex = 5;
            this.btnReloadScenes.UseVisualStyleBackColor = true;
            this.btnReloadScenes.Click += new System.EventHandler(this.BtnReloadScenes_Click);
            // 
            // scenesBox
            // 
            this.scenesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.scenesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scenesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scenesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.scenesBox.Icon = null;
            this.scenesBox.Location = new System.Drawing.Point(95, 120);
            this.scenesBox.Name = "scenesBox";
            this.scenesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.scenesBox.SelectedIndex = -1;
            this.scenesBox.SelectedItem = null;
            this.scenesBox.Size = new System.Drawing.Size(301, 30);
            this.scenesBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Scene:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SceneSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReloadScenes);
            this.Controls.Add(this.scenesBox);
            this.Controls.Add(this.label1);
            this.Name = "SceneSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadScenes;
        private MacroDeck.GUI.CustomControls.RoundedComboBox scenesBox;
        private System.Windows.Forms.Label label1;
    }
}
