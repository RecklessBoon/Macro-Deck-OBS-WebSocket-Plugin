
namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class FilterSelector
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
            this.btnReloadSources = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.sourcesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.radioToggle = new System.Windows.Forms.RadioButton();
            this.radioShow = new System.Windows.Forms.RadioButton();
            this.radioHide = new System.Windows.Forms.RadioButton();
            this.btnReloadScenes = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.scenesBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.lblScene = new System.Windows.Forms.Label();
            this.filtersBox = new SuchByte.MacroDeck.GUI.CustomControls.RoundedComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnReloadFilters = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.SuspendLayout();
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
            this.btnReloadSources.Location = new System.Drawing.Point(555, 76);
            this.btnReloadSources.Name = "btnReloadSources";
            this.btnReloadSources.Progress = 0;
            this.btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadSources.Size = new System.Drawing.Size(30, 30);
            this.btnReloadSources.TabIndex = 14;
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
            this.sourcesBox.Location = new System.Drawing.Point(248, 76);
            this.sourcesBox.Name = "sourcesBox";
            this.sourcesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.sourcesBox.SelectedIndex = -1;
            this.sourcesBox.SelectedItem = null;
            this.sourcesBox.Size = new System.Drawing.Size(301, 30);
            this.sourcesBox.TabIndex = 13;
            this.sourcesBox.SelectedIndexChanged += new System.EventHandler(this.sourcesBox_SelectedIndexChanged);
            // 
            // lblSource
            // 
            this.lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSource.Location = new System.Drawing.Point(129, 76);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(113, 30);
            this.lblSource.TabIndex = 12;
            this.lblSource.Text = "Source:";
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioToggle
            // 
            this.radioToggle.Checked = true;
            this.radioToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioToggle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioToggle.Location = new System.Drawing.Point(235, 242);
            this.radioToggle.Name = "radioToggle";
            this.radioToggle.Size = new System.Drawing.Size(245, 22);
            this.radioToggle.TabIndex = 11;
            this.radioToggle.TabStop = true;
            this.radioToggle.Text = "Toggle";
            this.radioToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioToggle.UseVisualStyleBackColor = true;
            // 
            // radioShow
            // 
            this.radioShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioShow.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioShow.Location = new System.Drawing.Point(235, 204);
            this.radioShow.Name = "radioShow";
            this.radioShow.Size = new System.Drawing.Size(245, 22);
            this.radioShow.TabIndex = 10;
            this.radioShow.Text = "Show";
            this.radioShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioShow.UseVisualStyleBackColor = true;
            // 
            // radioHide
            // 
            this.radioHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioHide.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioHide.Location = new System.Drawing.Point(235, 165);
            this.radioHide.Name = "radioHide";
            this.radioHide.Size = new System.Drawing.Size(245, 22);
            this.radioHide.TabIndex = 9;
            this.radioHide.Text = "Hide";
            this.radioHide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioHide.UseVisualStyleBackColor = true;
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
            this.btnReloadScenes.Location = new System.Drawing.Point(555, 40);
            this.btnReloadScenes.Name = "btnReloadScenes";
            this.btnReloadScenes.Progress = 0;
            this.btnReloadScenes.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadScenes.Size = new System.Drawing.Size(30, 30);
            this.btnReloadScenes.TabIndex = 17;
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
            this.scenesBox.Location = new System.Drawing.Point(248, 40);
            this.scenesBox.Name = "scenesBox";
            this.scenesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.scenesBox.SelectedIndex = -1;
            this.scenesBox.SelectedItem = null;
            this.scenesBox.Size = new System.Drawing.Size(301, 30);
            this.scenesBox.TabIndex = 16;
            this.scenesBox.SelectedIndexChanged += new System.EventHandler(this.ScenesBox_SelectedIndexChanged);
            // 
            // lblScene
            // 
            this.lblScene.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblScene.Location = new System.Drawing.Point(129, 40);
            this.lblScene.Name = "lblScene";
            this.lblScene.Size = new System.Drawing.Size(113, 30);
            this.lblScene.TabIndex = 15;
            this.lblScene.Text = "Scene:";
            this.lblScene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filtersBox
            // 
            this.filtersBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.filtersBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.filtersBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtersBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.filtersBox.Icon = null;
            this.filtersBox.Location = new System.Drawing.Point(248, 112);
            this.filtersBox.Name = "filtersBox";
            this.filtersBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.filtersBox.SelectedIndex = -1;
            this.filtersBox.SelectedItem = null;
            this.filtersBox.Size = new System.Drawing.Size(301, 30);
            this.filtersBox.TabIndex = 18;
            // 
            // lblFilter
            // 
            this.lblFilter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFilter.Location = new System.Drawing.Point(129, 112);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(113, 30);
            this.lblFilter.TabIndex = 19;
            this.lblFilter.Text = "Filter:";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReloadFilters
            // 
            this.btnReloadFilters.BorderRadius = 8;
            this.btnReloadFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadFilters.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadFilters.ForeColor = System.Drawing.Color.White;
            this.btnReloadFilters.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadFilters.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadFilters.Location = new System.Drawing.Point(555, 113);
            this.btnReloadFilters.Name = "btnReloadFilters";
            this.btnReloadFilters.Progress = 0;
            this.btnReloadFilters.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadFilters.Size = new System.Drawing.Size(30, 30);
            this.btnReloadFilters.TabIndex = 20;
            this.btnReloadFilters.UseVisualStyleBackColor = true;
            this.btnReloadFilters.UseWindowsAccentColor = true;
            // 
            // FilterSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReloadFilters);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.filtersBox);
            this.Controls.Add(this.btnReloadScenes);
            this.Controls.Add(this.scenesBox);
            this.Controls.Add(this.lblScene);
            this.Controls.Add(this.btnReloadSources);
            this.Controls.Add(this.sourcesBox);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.radioToggle);
            this.Controls.Add(this.radioShow);
            this.Controls.Add(this.radioHide);
            this.Name = "FilterSelector";
            this.ResumeLayout(false);

        }

        #endregion

        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadSources;
        private MacroDeck.GUI.CustomControls.RoundedComboBox sourcesBox;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.RadioButton radioToggle;
        private System.Windows.Forms.RadioButton radioShow;
        private System.Windows.Forms.RadioButton radioHide;
        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadScenes;
        private MacroDeck.GUI.CustomControls.RoundedComboBox scenesBox;
        private System.Windows.Forms.Label lblScene;
        private MacroDeck.GUI.CustomControls.RoundedComboBox filtersBox;
        private System.Windows.Forms.Label lblFilter;
        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadFilters;
    }
}
