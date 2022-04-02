
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetTextValueConfigurator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetTextValueConfigurator));
            this.value = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.lblVariable = new System.Windows.Forms.Label();
            this.btnTemplateEditor = new SuchByte.MacroDeck.GUI.CustomControls.PictureButton();
            this.btnReloadScenes = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.scenesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.lblScene = new System.Windows.Forms.Label();
            this.btnReloadSources = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.sourcesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.lblSource = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnTemplateEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // value
            // 
            this.value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.value.Cursor = System.Windows.Forms.Cursors.Hand;
            this.value.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.value.Icon = null;
            this.value.Location = new System.Drawing.Point(298, 194);
            this.value.MaxCharacters = 32767;
            this.value.Multiline = false;
            this.value.Name = "value";
            this.value.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.value.PasswordChar = false;
            this.value.PlaceHolderColor = System.Drawing.Color.Gray;
            this.value.PlaceHolderText = "";
            this.value.ReadOnly = false;
            this.value.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.value.SelectionStart = 0;
            this.value.Size = new System.Drawing.Size(171, 25);
            this.value.TabIndex = 2;
            this.value.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblVariable
            // 
            this.lblVariable.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVariable.Location = new System.Drawing.Point(126, 196);
            this.lblVariable.Name = "lblVariable";
            this.lblVariable.Size = new System.Drawing.Size(166, 23);
            this.lblVariable.TabIndex = 4;
            this.lblVariable.Text = "Value";
            this.lblVariable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTemplateEditor
            // 
            this.btnTemplateEditor.BackColor = System.Drawing.Color.Transparent;
            this.btnTemplateEditor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTemplateEditor.BackgroundImage")));
            this.btnTemplateEditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTemplateEditor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTemplateEditor.HoverImage = ((System.Drawing.Image)(resources.GetObject("btnTemplateEditor.HoverImage")));
            this.btnTemplateEditor.Location = new System.Drawing.Point(470, 194);
            this.btnTemplateEditor.Name = "btnTemplateEditor";
            this.btnTemplateEditor.Size = new System.Drawing.Size(25, 25);
            this.btnTemplateEditor.TabIndex = 6;
            this.btnTemplateEditor.TabStop = false;
            this.btnTemplateEditor.Click += new System.EventHandler(this.btnTemplateEditor_Click);
            // 
            // btnReloadScenes
            // 
            this.btnReloadScenes.BorderRadius = 8;
            this.btnReloadScenes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadScenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadScenes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadScenes.ForeColor = System.Drawing.Color.White;
            this.btnReloadScenes.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadScenes.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadScenes.Location = new System.Drawing.Point(605, 113);
            this.btnReloadScenes.Name = "btnReloadScenes";
            this.btnReloadScenes.Progress = 0;
            this.btnReloadScenes.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadScenes.Size = new System.Drawing.Size(30, 30);
            this.btnReloadScenes.TabIndex = 23;
            this.btnReloadScenes.UseVisualStyleBackColor = true;
            this.btnReloadScenes.UseWindowsAccentColor = true;
            this.btnReloadScenes.Click += new System.EventHandler(this.BtnReloadScenes_Click);
            // 
            // scenesBox
            // 
            this.scenesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.scenesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scenesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scenesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.scenesBox.Icon = null;
            this.scenesBox.Location = new System.Drawing.Point(298, 113);
            this.scenesBox.Name = "scenesBox";
            this.scenesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.scenesBox.SelectedIndex = -1;
            this.scenesBox.SelectedItem = null;
            this.scenesBox.Size = new System.Drawing.Size(301, 30);
            this.scenesBox.TabIndex = 22;
            this.scenesBox.SelectedIndexChanged += new System.EventHandler(this.ScenesBox_SelectedIndexChanged);
            // 
            // lblScene
            // 
            this.lblScene.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblScene.Location = new System.Drawing.Point(179, 113);
            this.lblScene.Name = "lblScene";
            this.lblScene.Size = new System.Drawing.Size(113, 30);
            this.lblScene.TabIndex = 21;
            this.lblScene.Text = "Scene:";
            this.lblScene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReloadSources
            // 
            this.btnReloadSources.BorderRadius = 8;
            this.btnReloadSources.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadSources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadSources.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadSources.ForeColor = System.Drawing.Color.White;
            this.btnReloadSources.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadSources.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadSources.Location = new System.Drawing.Point(605, 149);
            this.btnReloadSources.Name = "btnReloadSources";
            this.btnReloadSources.Progress = 0;
            this.btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadSources.Size = new System.Drawing.Size(30, 30);
            this.btnReloadSources.TabIndex = 20;
            this.btnReloadSources.UseVisualStyleBackColor = true;
            this.btnReloadSources.UseWindowsAccentColor = true;
            this.btnReloadSources.Click += new System.EventHandler(this.BtnReloadSources_Click);
            // 
            // sourcesBox
            // 
            this.sourcesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sourcesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sourcesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourcesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sourcesBox.Icon = null;
            this.sourcesBox.Location = new System.Drawing.Point(298, 149);
            this.sourcesBox.Name = "sourcesBox";
            this.sourcesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.sourcesBox.SelectedIndex = -1;
            this.sourcesBox.SelectedItem = null;
            this.sourcesBox.Size = new System.Drawing.Size(301, 30);
            this.sourcesBox.TabIndex = 19;
            // 
            // lblSource
            // 
            this.lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSource.Location = new System.Drawing.Point(179, 149);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(113, 30);
            this.lblSource.TabIndex = 18;
            this.lblSource.Text = "Source:";
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetTextValueConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReloadScenes);
            this.Controls.Add(this.scenesBox);
            this.Controls.Add(this.lblScene);
            this.Controls.Add(this.btnReloadSources);
            this.Controls.Add(this.sourcesBox);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnTemplateEditor);
            this.Controls.Add(this.lblVariable);
            this.Controls.Add(this.value);
            this.Name = "SetTextValueConfigurator";
            ((System.ComponentModel.ISupportInitialize)(this.btnTemplateEditor)).EndInit();
            this.ResumeLayout(false);

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
    }
}
