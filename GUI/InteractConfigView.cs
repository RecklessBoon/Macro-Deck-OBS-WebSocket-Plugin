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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class InteractConfigView : ActionConfigControl, IConnDepConfigs
    {

        PluginAction pluginAction;
        InteractConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public InteractConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            this.lblScene.Text = PluginLanguageManager.PluginStrings.Scene;
            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;

            LoadConfig();
            ResetValues();
            LoadScenes();
            connectionSelector1.ValueChanged += (sender, args) => LoadScenes();
        }

        public override bool OnActionSave()
        {
            var config = JObject.FromObject(new InteractConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SceneName = this.scenesBox.Text,
                SourceName = this.sourcesBox.Text,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = "Interact with " + (String.IsNullOrWhiteSpace(this.sourcesBox.Text) ? this.scenesBox.Text : this.sourcesBox.Text);
            return true;
        }

        private void LoadScenes()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!conn?.IsConnected ?? false)
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            this.scenesBox.Items.Clear();
            this.scenesBox.Text = String.Empty;

            var self = this;
            _ = Task.Run(async () =>
            {
                var response = await conn.OBS.ScenesRequests.GetSceneListAsync();
                foreach (JObject scene in response.Scenes)
                {
                    var name = scene["sceneName"]?.ToString();
                    if (!name.Equals(String.Empty))
                    {
                        scenesBox.Invoke((MethodInvoker)delegate { scenesBox.Items.Add(name); });
                    }
                }

                self.Invoke((MethodInvoker)delegate
                {
                    scenesBox.Text = config?.SceneName;
                    LoadSources();
                });
            });

        }

        private void LoadSources()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!conn?.IsConnected ?? false)
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            this.sourcesBox.Items.Clear();
            this.sourcesBox.Text = String.Empty;

            var self = this;
            var sceneName = scenesBox.Text;
            _ = Task.Run(async () =>
            {
                var response = await conn.OBS.SceneItemsRequests.GetSceneItemListAsync(sceneName);
                if (response != null)
                {
                    foreach (JObject item in response.SceneItems)
                    {
                        var name = item["sourceName"]?.ToString();
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
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<InteractConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.scenesBox.Text = config?.SceneName;
            this.sourcesBox.Text = config?.SourceName;
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

    }
}
