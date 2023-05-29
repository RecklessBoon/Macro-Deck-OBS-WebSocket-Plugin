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
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SourceVisibilityAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSourceVisibility;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSourceVisibilityDescription;
        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.Obs.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sceneName = configurationObject["sceneName"].ToString();
                    string sourceName = configurationObject["sourceName"].ToString();

                    _ = Task.Run(async () =>
                    {
                        var sceneItemId = (await PluginInstance.Main.Obs.SceneItemsRequests.GetSceneItemIdAsync(sceneName, sourceName))?.SceneItemId ?? -1;
                        if (sceneItemId == -1) return;
                        var sceneItemEnabled = (await PluginInstance.Main.Obs.SceneItemsRequests.GetSceneItemEnabledAsync(sceneName, sceneItemId))?.SceneItemEnabled;
                        if (sceneItemEnabled == null) return;

                        switch (configurationObject["method"].ToString())
                        {
                            case "hide":
                                await PluginInstance.Main.Obs.SceneItemsRequests.SetSceneItemEnabledAsync(sceneName, sceneItemId, false);
                                break;
                            case "show":
                                await PluginInstance.Main.Obs.SceneItemsRequests.SetSceneItemEnabledAsync(sceneName, sceneItemId, true);
                                break;
                            case "toggle":
                                await PluginInstance.Main.Obs.SceneItemsRequests.SetSceneItemEnabledAsync(sceneName, sceneItemId, !(bool)sceneItemEnabled);
                                break;
                        }
                    });
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
