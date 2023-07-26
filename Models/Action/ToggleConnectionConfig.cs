using SuchByte.OBSWebSocketPlugin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public partial class ToggleConnectionConfig: ConfigBase
    {
        public override int TargetVersion => 1;

        public SelectionType SelectionType;
    }
}
