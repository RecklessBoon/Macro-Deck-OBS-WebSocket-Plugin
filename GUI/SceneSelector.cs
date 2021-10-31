using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class SceneSelector : ActionConfigControl
    {
        PluginAction pluginAction;

        public SceneSelector(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            actionConfigurator.ActionSave += OnActionSave;

            this.LoadScenes();
        }

        private void OnActionSave(object sender, EventArgs e)
        {
            this.UpdateConfig();
        }

        private void LoadScenes()
        {
            if (!PluginInstance.Main.OBS.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog("Not connected", "Macro Deck is not connected to OBS. Please make sure, OBS and the OBS-WebSocket plugin are working properly and you configured the plugin.", System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.scenesBox.Items.Clear();

            foreach (OBSScene scene in PluginInstance.Main.OBS.ListScenes().ToArray())
            {
                this.scenesBox.Items.Add(scene.Name);
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
                    this.scenesBox.Text = configurationObject["scene"].ToString();
                }
                catch { }
            }
        }

        private void UpdateConfig()
        {
            if (String.IsNullOrWhiteSpace(this.scenesBox.Text))
            {
                return;
            }
            JObject configurationObject = JObject.FromObject(new
            {
                scene = this.scenesBox.Text,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.DisplayName = this.pluginAction.Name + " -> " + this.scenesBox.Text;
        }

        private void BtnReloadScenes_Click(object sender, EventArgs e)
        {
            this.LoadScenes();
        }
    }
}
