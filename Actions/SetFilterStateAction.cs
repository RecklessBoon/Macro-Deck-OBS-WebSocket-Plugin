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
                    string sceneName = configurationObject["sceneName"].ToString();
                    string sourceName = configurationObject["sourceName"].ToString();
                    string filterName = configurationObject["filterName"].ToString();

                    string targetName = String.IsNullOrWhiteSpace(sourceName) ? sceneName : sourceName;

                    switch (configurationObject["method"].ToString())
                    {
                        case "hide":
                            PluginInstance.Main.OBS4.SetSourceFilterVisibility(targetName, filterName, false);
                            break;
                        case "show":
                            PluginInstance.Main.OBS4.SetSourceFilterVisibility(targetName, filterName, true);
                            break;
                        case "toggle":
                            PluginInstance.Main.OBS4.SetSourceFilterVisibility(targetName, filterName, !PluginInstance.Main.OBS4.GetSourceFilterInfo(targetName, filterName).IsEnabled);
                            break;
                    }
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
                    string sceneName = configurationObject["sceneName"].ToString();
                    string sourceName = configurationObject["sourceName"].ToString();
                    string filterName = configurationObject["filterName"].ToString();

                    string targetName = String.IsNullOrWhiteSpace(sourceName) ? sceneName : sourceName;

                    switch (configurationObject["method"].ToString())
                    {
                        case "hide":
                            _ = PluginInstance.Main.OBS5.FiltersRequests.SetSourceFilterEnabledAsync(targetName, filterName, false);
                            break;
                        case "show":
                            _ = PluginInstance.Main.OBS5.FiltersRequests.SetSourceFilterEnabledAsync(targetName, filterName, true);
                            break;
                        case "toggle":
                            _ = Task.Run(async () =>
                            {
                                var filter = await PluginInstance.Main.OBS5.FiltersRequests.GetSourceFilterAsync(sourceName, filterName);
                                await PluginInstance.Main.OBS5.FiltersRequests.SetSourceFilterEnabledAsync(sourceName, filterName, !filter.FilterEnabled);
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
