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
    public class SetAudioMutedAction : PluginAction
    {
        public override string Name => "OBS Set audio muted";

        public override string DisplayName { get; set; } = "OBS Set audio muted";

        public override string Description => "Mute/unmute/toggle audio source";
        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    switch (configurationObject["method"].ToString())
                    {
                        case "mute":
                            PluginInstance.Main.OBS.SetMute(sourceName, true);
                            break;
                        case "unmute":
                            PluginInstance.Main.OBS.SetMute(sourceName, false);
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS.SetMute(sourceName, !PluginInstance.Main.OBS.GetMute(sourceName));
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new AudioSourceSelector(this, actionConfigurator);
        }
    }


}
