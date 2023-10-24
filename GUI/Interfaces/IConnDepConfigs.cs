using SuchByte.OBSWebSocketPlugin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.GUI.Interfaces
{
    public interface IConnDepConfigs
    {
        virtual Connection Conn {
            get
            {
                var val = ConnectionSelector.Value ?? "";
                var list = PluginInstance.Main.Connections;
                return list.ContainsKey(val) ? list.GetValueOrDefault(val) : null;
            }
        }

        ConnectionSelector ConnectionSelector { get; }
    }
}
