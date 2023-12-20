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
    public partial class SourceVisibilityConfigView : ActionConfigControl, IConnDepConfigs
    {

        PluginAction pluginAction;
        SourceVisibilityConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SourceVisibilityConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            this.lblScene.Text = PluginLanguageManager.PluginStrings.Scene;
            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.radioHide.Text = PluginLanguageManager.PluginStrings.Hide;
            this.radioShow.Text = PluginLanguageManager.PluginStrings.Show;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            LoadConfig();
            ResetValues();
            LoadScenes();

            connectionSelector1.ValueChanged += (sender, args) => LoadScenes();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.scenesBox.Text) || String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }
            var method = Enum.VisibilityMethodType.Toggle;
            if (this.radioHide.Checked)
            {
                method = Enum.VisibilityMethodType.Hide;
            }
            else if (this.radioShow.Checked)
            {
                method = Enum.VisibilityMethodType.Show;
            }
            else if (this.radioToggle.Checked)
            {
                method = Enum.VisibilityMethodType.Toggle;
            }
            var config = JObject.FromObject(new SourceVisibilityConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SceneName = this.scenesBox.Text,
                SourceName = this.sourcesBox.Text,
                Method = method,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = method.ToString() + " " + this.scenesBox.Text + "/" + this.sourcesBox.Text;
            return true;
        }

        private void LoadScenes()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!(conn?.IsConnected ?? false))
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
                var sceneListResponse = await conn.OBS.ScenesRequests.GetSceneListAsync();
                foreach (JObject scene in sceneListResponse.Scenes)
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

            if (!(conn?.IsConnected ?? false))
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
                    await RecurseSourceResponse(conn, response.SceneItems);
                }
                self.Invoke((MethodInvoker)delegate
                {
                    sourcesBox.Text = config?.SourceName;
                });
            });
        }

        private async Task RecurseSourceResponse(Connection conn, JObject[] sceneItems)
        {
            foreach (JObject item in sceneItems)
            {
                if (item["isGroup"].Value<bool?>() == true)
                {
                    var sub = await conn.OBS.SceneItemsRequests.GetGroupSceneItemListAsync(item["sourceName"]?.ToString());
                    await RecurseSourceResponse(conn, sub.SceneItems);
                }
                else
                {
                    var name = item["sourceName"]?.ToString();
                    if (!String.IsNullOrEmpty(name))
                    {
                        sourcesBox.Invoke((MethodInvoker)delegate { sourcesBox.Items.Add(name); });
                    }
                }
            }
        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SourceVisibilityConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.scenesBox.Text = config?.SceneName;
            this.sourcesBox.Text = config?.SourceName;

            switch (config?.Method ?? Enum.VisibilityMethodType.Toggle)
            {
                case Enum.VisibilityMethodType.Hide:
                    this.radioHide.Checked = true;
                    break;
                case Enum.VisibilityMethodType.Show:
                    this.radioShow.Checked = true;
                    break;
                case Enum.VisibilityMethodType.Toggle:
                    this.radioToggle.Checked = true;
                    break;
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
    }
}
