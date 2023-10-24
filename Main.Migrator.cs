using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.DataTypes.Core;
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
            var configVersion = GetConfigVersion();
            var currentVersion = GetCurrentVersion();

            if (configVersion != currentVersion)
            {
                var choice = PromptAutoUpdate();
                if (choice == DialogResult.Yes)
                {
                    MigrateConfigs();
                    MigrateActions();
                    SetCurrentVersion();
                } else if (choice == DialogResult.No)
                {
                    var skip = PromptSkipUpdate();
                    if (skip == DialogResult.Yes)
                    {
                        SetCurrentVersion();
                    }
                }
            }
        }

        private DialogResult PromptAutoUpdate()
        {
            var message = new MacroDeck.GUI.CustomControls.MessageBox();
            message.Width = 500;
            message.Height = 300;
            return message.ShowDialog(
                string.Format("{0}: Auto-Update Buttons?", Name),
                "Actions and/or Condition configurations can be updated from the previous version for you automatically.\n\n>> WARNING <<\nYou should perform a backup before attempting this! While it should work, there's a chance it could fail.\n\nProceed to automatically update existing configurations to newest version?",
                MessageBoxButtons.YesNo
            );
        }

        private DialogResult PromptSkipUpdate()
        {
            var message = new MacroDeck.GUI.CustomControls.MessageBox();
            return message.ShowDialog(
                string.Format("{0}: Skip Auto-Update Buttons?", Name),
                "Select \"Yes\" to never see the Auto-Update Buttons dialog for this version again. Select \"No\" to show it next time Macro Deck is started.",
                MessageBoxButtons.YesNo
            );
        }

        private System.Version GetConfigVersion()
        {
            var versionString = PluginConfiguration.GetValue(this, "Version");
            versionString = versionString == "" ? "0.0.0" : versionString;
            return new System.Version(versionString);
        }

        private System.Version GetCurrentVersion()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
        }

        private void SetCurrentVersion()
        {
            var current_version = GetCurrentVersion();
            PluginConfiguration.SetValue(this, "Version", current_version.ToString());
        }

        private void MigrateConfigs()
        {
            var version = GetConfigVersion();

            if (version.ToString() == "0.0.0")
            {
                V1_5_0();
            }
        }

        private void V1_5_0()
        {
            var variables = VariableManager.GetVariables(this);
            foreach (var variable in variables)
            {
                if (!variable.Name.StartsWith("obs_default_"))
                {
                    VariableManager.SetValue(String.Format("obs_default_{0}", variable.Name.Replace("obs_", "")), variable.Value, variable.VariableType, PluginInstance.Main, null);
                }
            }

            foreach (var profile in ProfileManager.Profiles)
            {
                foreach (var folder in profile.Folders)
                {
                    foreach (var button in folder.ActionButtons)
                    {
                        if (button.StateBindingVariable.StartsWith("obs_"))
                        {
                            button.StateBindingVariable = String.Format("obs_default_{0}", button.StateBindingVariable.Replace("obs_", ""));
                        }

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
                if (!variable.Name.StartsWith("obs_default_"))
                {
                    VariableManager.DeleteVariable(variable.Name);
                }
            }
            GC.Collect();
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
                                var newConfig = actionBase.GetConfig().UpgradeConfig();
                                action.Configuration = JObject.FromObject(newConfig).ToString();
                            }
                        }
                        foreach (var action in FindNestedActions(button.ActionsRelease))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                var newConfig = actionBase.GetConfig().UpgradeConfig();
                                action.Configuration = JObject.FromObject(newConfig).ToString();
                            }
                        }
                        foreach (var action in FindNestedActions(button.ActionsLongPress))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                var newConfig = actionBase.GetConfig().UpgradeConfig();
                                action.Configuration = JObject.FromObject(newConfig).ToString();
                            }
                        }
                        foreach (var action in FindNestedActions(button.ActionsLongPressRelease))
                        {
                            var actionBase = action as ActionBase;
                            var actionConfig = actionBase?.GetConfig();
                            if (actionBase != null && actionConfig.Version != actionConfig.TargetVersion)
                            {
                                var newConfig = actionBase.GetConfig().UpgradeConfig();
                                action.Configuration = JObject.FromObject(newConfig).ToString();
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
                                    var newConfig = actionBase.GetConfig().UpgradeConfig();
                                    action.Configuration = JObject.FromObject(newConfig).ToString();
                                }
                            }
                        }
                    }
                }
            }
            GC.Collect();
        }
    }
}
