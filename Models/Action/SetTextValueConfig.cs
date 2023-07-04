using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public class SetTextValueConfig: ConfigBase
    {
        public string SceneName;
        public string SourceName;
        public string Value;
    }
}
