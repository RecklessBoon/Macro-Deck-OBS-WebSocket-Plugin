using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Threading.Tasks;
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
            if (!PluginInstance.Main.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.scenesBox.Items.Clear();
            this.scenesBox.Text = String.Empty;

            if (PluginInstance.Main.OBS4 != null)
            {
                foreach (OBSScene scene in PluginInstance.Main.OBS4.ListScenes().ToArray())
                {
                    this.scenesBox.Items.Add(scene.Name);
                }
                LoadSources();
            }
            else
            {
                var self = this;
                _ = Task.Run(async () =>
                {
                    var response = await PluginInstance.Main.OBS5.ScenesRequests.GetSceneListAsync();
                    foreach (JObject scene in response.Scenes)
                    {
                        var name = scene["sceneName"]?.ToString();
                        if (!name.Equals(String.Empty))
                        {
                            scenesBox.Invoke((MethodInvoker)delegate { scenesBox.Items.Add(name); });
                        }
                    }
                    self.Invoke((MethodInvoker)delegate { LoadConfig(); LoadSources(); });
                });
            }

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
            this.sourcesBox.Text = String.Empty;

            if (PluginInstance.Main.OBS4 != null)
            {
                foreach (var sceneItem in PluginInstance.Main.OBS4.GetSceneItemList(this.scenesBox.Text))
                {
                    this.sourcesBox.Items.Add(sceneItem.SourceName);
                }
                LoadFilters();
            }
            else
            {
                var self = this;
                var sceneName = scenesBox.Text;
                _ = Task.Run(async () =>
                {
                    var response = await PluginInstance.Main.OBS5.SceneItemsRequests.GetSceneItemListAsync(sceneName);
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
                    self.Invoke((MethodInvoker)delegate { LoadConfig(); LoadFilters(); });
                });
            }
        }

        private void LoadFilters()
        {
            if (!PluginInstance.Main.IsConnected)
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

            if (PluginInstance.Main.OBS4 != null)
            {
                foreach (var filterItem in PluginInstance.Main.OBS4.GetSourceFilters(String.IsNullOrWhiteSpace(this.sourcesBox.Text) ? this.scenesBox.Text : this.sourcesBox.Text))
                {
                    this.filtersBox.Items.Add(filterItem.Name);
                }
                LoadConfig();
            }
            else
            {
                var self = this;
                var sourceName = sourcesBox.Text;
                _ = Task.Run(async () =>
                {
                    var response = await PluginInstance.Main.OBS5.FiltersRequests.GetSourceFilterListAsync(sourceName);
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
                    self.Invoke((MethodInvoker)delegate { LoadConfig(); });
                });
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
