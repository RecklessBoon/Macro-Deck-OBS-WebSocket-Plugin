using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class SaveReplayBufferConfigView : ActionConfigControl
    {
        private PluginAction pluginAction;
        SaveReplayBufferConfig config;

        public SaveReplayBufferConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;

            if (pluginAction.Configuration != null)
            {
                config = JObject.Parse(pluginAction.Configuration).ToObject<SaveReplayBufferConfig>();
                connectionSelector1.connections.SelectedValue = config?.ConnectionName;
            }
        }

        public override bool OnActionSave()
        {
            var selectedConnection = connectionSelector1.connections.SelectedValue;
            if (selectedConnection == null) return false;

            var config = JObject.FromObject(new SaveReplayBufferConfig { ConnectionName = selectedConnection.ToString() });

            pluginAction.Configuration = config.ToString();
            pluginAction.ConfigurationSummary = selectedConnection.ToString();

            return true;
        }
    }
}
