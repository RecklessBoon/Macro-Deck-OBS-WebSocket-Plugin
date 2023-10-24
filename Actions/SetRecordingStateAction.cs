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
using System.Linq;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetRecordingStateAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetRecordingState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetRecordingStateDescription;

        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<GenericStateConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<GenericStateConfig>();

                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    if (conn == null) return;

                    switch (config.Method)
                    {
                        case Enum.StateMethodType.Start:
                            _ = conn.OBS.RecordRequests.StartRecordAsync();
                            break;
                        case Enum.StateMethodType.Stop:
                            _ = conn.OBS.RecordRequests.StopRecordAsync();
                            break;
                        case Enum.StateMethodType.Toggle:
                            _ = conn.OBS.RecordRequests.ToggleRecordAsync();
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new GenericStateConfigView(this, actionConfigurator);
        }
    }
}
