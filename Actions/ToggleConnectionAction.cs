using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class ToggleConnectionAction : PluginAction
    {
        public override string Name => "OBS Toggle connection";

        public override string DisplayName { get; set; } = "OBS Toggle connection";

        public override string Description => "Toggles the connection between OBS and Macro Deck";

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            PluginInstance.Main.Connect();
        }
    }
}
