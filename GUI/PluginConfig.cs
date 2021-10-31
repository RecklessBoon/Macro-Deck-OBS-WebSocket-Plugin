using OBSWebsocketDotNet;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {

        Main _main;
        public PluginConfig(Main main)
        {
            this._main = main;
            InitializeComponent();

            List<Dictionary<string, string>> credentialsList = PluginCredentials.GetPluginCredentials(main);
            Dictionary<string, string> credentials = null;
            if (credentialsList != null && credentialsList.Count > 0)
            {
                credentials = credentialsList[0];
            }
            if (credentials != null)
            {
                this.host.Text = credentials["host"];
                this.password.Text = credentials["password"];
            } else
            {
                this.host.Text = "ws://127.0.0.1:4444";
            }

        }

        private void OBS_Connected(object sender, EventArgs e)
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>();
            credentials["host"] = this.host.Text;
            credentials["password"] = this.password.Text;
            PluginCredentials.SetCredentials(this._main, credentials);
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this._main.OBS.IsConnected)
            {
                this.Close();
                return;
            }
            try
            {
                this._main.OBS.Connected += OBS_Connected;
                this._main.OBS.Connect(this.host.Text, this.password.Text);
            }
            catch (AuthFailureException)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog("OBS Authentication failed", "Please make sure, you set the correct password", MessageBoxButtons.OK);
                }
                return;
            }
            catch (ErrorResponseException)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog("OBS Connection failed", "Please make sure, you set the correct host and OBS is running with the OBS-WebSocket plugin installed", MessageBoxButtons.OK);
                }
                return;
            }
        }
    }
}
