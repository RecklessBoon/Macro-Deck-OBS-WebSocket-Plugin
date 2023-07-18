using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI.Interfaces;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class SetAudioMutedConfigView : ActionConfigControl, IConnDepConfigs
    {

        PluginAction pluginAction;
        SetAudioMutedConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SetAudioMutedConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.radioMute.Text = PluginLanguageManager.PluginStrings.Mute;
            this.radioUnmute.Text = PluginLanguageManager.PluginStrings.Unmute;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            LoadConfig();
            ResetValues();
            LoadSources();
            connectionSelector1.ValueChanged += (sender, args) => LoadSources();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }
            var method = Enum.AudioMethodType.Toggle;
            if (this.radioMute.Checked)
            {
                method = Enum.AudioMethodType.Mute;
            }
            else if (this.radioUnmute.Checked)
            {
                method = Enum.AudioMethodType.Unmute;
            }
            else if (this.radioToggle.Checked)
            {
                method = Enum.AudioMethodType.Toggle;
            }
            var config = JObject.FromObject(new SetAudioMutedConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SourceName = this.sourcesBox.Text,
                Method = method,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = method.ToString() + " " + this.sourcesBox.Text;
            return true;
        }

        private void LoadSources()
        {
            var connection = (this as IConnDepConfigs).Conn;
            if (connection == null) return;

            if (!(connection?.IsConnected ?? false))
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            this.sourcesBox.Items.Clear();

            var self = this;
            _ = Task.Run(async () =>
            {
                var specialResponse = await connection.OBS.InputsRequests.GetSpecialInputsAsync();
                var properties = specialResponse.GetType().GetProperties();
                foreach (PropertyInfo input in properties)
                {
                    var name = specialResponse.GetType().GetProperty(input.Name).GetValue(specialResponse);
                    if (!String.IsNullOrEmpty(name?.ToString()))
                    {
                        sourcesBox.Invoke((MethodInvoker)delegate { sourcesBox.Items.Add(name); });
                    }
                }

                var response = await connection.OBS.InputsRequests.GetInputListAsync();
                foreach (JObject input in response.Inputs)
                {
                    var name = input["inputName"]?.ToString();
                    var muteStatus = await connection.OBS.InputsRequests.GetInputMuteAsync(name);
                    if (muteStatus != null)
                    {
                        if (!String.IsNullOrEmpty(name))
                        {
                            sourcesBox.Invoke((MethodInvoker)delegate { sourcesBox.Items.Add(name); });
                        }
                    }
                }

                self.Invoke((MethodInvoker)delegate
                {
                    sourcesBox.Text = config?.SourceName;
                });
            });

        }

        private void LoadConfig(object sender = null)
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SetAudioMutedConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.sourcesBox.Text = config?.SourceName;

            switch (config?.Method ?? Enum.AudioMethodType.Toggle)
            {
                case Enum.AudioMethodType.Mute:
                    this.radioMute.Checked = true;
                    break;
                case Enum.AudioMethodType.Unmute:
                    this.radioUnmute.Checked = true;
                    break;
                case Enum.AudioMethodType.Toggle:
                    this.radioToggle.Checked = true;
                    break;
            }
        }


        private void BtnReloadSources_Click(object sender, EventArgs e)
        {
            this.LoadSources();
        }
    }
}
