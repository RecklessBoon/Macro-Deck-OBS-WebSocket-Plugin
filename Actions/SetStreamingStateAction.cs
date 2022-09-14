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
    public class SetStreamingStateAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetStreamingState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetStreamingStateDescription;

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
                            PluginInstance.Main.OBS4.StartStreaming();
                            break;
                        case "stop":
                            PluginInstance.Main.OBS4.StopStreaming();
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS4.ToggleStreaming();
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
                            _ = PluginInstance.Main.OBS5.StreamRequests.StartStreamAsync();
                            break;
                        case "stop":
                            _ = PluginInstance.Main.OBS5.StreamRequests.StopStreamAsync();
                            break;
                        case "toggle":
                            _ = PluginInstance.Main.OBS5.StreamRequests.ToggleStreamAsync();
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
