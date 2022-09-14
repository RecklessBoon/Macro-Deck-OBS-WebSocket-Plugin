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
            if (PluginInstance.Main.OBS4 != null) TriggerOBS4(clientId, actionButton);
            else if (PluginInstance.Main.OBS5 != null) TriggerOBS5(clientId, actionButton);
        }

        protected void TriggerOBS4(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS4.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    switch (configurationObject["method"].ToString())
                    {
                        case "start":
                            PluginInstance.Main.OBS4.StartVirtualCam();
                            break;
                        case "stop":
                            PluginInstance.Main.OBS4.StopVirtualCam();
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS4.ToggleVirtualCam();
                            break;
                    }
                }
                catch { }
            }
        }

        protected void TriggerOBS5(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS5.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    switch (configurationObject["method"].ToString())
                    {
                        case "start":
                            _ = PluginInstance.Main.OBS5.OutputsRequests.StartVirtualCamAsync();
                            break;
                        case "stop":
                            _ = PluginInstance.Main.OBS5.OutputsRequests.StopVirtualCamAsync();
                            break;
                        case "toggle":
                            _ = PluginInstance.Main.OBS5.OutputsRequests.ToggleVirtualCamAsync();
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
