﻿namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class SaveReplayBufferConfigView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lblConnection = new System.Windows.Forms.Label();
            connectionSelector1 = new ConnectionSelector();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblConnection, 0, 0);
            tableLayoutPanel1.Controls.Add(connectionSelector1, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(569, 334);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblConnection
            // 
            lblConnection.AutoSize = true;
            lblConnection.Dock = System.Windows.Forms.DockStyle.Top;
            lblConnection.Location = new System.Drawing.Point(3, 3);
            lblConnection.Margin = new System.Windows.Forms.Padding(3);
            lblConnection.Name = "lblConnection";
            lblConnection.Size = new System.Drawing.Size(110, 23);
            lblConnection.TabIndex = 1;
            lblConnection.Text = "Connection:";
            // 
            // connectionSelector1
            // 
            connectionSelector1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            connectionSelector1.Location = new System.Drawing.Point(116, 0);
            connectionSelector1.Margin = new System.Windows.Forms.Padding(0);
            connectionSelector1.Name = "connectionSelector1";
            connectionSelector1.Size = new System.Drawing.Size(453, 26);
            connectionSelector1.TabIndex = 2;
            connectionSelector1.Value = null;
            // 
            // SaveReplayBufferConfigView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(3, 40, 3, 3);
            Name = "SaveReplayBufferConfigView";
            Padding = new System.Windows.Forms.Padding(5);
            Size = new System.Drawing.Size(579, 344);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblConnection;
        private ConnectionSelector connectionSelector1;
    }
}
