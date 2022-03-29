using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
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
    public partial class FilterSelector : ActionConfigControl
    {

        PluginAction pluginAction;

        public FilterSelector(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblScene.Text = PluginLanguageManager.PluginStrings.Scene;
            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.lblFilter.Text = PluginLanguageManager.PluginStrings.Filter;
            this.radioHide.Text = PluginLanguageManager.PluginStrings.Disable;
            this.radioShow.Text = PluginLanguageManager.PluginStrings.Enable;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            LoadScenes();
            LoadConfig();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.filtersBox.Text))
            {
                return false;
            }
            string method = "toggle";
            if (this.radioHide.Checked)
            {
                method = "hide";
            }
            else if (this.radioShow.Checked)
            {
                method = "show";
            }
            else if (this.radioToggle.Checked)
            {
                method = "toggle";
            }
            JObject configurationObject = JObject.FromObject(new
            {
                sceneName = this.scenesBox.Text,
                sourceName = this.sourcesBox.Text,
                filterName = this.filtersBox.Text,
                method = method,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.ConfigurationSummary = method + " " + (String.IsNullOrWhiteSpace(this.sourcesBox.Text) ? this.scenesBox.Text : this.sourcesBox.Text) + "/" + this.filtersBox.Text;
            return true;
        }

        private void LoadScenes()
        {
            if (!PluginInstance.Main.OBS.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.scenesBox.Items.Clear();
            this.scenesBox.Text = String.Empty;

            foreach (OBSScene scene in PluginInstance.Main.OBS.ListScenes().ToArray())
            {
                this.scenesBox.Items.Add(scene.Name);
            }
            
            LoadSources();
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
            this.sourcesBox.Text = String.Empty;

            foreach (var sceneItem in PluginInstance.Main.OBS.GetSceneItemList(this.scenesBox.Text))
            {
                this.sourcesBox.Items.Add(sceneItem.SourceName);
            }

            LoadFilters();
        }

        private void LoadFilters()
        {
            if (!PluginInstance.Main.OBS.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.filtersBox.Items.Clear();
            this.filtersBox.Text = String.Empty;

            if (String.IsNullOrWhiteSpace(this.scenesBox.Text) && String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return;
            }

            foreach (var filterItem in PluginInstance.Main.OBS.GetSourceFilters(String.IsNullOrWhiteSpace(this.sourcesBox.Text) ? this.scenesBox.Text : this.sourcesBox.Text))
            {
                this.filtersBox.Items.Add(filterItem.Name);
            }

        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.pluginAction.Configuration);
                    this.scenesBox.Text = configurationObject["sceneName"].ToString();
                    this.sourcesBox.Text = configurationObject["sourceName"].ToString();
                    this.filtersBox.Text = configurationObject["filterName"].ToString();

                    switch (configurationObject["method"].ToString())
                    {
                        case "hide":
                            this.radioHide.Checked = true;
                            break;
                        case "show":
                            this.radioShow.Checked = true;
                            break;
                        case "toggle":
                            this.radioToggle.Checked = true;
                            break;
                    }
                }
                catch { }
            }
        }


        private void BtnReloadScenes_Click(object sender, EventArgs e)
        {
            LoadScenes();
        }

        private void BtnReloadSources_Click(object sender, EventArgs e)
        {
            LoadSources();
        }

        private void ScenesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSources();
        }

        private void sourcesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilters();
        }

    }
}
