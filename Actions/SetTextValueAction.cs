using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using SuchByte.MacroDeck.Variables.Plugin.GUI;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Text;
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
                    string value = configurationObject["value"].ToString();

                    TextGDIPlusProperties properties = PluginInstance.Main.OBS4.GetTextGDIPlusProperties(sourceName);
                    properties.Text = VariableManager.RenderTemplate(value);
                    PluginInstance.Main.OBS4.SetTextGDIPlusProperties(properties);
                }
                catch { }
            }
        }

        protected void TriggerOBS5(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS5.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    string value = configurationObject["value"].ToString();

                    _ = Task.Run(async () =>
                    {
                        var response = await PluginInstance.Main.OBS5.InputsRequests.GetInputSettingsAsync(sourceName);
                        var inputSettings = JObject.FromObject(response.InputSettings);
                        inputSettings["text"] = VariableManager.RenderTemplate(value);

                        await PluginInstance.Main.OBS5.InputsRequests.SetInputSettingsAsync(sourceName, inputSettings);
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
