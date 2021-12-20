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
            if (!PluginInstance.Main.OBS.IsConnected) return;
            PluginInstance.Main.OBS.SaveReplayBuffer();
            
        }
    }
}
