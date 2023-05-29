using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetReplayBufferState : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetReplayBufferState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetReplayBufferStateDescription;

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.Obs.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    switch (configurationObject["method"].ToString())
                    {
                        case "start":
                            _ = PluginInstance.Main.Obs.OutputsRequests.StartReplayBufferAsync();
                            break;
                        case "stop":
                            _ = PluginInstance.Main.Obs.OutputsRequests.StopReplayBufferAsync();
                            break;
                        case "toggle":
                            _ = PluginInstance.Main.Obs.OutputsRequests.ToggleReplayBufferAsync();
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new StateSelector(this, actionConfigurator);
        }
    }
}
