
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetTextValueConfigView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetTextValueConfigView));
            value = new RoundedTextBox();
            lblVariable = new System.Windows.Forms.Label();
            btnTemplateEditor = new PictureButton();
            btnReloadScenes = new ButtonPrimary();
            scenesBox = new RoundedComboBox();
            lblScene = new System.Windows.Forms.Label();
            btnReloadSources = new ButtonPrimary();
            sourcesBox = new RoundedComboBox();
            lblSource = new System.Windows.Forms.Label();
            connectionSelector1 = new ConnectionSelector();
            lblConnection = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)btnTemplateEditor).BeginInit();
            SuspendLayout();
            // 
            // value
            // 
            value.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            value.Cursor = System.Windows.Forms.Cursors.Hand;
            value.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            value.Icon = null;
            value.Location = new System.Drawing.Point(298, 194);
            value.MaxCharacters = 32767;
            value.Multiline = false;
            value.Name = "value";
            value.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            value.PasswordChar = false;
            value.PlaceHolderColor = System.Drawing.Color.Gray;
            value.PlaceHolderText = "";
            value.ReadOnly = false;
            value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            value.SelectionStart = 0;
            value.Size = new System.Drawing.Size(171, 25);
            value.TabIndex = 2;
            value.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblVariable
            // 
            lblVariable.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblVariable.Location = new System.Drawing.Point(126, 196);
            lblVariable.Name = "lblVariable";
            lblVariable.Size = new System.Drawing.Size(166, 23);
            lblVariable.TabIndex = 4;
            lblVariable.Text = "Value";
            lblVariable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTemplateEditor
            // 
            btnTemplateEditor.BackColor = System.Drawing.Color.Transparent;
            btnTemplateEditor.BackgroundImage = (System.Drawing.Image)resources.GetObject("btnTemplateEditor.BackgroundImage");
            btnTemplateEditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnTemplateEditor.Cursor = System.Windows.Forms.Cursors.Hand;
            btnTemplateEditor.HoverImage = (System.Drawing.Image)resources.GetObject("btnTemplateEditor.HoverImage");
            btnTemplateEditor.Location = new System.Drawing.Point(470, 194);
            btnTemplateEditor.Name = "btnTemplateEditor";
            btnTemplateEditor.Size = new System.Drawing.Size(25, 25);
            btnTemplateEditor.TabIndex = 6;
            btnTemplateEditor.TabStop = false;
            btnTemplateEditor.Click += btnTemplateEditor_Click;
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
            btnReloadScenes.Location = new System.Drawing.Point(605, 113);
            btnReloadScenes.Name = "btnReloadScenes";
            btnReloadScenes.Progress = 0;
            btnReloadScenes.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadScenes.Size = new System.Drawing.Size(30, 30);
            btnReloadScenes.TabIndex = 23;
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
            scenesBox.Location = new System.Drawing.Point(298, 113);
            scenesBox.Name = "scenesBox";
            scenesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            scenesBox.SelectedIndex = -1;
            scenesBox.SelectedItem = null;
            scenesBox.Size = new System.Drawing.Size(301, 30);
            scenesBox.TabIndex = 22;
            scenesBox.SelectedIndexChanged += ScenesBox_SelectedIndexChanged;
            // 
            // lblScene
            // 
            lblScene.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblScene.Location = new System.Drawing.Point(179, 113);
            lblScene.Name = "lblScene";
            lblScene.Size = new System.Drawing.Size(113, 30);
            lblScene.TabIndex = 21;
            lblScene.Text = "Scene:";
            lblScene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReloadSources
            // 
            btnReloadSources.BorderRadius = 8;
            btnReloadSources.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReloadSources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReloadSources.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnReloadSources.ForeColor = System.Drawing.Color.White;
            btnReloadSources.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnReloadSources.Icon = Properties.Resources.reload;
            btnReloadSources.Location = new System.Drawing.Point(605, 149);
            btnReloadSources.Name = "btnReloadSources";
            btnReloadSources.Progress = 0;
            btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadSources.Size = new System.Drawing.Size(30, 30);
            btnReloadSources.TabIndex = 20;
            btnReloadSources.UseVisualStyleBackColor = true;
            btnReloadSources.UseWindowsAccentColor = true;
            btnReloadSources.Click += BtnReloadSources_Click;
            // 
            // sourcesBox
            // 
            sourcesBox.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            sourcesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            sourcesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            sourcesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            sourcesBox.Icon = null;
            sourcesBox.Location = new System.Drawing.Point(298, 149);
            sourcesBox.Name = "sourcesBox";
            sourcesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            sourcesBox.SelectedIndex = -1;
            sourcesBox.SelectedItem = null;
            sourcesBox.Size = new System.Drawing.Size(301, 30);
            sourcesBox.TabIndex = 19;
            // 
            // lblSource
            // 
            lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSource.Location = new System.Drawing.Point(179, 149);
            lblSource.Name = "lblSource";
            lblSource.Size = new System.Drawing.Size(113, 30);
            lblSource.TabIndex = 18;
            lblSource.Text = "Source:";
            lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(298, 84);
            connectionSelector1.Margin = new System.Windows.Forms.Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 24;
            connectionSelector1.Value = null;
            // 
            // lblConnection
            // 
            lblConnection.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblConnection.Location = new System.Drawing.Point(179, 84);
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new System.Drawing.Size(113, 26);
            lblConnection.TabIndex = 25;
            lblConnection.Text = "Connection:";
            lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetTextValueConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lblConnection);
            Controls.Add(connectionSelector1);
            Controls.Add(btnReloadScenes);
            Controls.Add(scenesBox);
            Controls.Add(lblScene);
            Controls.Add(btnReloadSources);
            Controls.Add(sourcesBox);
            Controls.Add(lblSource);
            Controls.Add(btnTemplateEditor);
            Controls.Add(lblVariable);
            Controls.Add(value);
            Name = "SetTextValueConfigView";
            ((System.ComponentModel.ISupportInitialize)btnTemplateEditor).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private RoundedTextBox value;
        private System.Windows.Forms.Label lblVariable;
        private PictureButton btnTemplateEditor;
        private ButtonPrimary btnReloadScenes;
        private RoundedComboBox scenesBox;
        private System.Windows.Forms.Label lblScene;
        private ButtonPrimary btnReloadSources;
        private RoundedComboBox sourcesBox;
        private System.Windows.Forms.Label lblSource;
        private ConnectionSelector connectionSelector1;
        private System.Windows.Forms.Label lblConnection;
    }
}
