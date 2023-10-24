using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.CottleIntegration;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetTextValueAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetTextValue;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetTextValueDescription;

        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<SetTextValueConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<SetTextValueConfig>();

                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    if (conn == null) return;

                    string sourceName = config.SourceName;
                    string value = config.Value;

                    _ = Task.Run(async () =>
                    {
                        var response = await conn.OBS.InputsRequests.GetInputSettingsAsync(sourceName);
                        var inputSettings = JObject.FromObject(response.InputSettings);
                        inputSettings["text"] = TemplateManager.RenderTemplate(value);

                        await conn.OBS.InputsRequests.SetInputSettingsAsync(sourceName, inputSettings);
                    });
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetTextValueConfigView(this, actionConfigurator);
        }
    }
}
