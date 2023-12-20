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
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class SetSourceVolumeConfigView : ActionConfigControl, IConnDepConfigs
    {
        PluginAction pluginAction;
        SetSourceVolumeConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SetSourceVolumeConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.radioIncrease.Text = PluginLanguageManager.PluginStrings.Increase;
            this.radioDecrease.Text = PluginLanguageManager.PluginStrings.Decrease;
            this.radioSet.Text = PluginLanguageManager.PluginStrings.Set;

            LoadConfig();
            ResetValues();
            this.LoadSources();

            connectionSelector1.ValueChanged += (sender, args) => LoadSources();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }
            var method = Enum.IncrementalMethodType.Set;
            if (this.radioIncrease.Checked)
            {
                method = Enum.IncrementalMethodType.Increase;
            }
            else if (this.radioDecrease.Checked)
            {
                method = Enum.IncrementalMethodType.Decrease;
            }
            else if (this.radioSet.Checked)
            {
                method = Enum.IncrementalMethodType.Set;
            }
            var config = JObject.FromObject(new SetSourceVolumeConfig
            {
                ConnectionName = connectionSelector1.Value,
                SourceName = sourcesBox.Text,
                Method = method,
                Decibel = (int)decibel.Value,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = this.sourcesBox.Text + " -> " + method + " -> " + this.lblToBy.Text + " -> " + this.decibel.Value + "dB";
            return true;
        }

        private void LoadSources()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!(conn?.IsConnected ?? false))
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            this.sourcesBox.Items.Clear();

            var self = this;
            _ = Task.Run(async () =>
            {
                var specialResponse = await conn.OBS.InputsRequests.GetSpecialInputsAsync();
                var properties = specialResponse.GetType().GetProperties();
                foreach (PropertyInfo input in properties)
                {
                    var name = specialResponse.GetType().GetProperty(input.Name).GetValue(specialResponse);
                    if (!String.IsNullOrEmpty(name?.ToString()))
                    {
                        sourcesBox.Invoke((MethodInvoker)delegate { sourcesBox.Items.Add(name); });
                    }
                }

                var response = await conn.OBS.InputsRequests.GetInputListAsync();
                foreach (JObject input in response.Inputs)
                {
                    var name = input["inputName"]?.ToString();
                    var muteStatus = await conn.OBS.InputsRequests.GetInputMuteAsync(name);
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

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SetSourceVolumeConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.sourcesBox.Text = config?.SourceName;

            switch (config?.Method ?? Enum.IncrementalMethodType.Set)
            {
                case Enum.IncrementalMethodType.Increase:
                    this.radioIncrease.Checked = true;
                    break;
                case Enum.IncrementalMethodType.Decrease:
                    this.radioDecrease.Checked = true;
                    break;
                case Enum.IncrementalMethodType.Set:
                    this.radioSet.Checked = true;
                    break;
            }

            this.decibel.Value = config?.Decibel ?? 0;
        }

        private void BtnReloadSources_Click(object sender, EventArgs e)
        {
            this.LoadSources();
        }

        private void Method_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioIncrease.Checked || this.radioDecrease.Checked)
            {
                this.lblToBy.Text = PluginLanguageManager.PluginStrings.GeneralBy;
                this.decibel.Maximum = 96;
                this.decibel.Minimum = 1;
            }
            else if (this.radioSet.Checked)
            {
                this.lblToBy.Text = PluginLanguageManager.PluginStrings.Toggle;
                this.decibel.Maximum = 0;
                this.decibel.Minimum = -96;
            }
        }

    }
}
