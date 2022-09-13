using OBSWebsocketDotNet;
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
        public const int DEFAULT_TIMEOUT = 5;

        public PluginConfig()
        {
            InitializeComponent();

            numTimeout.Value = DEFAULT_TIMEOUT;

            toolTip1.ToolTipTitle = PluginLanguageManager.PluginStrings.VersionTypeToolTipTitle;
            toolTip1.SetToolTip(hlpVersion, PluginLanguageManager.PluginStrings.VersionTypeToolTipBody);

            lblHost.Text = PluginLanguageManager.PluginStrings.Host;
            lblPassword.Text = PluginLanguageManager.PluginStrings.Password;
            lblVersion.Text = PluginLanguageManager.PluginStrings.Version;
            lblTimeout.Text = PluginLanguageManager.PluginStrings.Timeout;
            btnOk.Text = LanguageManager.Strings.Ok;
            versionType_Auto.Text = PluginLanguageManager.PluginStrings.Auto;
            versionType_WS5.Text = PluginLanguageManager.PluginStrings.WebSocket5;
            versionType_WS4.Text = PluginLanguageManager.PluginStrings.WebSocket4;

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
            }
            else
            {
                this.host.Text = "ws://127.0.0.1:4444";
            }

            var versionTypeFound = Enum.TryParse<OBSWebSocketVersionType>(PluginConfiguration.GetValue(PluginInstance.Main, "versionType"), out OBSWebSocketVersionType versionType);
            SetVersionTypeSelected(versionTypeFound ? versionType : OBSWebSocketVersionType.OBS_WEBSOCKET_AUTO);

            var timeoutFound = int.TryParse(PluginConfiguration.GetValue(PluginInstance.Main, "timeout"), out int timeout);
            numTimeout.Value = timeoutFound ? timeout : DEFAULT_TIMEOUT;
        }

        private OBSWebSocketVersionType GetVersionTypeSelected()
        {
            return versionType_Auto.Checked ? OBSWebSocketVersionType.OBS_WEBSOCKET_AUTO : versionType_WS5.Checked ? OBSWebSocketVersionType.OBS_WEBSOCKET_V5 : OBSWebSocketVersionType.OBS_WEBSOCKET_V4;
        }

        private void SetVersionTypeSelected(OBSWebSocketVersionType type)
        {
            switch (type)
            {
                case OBSWebSocketVersionType.OBS_WEBSOCKET_V4:
                    versionType_WS4.Checked = true;
                    break;
                case OBSWebSocketVersionType.OBS_WEBSOCKET_V5:
                    versionType_WS5.Checked = true;
                    break;
                case OBSWebSocketVersionType.OBS_WEBSOCKET_AUTO:
                default:
                    versionType_Auto.Checked = true;
                    break;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            PluginConfiguration.SetValue(PluginInstance.Main, "versionType", GetVersionTypeSelected().ToString());
            PluginConfiguration.SetValue(PluginInstance.Main, "timeout", numTimeout.Value.ToString());

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

        private void OnVersionType_AutoCheckedChanged(object sender, EventArgs e)
        {
            ButtonRadioButton rb = (ButtonRadioButton)sender;
            numTimeout.Enabled = rb.Checked;
            numTimeout.BackColor = rb.Checked ? Color.FromArgb(65, 65, 65) : Color.Gray;
        }
    }
}
