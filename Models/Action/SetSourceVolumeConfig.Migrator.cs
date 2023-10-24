using SuchByte.OBSWebSocketPlugin.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SuchByte.OBSWebSocketPlugin.Models.Action
{
    public partial class SetSourceVolumeConfig
    {
        protected override void Upgrade(int prevVersion)
        {
            if (prevVersion == 0) V1();
        }

        private void V1()
        {
            Version = 1;
            ConnectionName = PluginInstance.Main.Connections.FirstOrDefault().Key;
        }        
    }
}
