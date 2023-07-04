using SuchByte.OBSWebSocketPlugin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public class SetAudioMutedConfig: ConfigBase
    {
        public string SourceName;
        public AudioMethodType Method;
    }
}
