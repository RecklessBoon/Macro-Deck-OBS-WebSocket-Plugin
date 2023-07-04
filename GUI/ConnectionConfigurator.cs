using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class ConnectionConfigurator : UserControl
    {

        public ConnectionConfigurator()
        {
            InitializeComponent();

            lblName.Text = PluginLanguageManager.PluginStrings.Name;
            lblHost.Text = PluginLanguageManager.PluginStrings.Host;
            lblPassword.Text = PluginLanguageManager.PluginStrings.Password;
        }

        public ConnectionConfig Value
        {
            get
            {
                return new ConnectionConfig
                {
                    name = name.Text,
                    host = host.Text,
                    password = password.Text,
                };
            }
        }
    }
}
