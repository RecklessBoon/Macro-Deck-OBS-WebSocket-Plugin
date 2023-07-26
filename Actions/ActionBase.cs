using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public abstract class ActionBase: PluginAction, IConfigurableAction
    {
        public abstract ConfigBase GetConfig();

        public T GetConfig<T>() where T : ConfigBase 
        {
            return JObject.Parse(this.Configuration ?? "{}").ToObject<T>();
        }
    }
}
