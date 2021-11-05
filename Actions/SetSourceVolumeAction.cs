using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetSourceVolumeAction : PluginAction
    {

        public override string Name => PluginLanguageManager.PluginStrings.ActionSetSourceVolume;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetSourceVolumeDescription;
        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    int decibel = (int)PluginInstance.Main.OBS.GetVolume(sourceName, true).Volume; // fallback if parse failed
                    switch (configurationObject["method"].ToString())
                    {
                        case "set":
                            Int32.TryParse(configurationObject["decibel"].ToString(), out decibel);
                            PluginInstance.Main.OBS.SetVolume(sourceName, decibel, true);
                            break;
                        case "increase":
                            Int32.TryParse(configurationObject["decibel"].ToString(), out int increaseByDecibel);
                            PluginInstance.Main.OBS.SetVolume(sourceName, decibel + increaseByDecibel, true);
                            break;
                        case "decrease":
                            Int32.TryParse(configurationObject["decibel"].ToString(), out int decreaseByDecibel);
                            PluginInstance.Main.OBS.SetVolume(sourceName, decibel - decreaseByDecibel, true);
                            break;
                    }
                }
                catch{ }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new AudioSourceVolumeSelector(this, actionConfigurator);
        }


    }
}
