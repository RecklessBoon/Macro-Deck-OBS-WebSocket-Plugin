using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public interface IConfigurableAction
    {
        public ConfigBase GetConfig();
        public T GetConfig<T>() where T : ConfigBase;
    }
}
