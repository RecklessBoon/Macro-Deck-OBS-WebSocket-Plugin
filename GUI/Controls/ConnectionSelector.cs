using Newtonsoft.Json.Linq;
using SuchByte.OBSWebSocketPlugin.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class ConnectionSelector : UserControl
    {
        public ConnectionSelector()
        {
            InitializeComponent();
            connections.DataSource = PluginInstance.Main?.Connections?.ToList();
            connections.DisplayMember = "Key";
            connections.ValueMember = "Key";

            connections.SelectedIndexChanged += Connections_SelectedIndexChanged;
        }

        private void Connections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public string Value
        {
            get
            {
                return connections.SelectedValue?.ToString();
            }
            set
            {
                connections.SelectedValue = value ?? "";
            }
        }

        public event EventHandler ValueChanged;
    }
}
