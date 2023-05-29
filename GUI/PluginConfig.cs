using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {
        public PluginConfig()
        {
            InitializeComponent();

            lblHost.Text = PluginLanguageManager.PluginStrings.Host;
            lblPassword.Text = PluginLanguageManager.PluginStrings.Password;
            btnOk.Text = LanguageManager.Strings.Ok;

            List<Dictionary<string, string>> credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
            Dictionary<string, string> credentials = null;
            if (credentialsList != null && credentialsList.Count > 0)
            {
                credentials = credentialsList[0];
            }
            if (credentials != null && credentials.ContainsKey("host") && credentials.ContainsKey("password"))
            {
                this.host.Text = credentials["host"];
                this.password.Text = credentials["password"];
            }
            else
            {
                this.host.Text = "ws://127.0.0.1:4455";
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> credentials = new Dictionary<string, string>
            {
                ["host"] = this.host.Text,
                ["password"] = this.password.Text
            };
            PluginCredentials.SetCredentials(PluginInstance.Main, credentials);

            try
            {
                PluginInstance.Main.Disconnect();
                var self = this;
                _ = Task.Run(async () =>
                {
                    await PluginInstance.Main.SetupAndStartAsync();
                    if (PluginInstance.Main.IsConnected)
                    {
                        self.Invoke((MethodInvoker)delegate { self.Close(); });
                    }
                });
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error: {ex.Message + Environment.NewLine + ex.StackTrace} ");
            }
        }
    }
}
