using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.GUI.Dialogs;
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
    public partial class SetTextValueConfigView : ActionConfigControl, IConnDepConfigs
    {
        PluginAction pluginAction;
        SetTextValueConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SetTextValueConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            lblScene.Text = PluginLanguageManager.PluginStrings.Scene;
            lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            lblVariable.Text = PluginLanguageManager.PluginStrings.Value;

            LoadConfig();
            ResetValues();
            LoadScenes();

            connectionSelector1.ValueChanged += (sender, args) => LoadScenes();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return false;
            }

            var config = JObject.FromObject(new SetTextValueConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SceneName = this.scenesBox.Text,
                SourceName = this.sourcesBox.Text,
                Value = this.value.Text,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = this.sourcesBox.Text + "=" + this.value.Text;
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
                    if (!String.IsNullOrEmpty(name))
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

            if (String.IsNullOrWhiteSpace(this.scenesBox.Text))
            {
                return;
            }

            var self = this;
            var sceneName = scenesBox.Text;
            _ = Task.Run(async () =>
            {
                var response = await conn.OBS.SceneItemsRequests.GetSceneItemListAsync(sceneName);
                if (response != null)
                {
                    foreach (JObject sceneItem in response.SceneItems)
                    {
                        var name = sceneItem["sourceName"]?.ToString();
                        var type = sceneItem["inputKind"]?.ToString();
                        if (!String.IsNullOrEmpty(name) && ("text_gdiplus_v2").Equals(type))
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
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SetTextValueConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.scenesBox.Text = config?.SceneName;
            this.sourcesBox.Text = config?.SourceName;
            this.value.Text = config?.Value;
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
