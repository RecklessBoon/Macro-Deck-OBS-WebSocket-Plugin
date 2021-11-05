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
            PluginInstance.Main.Connect();
        }
    }
}
