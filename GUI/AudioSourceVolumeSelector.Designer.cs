
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class AudioSourceVolumeSelector
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
            this.radioSet = new System.Windows.Forms.RadioButton();
            this.radioDecrease = new System.Windows.Forms.RadioButton();
            this.radioIncrease = new System.Windows.Forms.RadioButton();
            this.lblToBy = new System.Windows.Forms.Label();
            this.decibel = new System.Windows.Forms.NumericUpDown();
            this.lblDecibel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.decibel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReloadSources
            // 
            this.btnReloadSources.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnReloadSources.BorderRadius = 8;
            this.btnReloadSources.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReloadSources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadSources.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReloadSources.ForeColor = System.Drawing.Color.White;
            this.btnReloadSources.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.btnReloadSources.Icon = global::SuchByte.OBSWebSocketPlugin.Properties.Resources.reload;
            this.btnReloadSources.Location = new System.Drawing.Point(555, 57);
            this.btnReloadSources.Name = "btnReloadSources";
            this.btnReloadSources.Progress = 0;
            this.btnReloadSources.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.btnReloadSources.Size = new System.Drawing.Size(30, 30);
            this.btnReloadSources.TabIndex = 14;
            this.btnReloadSources.UseVisualStyleBackColor = true;
            // 
            // sourcesBox
            // 
            this.sourcesBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.sourcesBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sourcesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourcesBox.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sourcesBox.Icon = null;
            this.sourcesBox.Location = new System.Drawing.Point(248, 57);
            this.sourcesBox.Name = "sourcesBox";
            this.sourcesBox.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.sourcesBox.SelectedIndex = -1;
            this.sourcesBox.SelectedItem = null;
            this.sourcesBox.Size = new System.Drawing.Size(301, 30);
            this.sourcesBox.TabIndex = 13;
            // 
            // lblSource
            // 
            this.lblSource.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSource.Location = new System.Drawing.Point(129, 57);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(113, 30);
            this.lblSource.TabIndex = 12;
            this.lblSource.Text = "Source:";
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioSet
            // 
            this.radioSet.Checked = true;
            this.radioSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioSet.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioSet.Location = new System.Drawing.Point(193, 190);
            this.radioSet.Name = "radioSet";
            this.radioSet.Size = new System.Drawing.Size(119, 22);
            this.radioSet.TabIndex = 11;
            this.radioSet.TabStop = true;
            this.radioSet.Text = "Set";
            this.radioSet.UseVisualStyleBackColor = true;
            this.radioSet.CheckedChanged += new System.EventHandler(this.Method_CheckedChanged);
            // 
            // radioDecrease
            // 
            this.radioDecrease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioDecrease.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioDecrease.Location = new System.Drawing.Point(193, 151);
            this.radioDecrease.Name = "radioDecrease";
            this.radioDecrease.Size = new System.Drawing.Size(119, 22);
            this.radioDecrease.TabIndex = 10;
            this.radioDecrease.Text = "Decrease";
            this.radioDecrease.UseVisualStyleBackColor = true;
            this.radioDecrease.CheckedChanged += new System.EventHandler(this.Method_CheckedChanged);
            // 
            // radioIncrease
            // 
            this.radioIncrease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioIncrease.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioIncrease.Location = new System.Drawing.Point(193, 110);
            this.radioIncrease.Name = "radioIncrease";
            this.radioIncrease.Size = new System.Drawing.Size(119, 22);
            this.radioIncrease.TabIndex = 9;
            this.radioIncrease.Text = "Increase";
            this.radioIncrease.UseVisualStyleBackColor = true;
            this.radioIncrease.CheckedChanged += new System.EventHandler(this.Method_CheckedChanged);
            // 
            // lblToBy
            // 
            this.lblToBy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblToBy.Location = new System.Drawing.Point(318, 111);
            this.lblToBy.Name = "lblToBy";
            this.lblToBy.Size = new System.Drawing.Size(97, 102);
            this.lblToBy.TabIndex = 15;
            this.lblToBy.Text = "to";
            this.lblToBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // decibel
            // 
            this.decibel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.decibel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.decibel.ForeColor = System.Drawing.Color.White;
            this.decibel.Location = new System.Drawing.Point(421, 151);
            this.decibel.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.decibel.Minimum = new decimal(new int[] {
            96,
            0,
            0,
            -2147483648});
            this.decibel.Name = "decibel";
            this.decibel.Size = new System.Drawing.Size(56, 26);
            this.decibel.TabIndex = 16;
            // 
            // lblDecibel
            // 
            this.lblDecibel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDecibel.Location = new System.Drawing.Point(483, 151);
            this.lblDecibel.Name = "lblDecibel";
            this.lblDecibel.Size = new System.Drawing.Size(38, 26);
            this.lblDecibel.TabIndex = 17;
            this.lblDecibel.Text = "dB";
            this.lblDecibel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AudioSourceVolumeSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDecibel);
            this.Controls.Add(this.decibel);
            this.Controls.Add(this.lblToBy);
            this.Controls.Add(this.btnReloadSources);
            this.Controls.Add(this.sourcesBox);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.radioSet);
            this.Controls.Add(this.radioDecrease);
            this.Controls.Add(this.radioIncrease);
            this.Name = "AudioSourceVolumeSelector";
            ((System.ComponentModel.ISupportInitialize)(this.decibel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MacroDeck.GUI.CustomControls.ButtonPrimary btnReloadSources;
        private MacroDeck.GUI.CustomControls.RoundedComboBox sourcesBox;
        private System.Windows.Forms.Label lblSource;
        private RadioButton radioSet;
        private RadioButton radioDecrease;
        private RadioButton radioIncrease;
        private System.Windows.Forms.Label lblToBy;
        private System.Windows.Forms.NumericUpDown decibel;
        private System.Windows.Forms.Label lblDecibel;
    }
}
