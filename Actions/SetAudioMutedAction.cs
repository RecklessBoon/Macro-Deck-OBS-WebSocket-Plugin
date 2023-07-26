using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.Enum;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetAudioMutedAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetAudioMuted;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetAudioMutedDescription;

        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<SetAudioMutedConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<SetAudioMutedConfig>();
                    string sourceName = config.SourceName;
                    Connection conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    switch (config.Method)
                    {
                        case AudioMethodType.Mute:
                            _ = conn.OBS.InputsRequests.SetInputMuteAsync(sourceName, true);
                            break;
                        case AudioMethodType.Unmute:
                            _ = conn.OBS.InputsRequests.SetInputMuteAsync(sourceName, false);
                            break;
                        case AudioMethodType.Toggle:
                            _ = Task.Run(async () =>
                            {
                                var mutedState = await conn.OBS.InputsRequests.GetInputMuteAsync(sourceName);
                                await conn.OBS.InputsRequests.SetInputMuteAsync(sourceName, !mutedState.InputMuted);
                            });
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetAudioMutedConfigView(this, actionConfigurator);
        }
    }


}
