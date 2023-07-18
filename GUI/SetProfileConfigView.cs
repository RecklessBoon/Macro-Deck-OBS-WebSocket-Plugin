using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Controllers;
using SuchByte.OBSWebSocketPlugin.GUI.Interfaces;
using SuchByte.OBSWebSocketPlugin.Language;
using SuchByte.OBSWebSocketPlugin.Models.Action;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class SetProfileConfigView : ActionConfigControl, IConnDepConfigs
    {
        PluginAction pluginAction;
        SetProfileConfig config;

        public ConnectionSelector ConnectionSelector => connectionSelector1;

        public SetProfileConfigView(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblConnection.Text = PluginLanguageManager.PluginStrings.Connection;
            this.lblProfile.Text = PluginLanguageManager.PluginStrings.Profile;

            LoadConfig();
            ResetValues();
            LoadProfiles();

            connectionSelector1.ValueChanged += (sender, args) => LoadProfiles();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.profilesBox.Text))
            {
                return false;
            }
            var config = JObject.FromObject(new SetProfileConfig
            {
                ConnectionName = this.connectionSelector1.Value,
                Profile = this.profilesBox.Text,
            });

            this.pluginAction.Configuration = config.ToString();
            this.pluginAction.ConfigurationSummary = this.profilesBox.Text;
            return true;
        }

        private void LoadProfiles()
        {
            var conn = (this as IConnDepConfigs).Conn;
            if (conn == null) return;

            if (!(conn?.IsConnected ?? false))
            {
                using var msgBox = new MacroDeck.GUI.CustomControls.MessageBox();
                msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }

            this.profilesBox.Items.Clear();

            var self = this;
            _ = Task.Run(async () =>
            {
                var response = await conn.OBS.ConfigRequests.GetProfileListAsync();
                foreach (var profile in response.Profiles)
                {
                    profilesBox.Invoke((MethodInvoker)delegate { profilesBox.Items.Add(profile); });
                }
                self.Invoke((MethodInvoker)delegate
                {
                    profilesBox.Text = config?.Profile;
                });
            });
        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    config = JObject.Parse(this.pluginAction.Configuration).ToObject<SetProfileConfig>();
                }
                catch { }
            }
        }

        private void ResetValues()
        {
            this.connectionSelector1.Value = config?.ConnectionName;
            this.profilesBox.Text = config?.Profile;
        }

        private void BtnReloadProfiles_Click(object sender, EventArgs e)
        {
            this.LoadProfiles();
        }
    }
}
