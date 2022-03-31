using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.GUI.Dialogs;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class SetTextValueConfigurator : ActionConfigControl
    {
        PluginAction pluginAction;

        public SetTextValueConfigurator(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            LoadScenes();
            LoadConfig();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }

            JObject configurationObject = JObject.FromObject(new
            {
                sceneName = this.scenesBox.Text,
                sourceName = this.sourcesBox.Text,
                value = this.value.Text,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.ConfigurationSummary = this.sourcesBox.Text + "=" + this.value.Text;
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

            if (String.IsNullOrWhiteSpace(this.scenesBox.Text))
            {
                return;
            }

                foreach (var sceneItem in PluginInstance.Main.OBS.GetSceneItemList(this.scenesBox.Text))
            {
                this.sourcesBox.Items.Add(sceneItem.SourceName);
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
                    this.value.Text = configurationObject["value"].ToString();
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

        private void btnTemplateEditor_Click(object sender, EventArgs e)
        {
            using (var templateEditor = new TemplateEditor(this.value.Text))
            {
                if (templateEditor.ShowDialog() == DialogResult.OK)
                {
                    this.value.Text = templateEditor.Template;
                }
            }
        }
    }
}
