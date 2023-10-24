using Newtonsoft.Json.Linq;
using SuchByte.OBSWebSocketPlugin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public partial class GenericStateConfig: ConfigBase
    {
        public override int TargetVersion => 1;
        public StateMethodType Method;
    }
}
