namespace SuchByte.OBSWebSocketPlugin.GUI.Controls
{
    partial class ConnectionToggler
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
            label1 = new System.Windows.Forms.Label();
            buttonPrimary1 = new MacroDeck.GUI.CustomControls.ButtonPrimary();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonPrimary1, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(645, 44);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 3);
            label1.Margin = new System.Windows.Forms.Padding(3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(483, 38);
            label1.TabIndex = 2;
            label1.Text = "label1";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonPrimary1
            // 
            buttonPrimary1.BorderRadius = 8;
            buttonPrimary1.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonPrimary1.FlatAppearance.BorderSize = 0;
            buttonPrimary1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPrimary1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonPrimary1.ForeColor = System.Drawing.Color.White;
            buttonPrimary1.HoverColor = System.Drawing.Color.Empty;
            buttonPrimary1.Icon = null;
            buttonPrimary1.Location = new System.Drawing.Point(492, 3);
            buttonPrimary1.Name = "buttonPrimary1";
            buttonPrimary1.Progress = 0;
            buttonPrimary1.ProgressColor = System.Drawing.Color.FromArgb(0, 103, 205);
            buttonPrimary1.Size = new System.Drawing.Size(150, 38);
            buttonPrimary1.TabIndex = 1;
            buttonPrimary1.Text = "Connect";
            buttonPrimary1.UseVisualStyleBackColor = true;
            buttonPrimary1.UseWindowsAccentColor = true;
            buttonPrimary1.Click += OnConnectButtonClicked;
            // 
            // ConnectionToggler
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ForeColor = System.Drawing.Color.White;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ConnectionToggler";
            Size = new System.Drawing.Size(645, 44);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MacroDeck.GUI.CustomControls.ButtonPrimary buttonPrimary1;
        private System.Windows.Forms.Label label1;
    }
}
