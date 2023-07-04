using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetProfileAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetProfile;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetProfileDescription;

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = JObject.Parse(this.Configuration).ToObject<SetProfileConfig>();
                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? "");
                    if (conn == null) return;

                    _ = conn.OBS.ConfigRequests.SetCurrentProfileAsync(config.Profile);
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetProfileConfigView(this, actionConfigurator);
        }
    }
}
