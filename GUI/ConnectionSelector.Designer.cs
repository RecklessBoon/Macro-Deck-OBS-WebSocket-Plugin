namespace SuchByte.OBSWebSocketPlugin.GUI
{
    partial class ConnectionSelector
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
            connections = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // connections
            // 
            connections.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            connections.DisplayMember = "Name";
            connections.Dock = System.Windows.Forms.DockStyle.Top;
            connections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            connections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            connections.ForeColor = System.Drawing.Color.White;
            connections.FormattingEnabled = true;
            connections.Location = new System.Drawing.Point(0, 0);
            connections.Name = "connections";
            connections.Size = new System.Drawing.Size(482, 23);
            connections.TabIndex = 0;
            connections.ValueMember = "Name";
            // 
            // ConnectionSelector
            // 
            BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            Controls.Add(connections);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "ConnectionSelector";
            Size = new System.Drawing.Size(482, 26);
            ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.ComboBox connections;
    }
}
