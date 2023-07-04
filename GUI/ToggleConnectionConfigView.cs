using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class ToggleConnectionConfigView : ActionConfigControl
    {

        PluginAction pluginAction;
        ToggleConnectionConfig config;

        public ToggleConnectionConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            // TODO: Add Connection label strings
            //this.lblSource.Text = PluginLanguageManager.PluginStrings.Source;
            //this.radioMute.Text = PluginLanguageManager.PluginStrings.Mute;
            //this.radioUnmute.Text = PluginLanguageManager.PluginStrings.Unmute;
            //this.radioToggle.Text = PluginLanguageManager.PluginStrings.Toggle;

            LoadConfig();
            ResetValues();
        }

        public override bool OnActionSave()
        {
            if (rbSingleConnection.Checked && String.IsNullOrWhiteSpace(connectionSelector1.Value))
            {
                return false;
            }
            var selectionType = Enum.SelectionType.All;
            if (this.rbSingleConnection.Checked)
            {
                selectionType = Enum.SelectionType.Single;
            }
            else if (this.rbAllConnections.Checked)
            {
                selectionType = Enum.SelectionType.All;
            }

            var config = JObject.FromObject(new ToggleConnectionConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                SelectionType = selectionType,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = "Toggle Connection " + selectionType.ToString();
            return true;
        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<ToggleConnectionConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;

            switch (config?.SelectionType ?? Enum.SelectionType.All)
            {
                case Enum.SelectionType.All:
                    this.rbAllConnections.Checked = true;
                    break;
                case Enum.SelectionType.Single:
                    this.rbSingleConnection.Checked = true;
                    break;
            }
        }

        private void rbSingleConnection_CheckedChanged(object sender, EventArgs e)
        {
            connectionSelector1.Enabled = this.rbSingleConnection.Checked;
        }
    }
}
