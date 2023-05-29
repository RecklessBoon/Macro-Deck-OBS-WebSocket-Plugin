using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Language;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin.GUI
{
    public partial class ProfileSelector : ActionConfigControl
    {
        PluginAction pluginAction;

        public ProfileSelector(PluginAction pluginAction, ActionConfigurator actionConfigurator)
        {
            this.pluginAction = pluginAction;
            InitializeComponent();

            this.lblProfile.Text = PluginLanguageManager.PluginStrings.Profile;

            this.LoadProfiles();
        }

        public override bool OnActionSave()
        {
            if (String.IsNullOrWhiteSpace(this.profilesBox.Text))
            {
                return false;
            }
            JObject configurationObject = JObject.FromObject(new
            {
                profile = this.profilesBox.Text,
            });

            this.pluginAction.Configuration = configurationObject.ToString();
            this.pluginAction.ConfigurationSummary = this.profilesBox.Text;
            return true;
        }

        private void LoadProfiles()
        {
            if (!PluginInstance.Main.IsConnected)
            {
                using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                {
                    msgBox.ShowDialog(LanguageManager.Strings.Error, PluginLanguageManager.PluginStrings.ErrorNotConnected, System.Windows.Forms.MessageBoxButtons.OK);
                }
                return;
            }

            this.profilesBox.Items.Clear();

            
            var self = this;
            _ = Task.Run(async () =>
            {
                var response = await PluginInstance.Main.Obs.ConfigRequests.GetProfileListAsync();
                foreach (var profile in response.Profiles)
                {
                    profilesBox.Invoke((MethodInvoker)delegate { profilesBox.Items.Add(profile); });
                }
                self.Invoke((MethodInvoker)delegate { LoadConfig(); });
            });
        }

        private void LoadConfig()
        {
            if (!String.IsNullOrWhiteSpace(this.pluginAction.Configuration))
            {
                try
                {
                    JObject configurationObject = JObject.Parse(this.pluginAction.Configuration);
                    this.profilesBox.Text = configurationObject["profile"].ToString();
                }
                catch { }
            }
        }

        private void BtnReloadProfiles_Click(object sender, EventArgs e)
        {
            this.LoadProfiles();
        }
    }
}
