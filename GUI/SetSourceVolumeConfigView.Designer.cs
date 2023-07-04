
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SetSourceVolumeConfigView
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
            lblSource = new Label();
            radioSet = new RadioButton();
            radioDecrease = new RadioButton();
            radioIncrease = new RadioButton();
            lblToBy = new Label();
            decibel = new NumericUpDown();
            lblDecibel = new Label();
            connectionSelector1 = new ConnectionSelector();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)decibel).BeginInit();
            SuspendLayout();
            // 
            // btnReloadSources
            // 
            btnReloadSources.BorderRadius = 8;
            btnReloadSources.Cursor = Cursors.Hand;
            btnReloadSources.FlatStyle = FlatStyle.Flat;
            btnReloadSources.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnReloadSources.ForeColor = System.Drawing.Color.White;
            btnReloadSources.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnReloadSources.Icon = Properties.Resources.reload;
            btnReloadSources.Location = new System.Drawing.Point(585, 123);
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
            sourcesBox.Cursor = Cursors.Hand;
            sourcesBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourcesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            sourcesBox.Icon = null;
            sourcesBox.Location = new System.Drawing.Point(278, 123);
            sourcesBox.Name = "sourcesBox";
            sourcesBox.Padding = new Padding(8, 2, 8, 2);
            sourcesBox.SelectedIndex = -1;
            sourcesBox.SelectedItem = null;
            sourcesBox.Size = new System.Drawing.Size(301, 30);
            sourcesBox.TabIndex = 13;
            // 
            // lblSource
            // 
            lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblSource.Location = new System.Drawing.Point(159, 123);
            lblSource.Name = "lblSource";
            lblSource.Size = new System.Drawing.Size(113, 30);
            lblSource.TabIndex = 12;
            lblSource.Text = "Source:";
            lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioSet
            // 
            radioSet.Checked = true;
            radioSet.Cursor = Cursors.Hand;
            radioSet.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioSet.Location = new System.Drawing.Point(223, 256);
            radioSet.Name = "radioSet";
            radioSet.Size = new System.Drawing.Size(119, 22);
            radioSet.TabIndex = 11;
            radioSet.TabStop = true;
            radioSet.Text = "Set";
            radioSet.UseVisualStyleBackColor = true;
            radioSet.CheckedChanged += Method_CheckedChanged;
            // 
            // radioDecrease
            // 
            radioDecrease.Cursor = Cursors.Hand;
            radioDecrease.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioDecrease.Location = new System.Drawing.Point(223, 217);
            radioDecrease.Name = "radioDecrease";
            radioDecrease.Size = new System.Drawing.Size(119, 22);
            radioDecrease.TabIndex = 10;
            radioDecrease.Text = "Decrease";
            radioDecrease.UseVisualStyleBackColor = true;
            radioDecrease.CheckedChanged += Method_CheckedChanged;
            // 
            // radioIncrease
            // 
            radioIncrease.Cursor = Cursors.Hand;
            radioIncrease.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            radioIncrease.Location = new System.Drawing.Point(223, 176);
            radioIncrease.Name = "radioIncrease";
            radioIncrease.Size = new System.Drawing.Size(119, 22);
            radioIncrease.TabIndex = 9;
            radioIncrease.Text = "Increase";
            radioIncrease.UseVisualStyleBackColor = true;
            radioIncrease.CheckedChanged += Method_CheckedChanged;
            // 
            // lblToBy
            // 
            lblToBy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblToBy.Location = new System.Drawing.Point(348, 177);
            lblToBy.Name = "lblToBy";
            lblToBy.Size = new System.Drawing.Size(97, 102);
            lblToBy.TabIndex = 15;
            lblToBy.Text = "to";
            lblToBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // decibel
            // 
            decibel.BackColor = System.Drawing.Color.FromArgb(65, 65, 65);
            decibel.BorderStyle = BorderStyle.None;
            decibel.ForeColor = System.Drawing.Color.White;
            decibel.Location = new System.Drawing.Point(451, 217);
            decibel.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            decibel.Minimum = new decimal(new int[] { 96, 0, 0, int.MinValue });
            decibel.Name = "decibel";
            decibel.Size = new System.Drawing.Size(56, 26);
            decibel.TabIndex = 16;
            // 
            // lblDecibel
            // 
            lblDecibel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblDecibel.Location = new System.Drawing.Point(513, 217);
            lblDecibel.Name = "lblDecibel";
            lblDecibel.Size = new System.Drawing.Size(38, 26);
            lblDecibel.TabIndex = 17;
            lblDecibel.Text = "dB";
            lblDecibel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Location = new System.Drawing.Point(278, 84);
            connectionSelector1.Margin = new Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(337, 26);
            connectionSelector1.TabIndex = 18;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(159, 84);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(113, 26);
            label1.TabIndex = 19;
            label1.Text = "Connection:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetSourceVolumeConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(connectionSelector1);
            Controls.Add(lblDecibel);
            Controls.Add(decibel);
            Controls.Add(lblToBy);
            Controls.Add(btnReloadSources);
            Controls.Add(sourcesBox);
            Controls.Add(lblSource);
            Controls.Add(radioSet);
            Controls.Add(radioDecrease);
            Controls.Add(radioIncrease);
            Name = "SetSourceVolumeConfigView";
            ((System.ComponentModel.ISupportInitialize)decibel).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadSources;
        private MacroDeck.GUI.CustomControls.RoundedComboBox sourcesBox;
        private Label lblSource;
        private RadioButton radioSet;
        private RadioButton radioDecrease;
        private RadioButton radioIncrease;
        private Label lblToBy;
        private NumericUpDown decibel;
        private Label lblDecibel;
        private ConnectionSelector connectionSelector1;
        private Label label1;
    }
}
