using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class StateSelector : ActionConfigControl
    {

        PluginAction pluginAction;

        public StateSelector(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.radioStart.Text = PluginLanguageManager.PluginStrings.Start;
            this.radioStop.Text = PluginLanguageManager.PluginStrings.Stop;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            this.LoadConfig();

            actionConfigurator.ActionSave += OnActionSave;
        }

        private void OnActionSave(object sender, EventArgs e)
        {
            this.UpdateConfig();
        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.pluginAction.Configuration);
                    switch (configurationObject["method"].ToString())
                    {
                        case "start":
                            this.radioStart.Checked = true;
                            break;
                        case "stop":
                            this.radioStop.Checked = true;
                            break;
                        case "toggle":
                            this.radioToggle.Checked = true;
                            break;
                    }
                }
                catch { }
            }
        }

        private void UpdateConfig()
        {
            string method = "toggle";
            if (this.radioStart.Checked)
            {
                method = "start";
            } else if (this.radioStop.Checked)
            {
                method = "stop";
            } else if (this.radioToggle.Checked)
            {
                method = "toggle";
            }
            JObject configurationObject = JObject.FromObject(new
            {
                method = method,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.DisplayName = this.pluginAction.Name + " -> " + method;
        }



    }
}