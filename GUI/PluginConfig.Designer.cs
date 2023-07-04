
using SuchByte.MacroDeck.GUI.CustomControls;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class PluginConfig
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnOk = new ButtonPrimary();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            formLayout = new System.Windows.Forms.TableLayoutPanel();
            repeatingLayout = new System.Windows.Forms.TableLayoutPanel();
            connectionConfig_1 = new ConnectionConfigurator();
            btnExit = new ButtonPrimary();
            btnAdd = new ButtonPrimary();
            formLayout.SuspendLayout();
            repeatingLayout.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOk.BorderRadius = 8;
            btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOk.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnOk.ForeColor = System.Drawing.Color.White;
            btnOk.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnOk.Icon = null;
            btnOk.Location = new System.Drawing.Point(402, 370);
            btnOk.Name = "btnOk";
            btnOk.Progress = 0;
            btnOk.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnOk.Size = new System.Drawing.Size(75, 25);
            btnOk.TabIndex = 6;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.UseWindowsAccentColor = true;
            btnOk.Click += BtnOk_Click;
            // 
            // toolTip1
            // 
            toolTip1.AutomaticDelay = 300;
            toolTip1.AutoPopDelay = int.MaxValue;
            toolTip1.InitialDelay = 300;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 0;
            // 
            // formLayout
            // 
            formLayout.AutoSize = true;
            formLayout.ColumnCount = 1;
            formLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            formLayout.Controls.Add(repeatingLayout, 1, 0);
            formLayout.Controls.Add(btnExit, 0, 0);
            formLayout.Controls.Add(btnAdd, 2, 1);
            formLayout.Controls.Add(btnOk, 3, 1);
            formLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            formLayout.Location = new System.Drawing.Point(1, 1);
            formLayout.MaximumSize = new System.Drawing.Size(0, 600);
            formLayout.Name = "formLayout";
            formLayout.RowCount = 3;
            formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            formLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            formLayout.Size = new System.Drawing.Size(480, 398);
            formLayout.TabIndex = 7;
            // 
            // repeatingLayout
            // 
            repeatingLayout.AutoScroll = true;
            repeatingLayout.AutoSize = true;
            repeatingLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            repeatingLayout.ColumnCount = 2;
            repeatingLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            repeatingLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            repeatingLayout.Controls.Add(connectionConfig_1, 0, 0);
            repeatingLayout.Dock = System.Windows.Forms.DockStyle.Top;
            repeatingLayout.Location = new System.Drawing.Point(3, 43);
            repeatingLayout.MaximumSize = new System.Drawing.Size(0, 600);
            repeatingLayout.Name = "repeatingLayout";
            repeatingLayout.Padding = new System.Windows.Forms.Padding(3);
            repeatingLayout.RowCount = 1;
            repeatingLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            repeatingLayout.Size = new System.Drawing.Size(474, 128);
            repeatingLayout.TabIndex = 7;
            repeatingLayout.CellPaint += repeatingLayout_CellPaint;
            // 
            // connectionConfig_1
            // 
            connectionConfig_1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            connectionConfig_1.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connectionConfig_1.Location = new System.Drawing.Point(16, 16);
            connectionConfig_1.Margin = new System.Windows.Forms.Padding(13);
            connectionConfig_1.Name = "connectionConfig_1";
            connectionConfig_1.Padding = new System.Windows.Forms.Padding(5);
            connectionConfig_1.Size = new System.Drawing.Size(335, 96);
            connectionConfig_1.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnExit.BorderRadius = 8;
            btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnExit.ForeColor = System.Drawing.Color.White;
            btnExit.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnExit.Icon = null;
            btnExit.Location = new System.Drawing.Point(442, 3);
            btnExit.Name = "btnExit";
            btnExit.Progress = 0;
            btnExit.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnExit.Size = new System.Drawing.Size(35, 34);
            btnExit.TabIndex = 10;
            btnExit.Text = "X";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.UseWindowsAccentColor = true;
            btnExit.Click += buttonPrimary1_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAdd.BorderRadius = 8;
            btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.HoverColor = System.Drawing.Color.FromArgb(0, 89, 184);
            btnAdd.Icon = null;
            btnAdd.Location = new System.Drawing.Point(382, 321);
            btnAdd.Name = "btnAdd";
            btnAdd.Progress = 0;
            btnAdd.ProgressColor = System.Drawing.Color.FromArgb(0, 46, 94);
            btnAdd.Size = new System.Drawing.Size(95, 25);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "+";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.UseWindowsAccentColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new System.Drawing.Size(0, 150);
            AutoSize = true;
            ClientSize = new System.Drawing.Size(482, 400);
            Controls.Add(formLayout);
            Location = new System.Drawing.Point(0, 0);
            MaximumSize = new System.Drawing.Size(1000, 400);
            Name = "PluginConfig";
            Text = "PluginConfiguration";
            Controls.SetChildIndex(formLayout, 0);
            formLayout.ResumeLayout(false);
            formLayout.PerformLayout();
            repeatingLayout.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ButtonPrimary btnOk;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel formLayout;
        private System.Windows.Forms.TableLayoutPanel repeatingLayout;
        private ButtonPrimary btnAdd;
        private ButtonPrimary btnExit;
        private ConnectionConfigurator connectionConfig_1;
    }
}