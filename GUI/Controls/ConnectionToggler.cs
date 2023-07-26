using Newtonsoft.Json.Linq;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI.Utilities;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI.Controls
{

    public partial class ConnectionToggler : UserControl
    {
        private Connection _Connection;
        public Connection Connection
        {
            get
            {
                return _Connection;
            }
            set
            {
                this.InvokeIfRequired(() =>
                {
                    _Connection = value;
                    label1.Text = value.Name;
                    if (value.IsConnected)
                    {
                        OnConnected(this, EventArgs.Empty);
                    }
                    else
                    {
                        value.OBS.Connected += OnConnected;
                    }
                });
            }
        }

        public ConnectionToggler(Connection connection)
        {
            InitializeComponent();

            buttonPrimary1.Text = PluginLanguageManager.PluginStrings.Connect;

            Connection = connection;
        }

        public void OnConnected(object sender, EventArgs e)
        {
            buttonPrimary1.Text = PluginLanguageManager.PluginStrings.Disconnect;
            void handler(object sender, EventArgs args)
            {
                buttonPrimary1.Text = PluginLanguageManager.PluginStrings.Connect;
                Connection.Disposed -= handler;
            }

            Connection.Disposed += handler;
        }

        public void OnConnectButtonClicked(object sender, EventArgs e)
        {
            if (Connection.IsConnected)
            {
                Connection.Dispose();
            }
            else
            {
                var newConn = Connection.FromPrev(Connection);
                PluginInstance.Main.StoreConnectConnectionAsync(newConn);
                Connection = newConn;
            }
        }
    }
}
