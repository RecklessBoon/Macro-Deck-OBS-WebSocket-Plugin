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
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = JObject.Parse(this.Configuration).ToObject<GenericStateConfig>();

                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? "");
                    if (conn == null) return;

                    switch (config.Method)
                    {
                        case Enum.StateMethodType.Start:
                            _ = conn.OBS.OutputsRequests.StartVirtualCamAsync();
                            break;
                        case Enum.StateMethodType.Stop:
                            _ = conn.OBS.OutputsRequests.StopVirtualCamAsync();
                            break;
                        case Enum.StateMethodType.Toggle:
                            _ = conn.OBS.OutputsRequests.ToggleVirtualCamAsync();
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
