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
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SourceVisibilityAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionSourceVisibility;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSourceVisibilityDescription;
        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<SourceVisibilityConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<SourceVisibilityConfig>();

                    var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    if (conn == null) return;

                    string sceneName = config.SceneName;
                    string sourceName = config.SourceName;

                    _ = Task.Run(async () =>
                    {
                        var sceneItemId = (await conn.OBS.SceneItemsRequests.GetSceneItemIdAsync(sceneName, sourceName))?.SceneItemId ?? -1;
                        if (sceneItemId == -1) return;
                        var sceneItemEnabled = (await conn.OBS.SceneItemsRequests.GetSceneItemEnabledAsync(sceneName, sceneItemId))?.SceneItemEnabled;
                        if (sceneItemEnabled == null) return;

                        switch (config.Method)
                        {
                            case Enum.VisibilityMethodType.Hide:
                                await conn.OBS.SceneItemsRequests.SetSceneItemEnabledAsync(sceneName, sceneItemId, false);
                                break;
                            case Enum.VisibilityMethodType.Show:
                                await conn.OBS.SceneItemsRequests.SetSceneItemEnabledAsync(sceneName, sceneItemId, true);
                                break;
                            case Enum.VisibilityMethodType.Toggle:
                                await conn.OBS.SceneItemsRequests.SetSceneItemEnabledAsync(sceneName, sceneItemId, !(bool)sceneItemEnabled);
                                break;
                        }
                    });
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SourceVisibilityConfigView(this, actionConfigurator);
        }



    }
}
