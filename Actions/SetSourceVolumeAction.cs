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
using System.Threading.Tasks;

namespace SuchByte.OBSWebSocketPlugin.Actions
{
    public class SetSourceVolumeAction : PluginAction
    {

        public override string Name => PluginLanguageManager.PluginStrings.ActionSetSourceVolume;

        public override string Description => PluginLanguageManager.PluginStrings.ActionSetSourceVolumeDescription;
        public override bool CanConfigure => true;

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            if (PluginInstance.Main.OBS4 != null) TriggerOBS4(clientId, actionButton);
            else if (PluginInstance.Main.OBS5 != null) TriggerOBS5(clientId, actionButton);
        }

        protected void TriggerOBS4(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS4.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    int decibel = (int)PluginInstance.Main.OBS4.GetVolume(sourceName, true).Volume; // fallback if parse failed
                    switch (configurationObject["method"].ToString())
                    {
                        case "set":
                            Int32.TryParse(configurationObject["decibel"].ToString(), out decibel);
                            PluginInstance.Main.OBS4.SetVolume(sourceName, decibel, true);
                            break;
                        case "increase":
                            Int32.TryParse(configurationObject["decibel"].ToString(), out int increaseByDecibel);
                            PluginInstance.Main.OBS4.SetVolume(sourceName, decibel + increaseByDecibel, true);
                            break;
                        case "decrease":
                            Int32.TryParse(configurationObject["decibel"].ToString(), out int decreaseByDecibel);
                            PluginInstance.Main.OBS4.SetVolume(sourceName, decibel - decreaseByDecibel, true);
                            break;
                    }
                }
                catch{ }
            }
        }

        protected void TriggerOBS5(string clientId, ActionButton actionButton)
        {
            if (!PluginInstance.Main.OBS5.IsConnected) return;
            if (!String.IsNullOrWhiteSpace(this.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.Configuration);
                    string sourceName = configurationObject["sourceName"].ToString();
                    _ = Task.Run(async () =>
                    {
                        int decibel = (await PluginInstance.Main.OBS5.InputsRequests.GetInputVolumeAsync(sourceName)).InputVolumeDb; // fallback if parse failed
                        switch (configurationObject["method"].ToString())
                        {
                            case "set":
                                Int32.TryParse(configurationObject["decibel"].ToString(), out decibel);
                                await PluginInstance.Main.OBS5.InputsRequests.SetInputVolumeAsync(sourceName, inputVolumeDb: decibel);
                                break;
                            case "increase":
                                Int32.TryParse(configurationObject["decibel"].ToString(), out int increaseByDecibel);
                                await PluginInstance.Main.OBS5.InputsRequests.SetInputVolumeAsync(sourceName, inputVolumeDb: decibel + increaseByDecibel);
                                break;
                            case "decrease":
                                Int32.TryParse(configurationObject["decibel"].ToString(), out int decreaseByDecibel);
                                await PluginInstance.Main.OBS5.InputsRequests.SetInputVolumeAsync(sourceName, inputVolumeDb: decibel - decreaseByDecibel);
                                break;
                        }
                    });
                }
                catch { }
            }
        }

        public override ActionConfigControl GetActionConfigControl(ActionConfigurator actionConfigurator)
        {
            return new AudioSourceVolumeSelector(this, actionConfigurator);
        }


    }
}
