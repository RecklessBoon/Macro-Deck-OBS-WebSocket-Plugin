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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Audio;
using Windows.UI.Composition.Scenes;

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

        private CancellationTokenSource LoadingScenesTokenSource;
        private TaskCompletionSource LoadingScenes;
        private void LoadScenes()
        {
            if ((LoadingScenes?.Task.IsCompleted ?? true) != true)
            {
                LoadingScenesTokenSource.Cancel();
                try
                {
                    LoadingScenes.Task.Wait(LoadingScenesTokenSource.Token);
                }
                catch (OperationCanceledException) { /* do nothing */ }
            }

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
            LoadingScenesTokenSource = new CancellationTokenSource();
            var task = Task.Run(async () =>
            {
                var sceneListResponse = await conn.OBS.ScenesRequests.GetSceneListAsync();

                if (LoadingScenesTokenSource.Token.IsCancellationRequested) return;

                foreach (JObject scene in sceneListResponse.Scenes)
                {
                    var name = scene["sceneName"]?.ToString();
                    if (!name.Equals(String.Empty))
                    {
                        scenesBox.Invoke((MethodInvoker)delegate { scenesBox.Items.Add(name); });
                    }

                    var response = await conn.OBS.SceneItemsRequests.GetSceneItemListAsync(name);
                    foreach (JObject item in response.SceneItems)
                    {
                        if (item["isGroup"].Value<bool?>() == true)
                        {
                            var groupName = item["sourceName"]?.ToString();
                            scenesBox.Invoke((MethodInvoker)delegate { scenesBox.Items.Add(groupName); });
                        }
                    }
                }

                self.Invoke((MethodInvoker)delegate
                {
                    scenesBox.Text = config?.SceneName;
                    LoadSources();
                });
            }, LoadingScenesTokenSource.Token);
            LoadingScenes = new TaskCompletionSource(task);
        }

        private CancellationTokenSource LoadingSourcesTokenSource;
        private TaskCompletionSource LoadingSources;
        private void LoadSources()
        {
            if ((LoadingSources?.Task.IsCompleted ?? true) != true)
            {
                LoadingSourcesTokenSource.Cancel();
                try
                {
                    LoadingSources.Task.Wait(LoadingSourcesTokenSource.Token);
                } catch (OperationCanceledException) { /* do nothing */ }
            }

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

            LoadingSourcesTokenSource = new CancellationTokenSource();
            var task = Task.Run(async () =>
            {
                var response = await conn.OBS.SceneItemsRequests.GetSceneItemListAsync(sceneName);

                if (LoadingSourcesTokenSource.Token.IsCancellationRequested) return;

                var sceneItems = response?.SceneItems;
                if (response == null)
                {
                    var group_response = await conn.OBS.SceneItemsRequests.GetGroupSceneItemListAsync(sceneName);
                    sceneItems = group_response?.SceneItems;
                }

                if (LoadingSourcesTokenSource.Token.IsCancellationRequested) return;

                if ((sceneItems?.Length ?? 0) > 0)
                {
                    foreach (JObject item in sceneItems)
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
            }, LoadingSourcesTokenSource.Token);
            LoadingSources = new TaskCompletionSource(task);
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
