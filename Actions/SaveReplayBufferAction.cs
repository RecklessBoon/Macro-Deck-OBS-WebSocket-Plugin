using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SaveReplayBufferAction : ActionBase
    {
        public override bool CanConfigure => true;

        public override string Name => PluginLanguageManager.PluginStrings.ActionSaveReplayBuffer;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSaveReplayBufferDescription;

        public override ConfigBase GetConfig() => GetConfig<SaveReplayBufferConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var config = GetConfig<SaveReplayBufferConfig>();
            Connection conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
            conn?.OBS.OutputsRequests.SaveReplayBufferAsync();
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SaveReplayBufferConfigView(this, actionConfigurator);
        }
    }
}
