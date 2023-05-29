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
    public class SetFilterStateAction : PluginAction
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionFilterState;

        public override string Description => PluginLanguageManager.PluginStrings.ActionFilterStateDescription;

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
                    string filterName = configurationObject["filterName"].ToString();

                    string targetName = String.IsNullOrWhiteSpace(sourceName) ? sceneName : sourceName;

                    switch (configurationObject["method"].ToString())
                    {
                        case "hide":
                            _ = PluginInstance.Main.Obs.FiltersRequests.SetSourceFilterEnabledAsync(targetName, filterName, false);
                            break;
                        case "show":
                            _ = PluginInstance.Main.Obs.FiltersRequests.SetSourceFilterEnabledAsync(targetName, filterName, true);
                            break;
                        case "toggle":
                            _ = Task.Run(async () =>
                            {
                                var filter = await PluginInstance.Main.Obs.FiltersRequests.GetSourceFilterAsync(sourceName, filterName);
                                await PluginInstance.Main.Obs.FiltersRequests.SetSourceFilterEnabledAsync(sourceName, filterName, !filter.FilterEnabled);
                            });
                            break;
                    }
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new FilterSelector(this, actionConfigurator);
        }
    }
}
