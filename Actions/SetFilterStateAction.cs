using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.Enum;
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
    public class SetFilterStateAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionFilterState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionFilterStateDescription;

        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<SetFilterStateConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    var config = GetConfig<SetFilterStateConfig>();

                    Connection conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                    if (conn == null) return;

                    string sceneName = config.SceneName;
                    string sourceName = config.SourceName;
                    string filterName = config.FilterName;
                    string targetName = String.IsNullOrWhiteSpace(sourceName) ? sceneName : sourceName;

                    switch (config.Method)
                    {
                        case Enum.VisibilityMethodType.Hide:
                            _ = conn.OBS.FiltersRequests.SetSourceFilterEnabledAsync(targetName, filterName, false);
                            break;
                        case Enum.VisibilityMethodType.Show:
                            _ = conn.OBS.FiltersRequests.SetSourceFilterEnabledAsync(targetName, filterName, true);
                            break;
                        case Enum.VisibilityMethodType.Toggle:
                            _ = Task.Run(async () =>
                            {
                                var filter = await conn.OBS.FiltersRequests.GetSourceFilterAsync(sourceName, filterName);
                                await conn.OBS.FiltersRequests.SetSourceFilterEnabledAsync(sourceName, filterName, !filter.FilterEnabled);
                            });
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new SetFilterStateConfigView(this, actionConfigurator);
        }
    }
}
