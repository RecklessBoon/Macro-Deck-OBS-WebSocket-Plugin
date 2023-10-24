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
    public class SetSceneAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetScene;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetSceneDescription;

        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<SetSceneConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<SetSceneConfig>();

                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    if (conn == null) return;

                    _ = conn.OBS.ScenesRequests.SetCurrentProgramSceneAsync(config.SceneName);
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetSceneConfigView(this, actionConfigurator);
        }
    }
}
