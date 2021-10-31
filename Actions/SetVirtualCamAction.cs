using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.GUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetVirtualCamAction : PluginAction
    {
        public override string Name => "OBS Set virtual cam state";

        public override string DisplayName { get; set; } = "OBS Set virtual cam state";

        public override string Description => "Start/stop/toggle virtual cam";

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    switch (configurationObject["method"].ToString())
                    {
                        case "start":
                            PluginInstance.Main.OBS.StartVirtualCam();
                            break;
                        case "stop":
                            PluginInstance.Main.OBS.StopVirtualCam();
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS.ToggleVirtualCam();
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
