using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetTextValueAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSetTextValue;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetTextValueDescription;

        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.Obs.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    string value = configurationObject["value"].ToString();

                    _ = Task.Run(async () =>
                    {
                        var response = await PluginInstance.Main.Obs.InputsRequests.GetInputSettingsAsync(sourceName);
                        var inputSettings = JObject.FromObject(response.InputSettings);
                        inputSettings["text"] = VariableManager.RenderTemplate(value);

                        await PluginInstance.Main.Obs.InputsRequests.SetInputSettingsAsync(sourceName, inputSettings);
                    });
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetTextValueConfigurator(this, actionConfigurator);
        }
    }
}
