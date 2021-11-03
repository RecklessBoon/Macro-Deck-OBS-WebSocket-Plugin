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

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SourceVisibilityAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSourceVisibility;

        public override string DisplayName { get; set; } = PluginLanguageManager.PluginStrings.ActionSourceVisibility;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSourceVisibilityDescription;
        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sceneName = configurationObject["sceneName"].ToString();
                    string sourceName = configurationObject["sourceName"].ToString();

                    switch (configurationObject["method"].ToString())
                    {
                        case "hide":
                            PluginInstance.Main.OBS.SetSourceRender(sourceName, false, sceneName);
                            break;
                        case "show":
                            PluginInstance.Main.OBS.SetSourceRender(sourceName, true, sceneName);
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS.SetSourceRender(sourceName, !PluginInstance.Main.OBS.GetSceneItemProperties(sourceName, sceneName).Visible, sceneName);
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SceneSourceSelector(this, actionConfigurator);
        }



    }
}
