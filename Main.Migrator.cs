using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Profiles;
using SuchByte.MacroDeck.Variables;
using SuchByte.OBSWebSocketPlugin.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin
{
    public partial class Main
    {
        private void MigrateVersion()
        {
            var version = GetConfigVersion();

            if (version.ToString() == "0.0.0") V1_5_0();

            MigrateActions();
            SetCurrentVersion();
        }

        private Version GetConfigVersion()
        {
            var versionString = PluginConfiguration.GetValue(this, "Version");
            versionString = versionString == "" ? "0.0.0" : versionString;
            return new Version(versionString);
        }

        private void SetCurrentVersion()
        {
            var current_version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            PluginConfiguration.SetValue(this, "Version", current_version.ToString());
        }

        private void V1_5_0()
        {
            var variables = VariableManager.GetVariables(this);
            foreach (var profile in ProfileManager.Profiles)
            {
                foreach (var folder in profile.Folders)
                {
                    foreach (var button in folder.ActionButtons)
                    {
                        foreach (var action in button.Actions)
                        {
                            if (action.GetType() == typeof(ConditionAction))
                            {
                                var condition = (ConditionAction)action;
                                if (condition.ConditionType == ConditionType.Variable && condition.ConditionValue1Source.StartsWith("obs_"))
                                {
                                    condition.ConditionValue1Source = String.Format("obs_default_{0}", condition.ConditionValue1Source.Replace("obs_", ""));
                                };
                            }
                        }
                        foreach (var action in button.ActionsRelease)
                        {
                            if (action.GetType() == typeof(ConditionAction))
                            {
                                var condition = (ConditionAction)action;
                                if (condition.ConditionType == ConditionType.Variable && condition.ConditionValue1Source.StartsWith("obs_"))
                                {
                                    condition.ConditionValue1Source = String.Format("obs_default_{0}", condition.ConditionValue1Source.Replace("obs_", ""));
                                };
                            }
                        }
                        foreach (var action in button.ActionsLongPress)
                        {
                            if (action.GetType() == typeof(ConditionAction))
                            {
                                var condition = (ConditionAction)action;
                                if (condition.ConditionType == ConditionType.Variable && condition.ConditionValue1Source.StartsWith("obs_"))
                                {
                                    condition.ConditionValue1Source = String.Format("obs_default_{0}", condition.ConditionValue1Source.Replace("obs_", ""));
                                };
                            }
                        }
                        foreach (var action in button.ActionsLongPressRelease)
                        {
                            if (action.GetType() == typeof(ConditionAction))
                            {
                                var condition = (ConditionAction)action;
                                if (condition.ConditionType == ConditionType.Variable && condition.ConditionValue1Source.StartsWith("obs_"))
                                {
                                    condition.ConditionValue1Source = String.Format("obs_default_{0}", condition.ConditionValue1Source.Replace("obs_", ""));
                                };
                            }
                        }
                        foreach (var listener in button.EventListeners)
                        {
                            if (listener.Parameter.StartsWith("obs_"))
                            {
                                listener.Parameter = String.Format("obs_default_{0}", listener.Parameter.Replace("obs_", ""));
                            }
                            foreach (var action in listener.Actions)
                            {
                                if (action.GetType() == typeof(ConditionAction))
                                {
                                    var condition = (ConditionAction)action;
                                    if (condition.ConditionType == ConditionType.Variable && condition.ConditionValue1Source.StartsWith("obs_"))
                                    {
                                        condition.ConditionValue1Source = String.Format("obs_default_{0}", condition.ConditionValue1Source.Replace("obs_", ""));
                                    };
                                }
                            }
                        }
                    }
                }
            }
            foreach (var variable in variables)
            {
                VariableManager.DeleteVariable(variable.Name);
            }
            PluginConfiguration.SetValue(this, "Version", "1.5.0");
        }

        private List<PluginAction> FindNestedActions(List<PluginAction> actions)
        {
            var allActions = new List<PluginAction>();
            foreach (var action in actions)
            {
                if (action.GetType() == typeof(ConditionAction))
                {
                    var condition = action as ConditionAction;
                    allActions.AddRange(FindNestedActions(condition.Actions));
                    allActions.AddRange(FindNestedActions(condition.ActionsElse));
                }
                else if ((action as ActionBase) != null)
                {
                    allActions.Add(action);
                }
            }
            return allActions;
        }

        private void MigrateActions()
        {
            List<ActionBase> upgradeableActions = new();
            foreach (var profile in ProfileManager.Profiles)
            {
                foreach (var folder in profile.Folders)
                {
                    foreach (var button in folder.ActionButtons)
                    {
                        foreach (var action in FindNestedActions(button.Actions))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                upgradeableActions.Add(actionBase);
                            }
                        }
                        foreach (var action in FindNestedActions(button.ActionsRelease))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                upgradeableActions.Add(actionBase);
                            }
                        }
                        foreach (var action in FindNestedActions(button.ActionsLongPress))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                upgradeableActions.Add(actionBase);
                            }
                        }
                        foreach (var action in FindNestedActions(button.ActionsLongPressRelease))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                upgradeableActions.Add(actionBase);
                            }
                        }
                        foreach (var listener in button.EventListeners)
                        {
                            foreach (var action in FindNestedActions(listener.Actions))
                            {
                                var actionBase = action as ActionBase;
                                var actionConfig = actionBase?.GetConfig();
                                if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                                {
                                    upgradeableActions.Add(actionBase);
                                }
                            }
                        }
                    }
                }
            }

            if (upgradeableActions.Count > 0)
            {
                var message = new MacroDeck.GUI.CustomControls.MessageBox();
                if (DialogResult.Yes ==
                    message.ShowDialog(
                        string.Format("{0}: Update Action Configs?", Name),
                        "Action configurations from previous versions were detected. Proceed to automatically update existing action configurations to newest version? WARNING: You should perform a backup before attempting this! While it should work, there's a chance it could fail.",
                        MessageBoxButtons.YesNo
                    )
                )
                {
                    foreach (var action in upgradeableActions)
                    {
                        var newConfig = action.GetConfig().UpgradeConfig();
                        action.Configuration = JObject.FromObject(newConfig).ToString();
                    }
                }
            }
            GC.Collect();
        }
    }
}
