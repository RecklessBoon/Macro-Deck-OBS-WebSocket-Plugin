using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
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
    public partial class AudioSourceVolumeSelector : ActionConfigControl
    {
        PluginAction pluginAction;

        public AudioSourceVolumeSelector(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.radioIncrease.Text = PluginLanguageManager.PluginStrings.Increase;
            this.radioDecrease.Text = PluginLanguageManager.PluginStrings.Decrease;
            this.radioSet.Text = PluginLanguageManager.PluginStrings.Set;

            this.LoadSources();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }
            string method = "set";
            if (this.radioIncrease.Checked)
            {
                method = "increase";
            }
            else if (this.radioDecrease.Checked)
            {
                method = "decrease";
            }
            else if (this.radioSet.Checked)
            {
                method = "set";
            }
            JObject configurationObject = JObject.FromObject(new
            {
                sourceName = this.sourcesBox.Text,
                method = method,
                decibel = this.decibel.Value,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.ConfigurationSummary = this.sourcesBox.Text + " -> " + method + " -> " + this.lblToBy.Text + " -> " + this.decibel.Value + "dB";
            return true;
        }

        private void LoadSources()
        {
            if (!PluginInstance.Main.OBS.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.sourcesBox.Items.Clear();


            foreach (var audioSource in PluginInstance.Main.OBS.GetSpecialSources().Values)
            {

                this.sourcesBox.Items.Add(audioSource);
            }

            this.LoadConfig();
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
                        case "increase":
                            this.radioIncrease.Checked = true;
                            break;
                        case "decrease":
                            this.radioDecrease.Checked = true;
                            break;
                        case "set":
                            this.radioSet.Checked = true;
                            break;
                    }

                    Int32.TryParse(configurationObject["decibel"].ToString(), out int decibel);
                    this.decibel.Value = decibel;
                }
                catch { }
            }
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
            } else if (this.radioSet.Checked)
            {
                this.lblToBy.Text = PluginLanguageManager.PluginStrings.Toggle;
                this.decibel.Maximum = 0;
                this.decibel.Minimum = -96;
            }
        }

    }
}
