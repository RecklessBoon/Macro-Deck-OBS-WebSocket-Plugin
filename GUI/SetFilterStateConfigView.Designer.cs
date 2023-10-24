﻿
namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetFilterStateConfigView
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
            btnReloadSources = new MacroDeck.GUI.CustomControls.ButtonPrimary();
            sourcesBox = new MacroDeck.GUI.CustomControls.RoundedComboBox();
            lblSource = new System.Windows.Forms.Label();
            radioToggle = new System.Windows.Forms.RadioButton();
            radioShow = new System.Windows.Forms.RadioButton();
            radioHide = new System.Windows.Forms.RadioButton();
            btnReloadScenes = new MacroDeck.GUI.CustomControls.ButtonPrimary();
            scenesBox = new MacroDeck.GUI.CustomControls.RoundedComboBox();
            lblScene = new System.Windows.Forms.Label();
            filtersBox = new MacroDeck.GUI.CustomControls.RoundedComboBox();
            lblFilter = new System.Windows.Forms.Label();
            btnReloadFilters = new MacroDeck.GUI.CustomControls.ButtonPrimary();
            connectionSelector1 = new ConnectionSelector();
            lblConnection = new System.Windows.Forms.Label();
            SuspendLayout();
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
            btnReloadSources.Location = new System.Drawing.Point(586, 132);
            btnReloadSources.Name = "btnReloadSources";
            btnReloadSources.Progress = 0;
            btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadSources.Size = new System.Drawing.Size(30, 30);
            btnReloadSources.TabIndex = 14;
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
            sourcesBox.Location = new System.Drawing.Point(279, 132);
            sourcesBox.Name = "sourcesBox";
            sourcesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            sourcesBox.SelectedIndex = -1;
            sourcesBox.SelectedItem = null;
            sourcesBox.Size = new System.Drawing.Size(301, 30);
            sourcesBox.TabIndex = 13;
            sourcesBox.SelectedIndexChanged += sourcesBox_SelectedIndexChanged;
            // 
            // lblSource
            // 
            lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSource.Location = new System.Drawing.Point(160, 132);
            lblSource.Name = "lblSource";
            lblSource.Size = new System.Drawing.Size(113, 30);
            lblSource.TabIndex = 12;
            lblSource.Text = "Source:";
            lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioToggle
            // 
            radioToggle.Checked = true;
            radioToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            radioToggle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioToggle.Location = new System.Drawing.Point(266, 298);
            radioToggle.Name = "radioToggle";
            radioToggle.Size = new System.Drawing.Size(245, 22);
            radioToggle.TabIndex = 11;
            radioToggle.TabStop = true;
            radioToggle.Text = "Toggle";
            radioToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioToggle.UseVisualStyleBackColor = true;
            // 
            // radioShow
            // 
            radioShow.Cursor = System.Windows.Forms.Cursors.Hand;
            radioShow.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioShow.Location = new System.Drawing.Point(266, 260);
            radioShow.Name = "radioShow";
            radioShow.Size = new System.Drawing.Size(245, 22);
            radioShow.TabIndex = 10;
            radioShow.Text = "Show";
            radioShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioShow.UseVisualStyleBackColor = true;
            // 
            // radioHide
            // 
            radioHide.Cursor = System.Windows.Forms.Cursors.Hand;
            radioHide.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioHide.Location = new System.Drawing.Point(266, 221);
            radioHide.Name = "radioHide";
            radioHide.Size = new System.Drawing.Size(245, 22);
            radioHide.TabIndex = 9;
            radioHide.Text = "Hide";
            radioHide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            radioHide.UseVisualStyleBackColor = true;
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
            btnReloadScenes.Location = new System.Drawing.Point(586, 96);
            btnReloadScenes.Name = "btnReloadScenes";
            btnReloadScenes.Progress = 0;
            btnReloadScenes.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadScenes.Size = new System.Drawing.Size(30, 30);
            btnReloadScenes.TabIndex = 17;
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
            scenesBox.Location = new System.Drawing.Point(279, 96);
            scenesBox.Name = "scenesBox";
            scenesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            scenesBox.SelectedIndex = -1;
            scenesBox.SelectedItem = null;
            scenesBox.Size = new System.Drawing.Size(301, 30);
            scenesBox.TabIndex = 16;
            scenesBox.SelectedIndexChanged += ScenesBox_SelectedIndexChanged;
            // 
            // lblScene
            // 
            lblScene.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblScene.Location = new System.Drawing.Point(160, 96);
            lblScene.Name = "lblScene";
            lblScene.Size = new System.Drawing.Size(113, 30);
            lblScene.TabIndex = 15;
            lblScene.Text = "Scene:";
            lblScene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filtersBox
            // 
            filtersBox.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            filtersBox.Cursor = System.Windows.Forms.Cursors.Hand;
            filtersBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            filtersBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            filtersBox.Icon = null;
            filtersBox.Location = new System.Drawing.Point(279, 168);
            filtersBox.Name = "filtersBox";
            filtersBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            filtersBox.SelectedIndex = -1;
            filtersBox.SelectedItem = null;
            filtersBox.Size = new System.Drawing.Size(301, 30);
            filtersBox.TabIndex = 18;
            // 
            // lblFilter
            // 
            lblFilter.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblFilter.Location = new System.Drawing.Point(160, 168);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new System.Drawing.Size(113, 30);
            lblFilter.TabIndex = 19;
            lblFilter.Text = "Filter:";
            lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReloadFilters
            // 
            btnReloadFilters.BorderRadius = 8;
            btnReloadFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReloadFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReloadFilters.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnReloadFilters.ForeColor = System.Drawing.Color.White;
            btnReloadFilters.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnReloadFilters.Icon = Properties.Resources.reload;
            btnReloadFilters.Location = new System.Drawing.Point(586, 169);
            btnReloadFilters.Name = "btnReloadFilters";
            btnReloadFilters.Progress = 0;
            btnReloadFilters.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnReloadFilters.Size = new System.Drawing.Size(30, 30);
            btnReloadFilters.TabIndex = 20;
            btnReloadFilters.UseVisualStyleBackColor = true;
            btnReloadFilters.UseWindowsAccentColor = true;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(279, 67);
            connectionSelector1.Margin = new System.Windows.Forms.Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 21;
            connectionSelector1.Value = null;
            // 
            // lblConnection
            // 
            lblConnection.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblConnection.Location = new System.Drawing.Point(160, 63);
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new System.Drawing.Size(113, 30);
            lblConnection.TabIndex = 22;
            lblConnection.Text = "Connection:";
            lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetFilterStateConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lblConnection);
            Controls.Add(connectionSelector1);
            Controls.Add(btnReloadFilters);
            Controls.Add(lblFilter);
            Controls.Add(filtersBox);
            Controls.Add(btnReloadScenes);
            Controls.Add(scenesBox);
            Controls.Add(lblScene);
            Controls.Add(btnReloadSources);
            Controls.Add(sourcesBox);
            Controls.Add(lblSource);
            Controls.Add(radioToggle);
            Controls.Add(radioShow);
            Controls.Add(radioHide);
            Name = "SetFilterStateConfigView";
            ResumeLayout(false);
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
        private ConnectionSelector connectionSelector1;
        private System.Windows.Forms.Label lblConnection;
    }
}
