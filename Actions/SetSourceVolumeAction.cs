using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetSourceVolumeAction : ActionBase
    {

        public override string Name => PluginLanguageManager.PluginStrings.ActionSetSourceVolume;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetSourceVolumeDescription;
        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<SetSourceVolumeConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<SetSourceVolumeConfig>();

                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    if (conn == null) return;

                    string sourceName = config.SourceName;
                    _ = Task.Run(async () =>
                    {
                        int currentDecibel = (await conn.OBS.InputsRequests.GetInputVolumeAsync(sourceName)).InputVolumeDb; // fallback if parse failed
                        switch (config.Method)
                        {
                            case Enum.IncrementalMethodType.Set:
                                await conn.OBS.InputsRequests.SetInputVolumeAsync(sourceName, inputVolumeDb: config.Decibel);
                                break;
                            case Enum.IncrementalMethodType.Increase:
                                await conn.OBS.InputsRequests.SetInputVolumeAsync(sourceName, inputVolumeDb: currentDecibel + config.Decibel);
                                break;
                            case Enum.IncrementalMethodType.Decrease:
                                await conn.OBS.InputsRequests.SetInputVolumeAsync(sourceName, inputVolumeDb: currentDecibel - config.Decibel);
                                break;
                        }
                    });
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetSourceVolumeConfigView(this, actionConfigurator);
        }


    }
}
