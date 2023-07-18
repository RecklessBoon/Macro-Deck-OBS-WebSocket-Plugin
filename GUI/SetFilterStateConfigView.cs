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
    public partial class SetFilterStateConfigView : ActionConfigControl, IConnDepConfigs
    {

        PluginAction pluginAction;
        SetFilterStateConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SetFilterStateConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            this.lblScene.Text = PluginLanguageManager.PluginStrings.Scene;
            this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            this.lblFilter.Text = PluginLanguageManager.PluginStrings.Filter;
            this.radioHide.Text = PluginLanguageManager.PluginStrings.Disable;
            this.radioShow.Text = PluginLanguageManager.PluginStrings.Enable;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            LoadConfig();
            ResetValues();
            LoadScenes();
            connectionSelector1.ValueChanged += (sender, args) => LoadScenes();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.filtersBox.Text))
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
            var config = JObject.FromObject(new SetFilterStateConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SceneName = this.scenesBox.Text,
                SourceName = this.sourcesBox.Text,
                FilterName = this.filtersBox.Text,
                Method = method,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = method.ToString() + " " + (String.IsNullOrWhiteSpace(this.sourcesBox.Text) ? this.scenesBox.Text : this.sourcesBox.Text) + "/" + this.filtersBox.Text;
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
                    LoadFilters();
                });
            });
        }

        private void LoadFilters()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!conn?.IsConnected ?? false)
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            this.filtersBox.Items.Clear();
            this.filtersBox.Text = String.Empty;

            if (String.IsNullOrWhiteSpace(this.scenesBox.Text) && String.IsNullOrWhiteSpace(this.sourcesBox.Text))
            {
                return;
            }

            var self = this;
            var sourceName = sourcesBox.Text;
            _ = Task.Run(async () =>
            {
                var response = await conn.OBS.FiltersRequests.GetSourceFilterListAsync(sourceName);
                if (response != null)
                {
                    foreach (JObject filter in response.Filters)
                    {
                        var name = filter["filterName"]?.ToString();
                        if (!String.IsNullOrWhiteSpace(name))
                        {
                            filtersBox.Invoke((MethodInvoker)delegate { filtersBox.Items.Add(name); });
                        }
                    }
                }
                self.Invoke((MethodInvoker)delegate
                {
                    filtersBox.Text = config?.FilterName;
                });
            });

        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SetFilterStateConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.scenesBox.Text = config?.SceneName;
            this.sourcesBox.Text = config?.SourceName;
            this.filtersBox.Text = config?.FilterName;

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

        private void sourcesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilters();
        }

    }
}
