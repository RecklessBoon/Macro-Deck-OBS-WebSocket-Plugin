using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Enum;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class GenericStateConfigView : ActionConfigControl
    {

        PluginAction pluginAction;
        GenericStateConfig config;

        public GenericStateConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.radioStart.Text = PluginLanguageManager.PluginStrings.Start;
            this.radioStop.Text = PluginLanguageManager.PluginStrings.Stop;
            this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            LoadConfig();
            ResetValues();
        }

        public override bool OnActionSave()
        {
            var method = StateMethodType.Toggle;
            if (this.radioStart.Checked)
            {
                method = StateMethodType.Start;
            }
            else if (this.radioStop.Checked)
            {
                method = StateMethodType.Stop;
            }
            else if (this.radioToggle.Checked)
            {
                method = StateMethodType.Toggle;
            }
            var config = JObject.FromObject(new GenericStateConfig
            {
                ConnectionName = connectionSelector1.Value,
                Method = method,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = method.ToString();
            return true;
        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<GenericStateConfig>();
                    
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            connectionSelector1.Value = config?.ConnectionName;

            switch (config?.Method ?? StateMethodType.Toggle)
            {
                case StateMethodType.Start:
                    this.radioStart.Checked = true;
                    break;
                case StateMethodType.Stop:
                    this.radioStop.Checked = true;
                    break;
                case StateMethodType.Toggle:
                    this.radioToggle.Checked = true;
                    break;
            }
        }

    }
}