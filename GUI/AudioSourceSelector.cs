﻿using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class AudioSourceSelector : ActionConfigControl
    {

        PluginAction pluginAction;

        public AudioSourceSelector(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.radioMute.Text = PluginLanguageManager.PluginStrings.Mute;
            this.radioUnmute.Text = PluginLanguageManager.PluginStrings.Unmute;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            this.LoadSources();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }
            string method = "toggle";
            if (this.radioMute.Checked)
            {
                method = "mute";
            }
            else if (this.radioUnmute.Checked)
            {
                method = "unmute";
            }
            else if (this.radioToggle.Checked)
            {
                method = "toggle";
            }
            JObject configurationObject = JObject.FromObject(new
            {
                sourceName = this.sourcesBox.Text,
                method = method,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.ConfigurationSummary = method + " " + this.sourcesBox.Text;
            return true;
        }

        private void LoadSources()
        {
            if (!PluginInstance.Main.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.sourcesBox.Items.Clear();
            
            var self = this;
            _ = Task.Run(async () =>
            {
                var specialResponse = await PluginInstance.Main.Obs.InputsRequests.GetSpecialInputsAsync();
                var properties = specialResponse.GetType().GetProperties();
                foreach (PropertyInfo input in properties)
                {
                    var name = specialResponse.GetType().GetProperty(input.Name).GetValue(specialResponse);
                    if (!String.IsNullOrEmpty(name?.ToString()))
                    {
                        sourcesBox.Invoke((MethodInvoker)delegate { sourcesBox.Items.Add(name); });
                    }
                }

                var response = await PluginInstance.Main.Obs.InputsRequests.GetInputListAsync();
                foreach (JObject input in response.Inputs)
                {
                    var name = input["inputName"]?.ToString();
                    var muteStatus = await PluginInstance.Main.Obs.InputsRequests.GetInputMuteAsync(name);
                    if (muteStatus != null)
                    {
                        if (!String.IsNullOrEmpty(name))
                        {
                            sourcesBox.Invoke((MethodInvoker)delegate { sourcesBox.Items.Add(name); });
                        }
                    }
                }

                self.Invoke((MethodInvoker)delegate { LoadConfig(); });
            });

        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.pluginAction.Configuration);
                    this.sourcesBox.Text = configurationObject["sourceName"].ToString();

                    switch (configurationObject["method"].ToString())
                    {
                        case "mute":
                            this.radioMute.Checked = true;
                            break;
                        case "unmute":
                            this.radioUnmute.Checked = true;
                            break;
                        case "toggle":
                            this.radioToggle.Checked = true;
                            break;
                    }
                }
                catch { }
            }
        }


        private void BtnReloadSources_Click(object sender, EventArgs e)
        {
            this.LoadSources();
        }
    }
}
