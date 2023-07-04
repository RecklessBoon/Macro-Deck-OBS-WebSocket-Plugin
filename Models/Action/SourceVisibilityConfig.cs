using SuchByte.OBSWebSocketPlugin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public class SourceVisibilityConfig: ConfigBase
    {
        public string SceneName;
        public string SourceName;
        public VisibilityMethodType Method;
    }
}
