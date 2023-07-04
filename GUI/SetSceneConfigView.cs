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
    public partial class SetSceneConfigView : ActionConfigControl, IConnDepConfigs
    {
        PluginAction pluginAction;
        SetSceneConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SetSceneConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblScene.Text = PluginLanguageManager.PluginStrings.Scene;

            LoadConfig();
            ResetValues();
            LoadScenes();

            connectionSelector1.ValueChanged += (sender, args) => LoadScenes();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.scenesBox.Text))
            {
                return false;
            }
            var config = JObject.FromObject(new SetSceneConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SceneName = this.scenesBox.Text,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = this.scenesBox.Text;
            return true;
        }


        private void LoadScenes()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!(conn?.IsConnected ?? false))
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, MessageBoxButtons.OK);
                return;
            }

            this.scenesBox.Items.Clear();

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
                self.Invoke((MethodInvoker)delegate {
                    scenesBox.Text = config?.SceneName;
                });
            });

        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SetSceneConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.scenesBox.Text = config?.SceneName;
        }

        private void BtnReloadScenes_Click(object sender, EventArgs e)
        {
            LoadScenes();
        }
    }
}
