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
            if(!PluginInstance.Main.Obs.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    switch (configurationObject["method"].ToString())
                    {
                        case "mute":
                            _ = PluginInstance.Main.Obs.InputsRequests.SetInputMuteAsync(sourceName, true);
                            break;
                        case "unmute":
                            _ = PluginInstance.Main.Obs.InputsRequests.SetInputMuteAsync(sourceName, false);
                            break;
                        case "toggle":
                            _ = Task.Run(async () =>
                            {
                                var mutedState = await PluginInstance.Main.Obs.InputsRequests.GetInputMuteAsync(sourceName);
                                await PluginInstance.Main.Obs.InputsRequests.SetInputMuteAsync(sourceName, !mutedState.InputMuted);
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
