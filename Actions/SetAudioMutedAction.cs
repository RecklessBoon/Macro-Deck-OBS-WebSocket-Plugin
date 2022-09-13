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
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetAudioMutedAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetAudioMuted;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetAudioMutedDescription;

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
                    string sourceName = configurationObject["sourceName"].ToString();
                    switch (configurationObject["method"].ToString())
                    {
                        case "mute":
                            PluginInstance.Main.OBS4.SetMute(sourceName, true);
                            break;
                        case "unmute":
                            PluginInstance.Main.OBS4.SetMute(sourceName, false);
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS4.SetMute(sourceName, !PluginInstance.Main.OBS4.GetMute(sourceName));
                            break;
                    }
                }
                catch { }
            }
        }

        protected void TriggerOBS5(string clientId, ActionButton actionButton)
        {
            if(!PluginInstance.Main.OBS5.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    switch (configurationObject["method"].ToString())
                    {
                        case "mute":
                            _ = PluginInstance.Main.OBS5.InputsRequests.SetInputMuteAsync(sourceName, true);
                            break;
                        case "unmute":
                            _ = PluginInstance.Main.OBS5.InputsRequests.SetInputMuteAsync(sourceName, false);
                            break;
                        case "toggle":
                            _ = Task.Run(async () =>
                            {
                                var mutedState = await PluginInstance.Main.OBS5.InputsRequests.GetInputMuteAsync(sourceName);
                                await PluginInstance.Main.OBS5.InputsRequests.SetInputMuteAsync(sourceName, !mutedState.InputMuted);
                            });
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
