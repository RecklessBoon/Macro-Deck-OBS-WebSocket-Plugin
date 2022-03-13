using OBSWebsocketDotNet;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
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
        public PluginConfig()
        {
            InitializeComponent();

            this.lblHost.Text = PluginLanguageManager.PluginStrings.Host;
            this.lblPassword.Text = PluginLanguageManager.PluginStrings.Password;
            this.btnOk.Text = LanguageManager.Strings.Ok;

            List<Dictionary<string, string>> credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
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
            Dictionary<string, string> credentials = new Dictionary<string, string>
            {
                ["host"] = this.host.Text,
                ["password"] = this.password.Text
            };
            PluginCredentials.SetCredentials(PluginInstance.Main, credentials);
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (PluginInstance.Main.OBS.IsConnected)
            {
                this.Close();
                return;
            }
            try
            {
                PluginInstance.Main.OBS.Connected += OBS_Connected;
                PluginInstance.Main.OBS.Connect(this.host.Text, this.password.Text);
            }
            catch (AuthFailureException)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(PluginLanguageManager.PluginStrings.AuthenticationFailed, PluginLanguageManager.PluginStrings.InfoWrongPassword, MessageBoxButtons.OK);
                }
                return;
            }
            catch (ErrorResponseException)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(PluginLanguageManager.PluginStrings.AuthenticationFailed, PluginLanguageManager.PluginStrings.InfoWrongPassword, MessageBoxButtons.OK);
                }
                return;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error: {ex.Message + Environment.NewLine + ex.StackTrace} ");
            }
        }
    }
}
