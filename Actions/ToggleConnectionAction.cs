using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class ToggleConnectionAction : ActionBase
    {
        public override string Name => PluginLanguageManager.PluginStrings.ActionToggleConnection;

        public override string Description => PluginLanguageManager.PluginStrings.ActionToggleConnectionDescription;

        public override bool CanConfigure => true;

        public override ConfigBase GetConfig() => GetConfig<ToggleConnectionConfig>();

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var config = GetConfig<ToggleConnectionConfig>();

            if (config.SelectionType == Enum.SelectionType.All)
            {
                foreach(var pair in PluginInstance.Main.Connections)
                {
                    Toggle(pair.Value);
                }
            } else
            {
                var conn = PluginInstance.Main.Connections.GetValueOrDefault(config?.ConnectionName ?? PluginInstance.Main.Connections.FirstOrDefault().Key);
                Toggle(conn);
            }


        }

        private static void Toggle(Connection conn)
        {
            if (conn == null) return;

            if (conn.IsConnected)
            {
                conn.Dispose();
            }
            else
            {
                var newConn = Connection.FromPrev(conn);
                PluginInstance.Main.StoreConnectConnectionAsync(newConn);
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new ToggleConnectionConfigView(this, actionConfigurator);
        }
    }
}
