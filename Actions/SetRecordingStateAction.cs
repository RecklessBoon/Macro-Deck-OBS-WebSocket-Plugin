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
    public class SetRecordingStateAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetRecordingState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetRecordingStateDescription;

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
                            _ = PluginInstance.Main.Obs.RecordRequests.StartRecordAsync();
                            break;
                        case "stop":
                            _ = PluginInstance.Main.Obs.RecordRequests.StopRecordAsync();
                            break;
                        case "toggle":
                            _ = PluginInstance.Main.Obs.RecordRequests.ToggleRecordAsync();
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
