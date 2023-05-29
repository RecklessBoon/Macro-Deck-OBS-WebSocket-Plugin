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
    public class SetVirtualCamAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetVirtualCamState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetVirtualCamStateDescription;

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
                            _ = PluginInstance.Main.Obs.OutputsRequests.StartVirtualCamAsync();
                            break;
                        case "stop":
                            _ = PluginInstance.Main.Obs.OutputsRequests.StopVirtualCamAsync();
                            break;
                        case "toggle":
                            _ = PluginInstance.Main.Obs.OutputsRequests.ToggleVirtualCamAsync();
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
