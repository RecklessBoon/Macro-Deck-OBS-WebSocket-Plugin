using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SaveReplayBufferAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSaveReplayBuffer;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSaveReplayBufferDescription;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginInstance.Main.OBS4 != null) TriggerOBS4(clientId, actionButton);
            else if (PluginInstance.Main.OBS5 != null) TriggerOBS5(clientId, actionButton);
        }

        protected void TriggerOBS4(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS4.IsConnected) return;
            PluginInstance.Main.OBS4.SaveReplayBuffer();
        }

        protected void TriggerOBS5(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS5.IsConnected) return;
            _ = PluginInstance.Main.OBS5.OutputsRequests.SaveReplayBufferAsync();
        }
    }
}
