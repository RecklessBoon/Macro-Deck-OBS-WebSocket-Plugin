using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class ToggleConnectionAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionToggleConnection;

        public override string Description => PluginLanguageManager.PluginStrings.ActionToggleConnectionDescription;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginInstance.Main.OBS4 != null) TriggerOBS4(clientId, actionButton);
            else if (PluginInstance.Main.OBS5 != null) TriggerOBS5(clientId, actionButton);
            else PluginInstance.Main.SetupAndStartAsync();
        }

        protected void TriggerOBS4(string clientId, ActionButton actionButton)
        {
            if (PluginInstance.Main.OBS4.IsConnected)
            {
                PluginInstance.Main.Disconnect();
            }
        }

        protected void TriggerOBS5(string clientId, ActionButton actionButton)
        {
            if (PluginInstance.Main.OBS5.IsConnected)
            {
                PluginInstance.Main.Disconnect();
            }
        }
    }
}
