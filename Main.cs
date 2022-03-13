using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Actions;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuchByte.OBSWebSocketPlugin {

    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        private string variablePrefix = "obs_";

        private string[] sceneSuggestions = new string[] { "" };

        public override string Description => "Control Open Broadcaster Software with Macro Deck.";
        public override bool CanConfigure => true;

        public OBSWebsocket OBS { get { return this.obs; } }

        protected OBSWebsocket obs;

        private ContentSelectorButton statusButton = new ContentSelectorButton();

        private ToolTip statusToolTip = new ToolTip();

        private MainWindow mainWindow;

        public Main()
        {
            PluginInstance.Main = this;
            MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            if (PluginCredentials.GetPluginCredentials(this) == null || PluginCredentials.GetPluginCredentials(this).Count == 0)
            {
                this.OpenConfigurator();
            }
            else
            {
                Task.Run(() =>
                {
                    this.Connect(false);
                });
            }
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            mainWindow = sender as MainWindow;

            if (this.obs == null) return;

            this.statusButton = new ContentSelectorButton
            {
                BackgroundImage = this.obs.IsConnected ? Properties.Resources.OBS_Online : Properties.Resources.OBS_Offline,
                BackgroundImageLayout = ImageLayout.Stretch,

            };
            this.statusToolTip.SetToolTip(this.statusButton, "OBS " + (this.obs.IsConnected ? " Connected" : "Disconnected"));
            statusButton.Click += StatusButton_Click;
            mainWindow.contentButtonPanel.Controls.Add(statusButton);
        }

        public override Image Icon => Properties.Resources.OBS_WebSocket;

        public override void Enable()
        {
            PluginLanguageManager.Initialize();
            this.Actions = new List<PluginAction>()
            {
                new SetReplayBufferState(),
                new SaveReplayBufferAction(),
                new SourceVisibilityAction(),
                new SetSourceVolumeAction(),
                new SetAudioMutedAction(),
                new SetProfileAction(),
                new SetRecordingStateAction(),
                new SetSceneAction(),
                new SetStreamingStateAction(),
                new SetVirtualCamAction(),
                new ToggleConnectionAction(),
            };
            ResetVariables();
            this.obs = new OBSWebsocket();
            this.obs.Connected += OnConnect;
            this.obs.Disconnected += OnDisconnect;

            this.obs.SceneChanged += OnSceneChange;
            this.obs.ProfileChanged += OnProfileChange;
           // this.obs.TransitionChanged += OnTransitionChange; // TODO

            this.obs.StreamingStateChanged += OnStreamingStateChange;
            this.obs.RecordingStateChanged += OnRecordingStateChange;

            this.obs.VirtualCameraStarted += OnVirtualCameraStarted;
            this.obs.VirtualCameraStopped += OnVirtualCameraStopped;

            this.obs.SceneListChanged += Obs_SceneListChanged;

            this.obs.SourceVolumeChanged += Obs_SourceVolumeChanged;

            this.obs.StreamStatus += OnStreamData;

            this.obs.SourceMuteStateChanged += Obs_SourceMuteStateChanged;

            this.obs.SceneItemVisibilityChanged += Obs_SceneItemVisibilityChanged;

            this.obs.ReplayBufferStateChanged += Obs_ReplayBufferStateChanged;

            Task.Run(() =>
            {
                this.Connect();
            });
            
        }

        private void Obs_SceneItemVisibilityChanged(OBSWebsocket sender, string sceneName, string itemName, bool isVisible)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + sceneName + "/" + itemName, isVisible ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, true);
        }

        private void Obs_SourceVolumeChanged(OBSWebsocket sender, string sourceName, float volume)
        {
            volume = PluginInstance.Main.OBS.GetVolume(sourceName, true).Volume; // get volume in dB
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + sourceName + " volume_db", (int)volume, MacroDeck.Variables.VariableType.Integer, this, true);
        }

        private void Obs_SourceMuteStateChanged(OBSWebsocket sender, string sourceName, bool muted)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + sourceName, muted ? "False" : "True", MacroDeck.Variables.VariableType.Bool, this, true);
        }

        private void Obs_SceneListChanged(object sender, EventArgs e)
        {
            List<string> scenesList = new List<string>();
            foreach (OBSScene scene in this.obs.ListScenes())
            {
                scenesList.Add(scene.Name);
            }
            this.sceneSuggestions = scenesList.ToArray();
        }

        private void ResetVariables()
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "connected", "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual_camera", "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "stream_time", "0h:0m:0s", MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "kbits", 0, MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "framerate", 0, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "dropped_frames", 0, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "total_frames", 0, MacroDeck.Variables.VariableType.Integer, this, false);
        }

        private void UpdateAllSourceItems()
        {
            foreach (var sceneItem in this.obs.GetCurrentScene().Items)
            {
                Obs_SceneItemVisibilityChanged(this.obs, this.obs.GetCurrentScene().Name, sceneItem.SourceName, sceneItem.Render); // Update source state; Render = visisble
            }
        }

        private void UpdateAllVariables()
        {
            UpdateAllSourceItems();
            foreach (var audioSource in this.obs.GetSpecialSources().Values)
            {
                Obs_SourceMuteStateChanged(this.obs, audioSource, this.obs.GetMute(audioSource)); // Update mute state
            }
            Obs_SceneListChanged(this, EventArgs.Empty); // Update the scene suggestions
            //MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", this.obs.GetCurrentTransition().Name, MacroDeck.Variables.VariableType.String, this, false); // TODO
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current_profile", this.obs.GetCurrentProfile(), MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current_scene", this.obs.GetCurrentScene().Name, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "connected", this.obs.IsConnected ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "replay_buffer", this.obs.GetReplayBufferStatus() ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual_camera", this.obs.GetVirtualCamStatus().IsActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", this.obs.GetRecordingStatus().IsRecording ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", this.obs.GetStreamingStatus().IsStreaming ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, false);
        }

        private void OnVirtualCameraStopped(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual_camera", "False", MacroDeck.Variables.VariableType.Bool, this);
        }

        private void OnVirtualCameraStarted(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual_camera", "True", MacroDeck.Variables.VariableType.Bool, this);
        }

        private void Obs_ReplayBufferStateChanged(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "replay_buffer", "True", MacroDeck.Variables.VariableType.Bool, this);
                    break;
                case OutputState.Stopped:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this);
                    break;
            }
        }


        private void OnRecordingStateChange(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "True", MacroDeck.Variables.VariableType.Bool, this);
                    break;
                case OutputState.Stopped:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this);
                    break;
            }
        }

        private void OnStreamingStateChange(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "True", MacroDeck.Variables.VariableType.Bool, this);
                    break;
                case OutputState.Stopped:
                    ResetVariables();
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this);
                    break;
            }
        }


        private void OnStreamData(OBSWebsocket sender, StreamStatus status)
        {
            TimeSpan streamTime = TimeSpan.FromSeconds(status.TotalStreamTime);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "stream_time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                                        streamTime.Hours,
                                                                                        streamTime.Minutes,
                                                                                        streamTime.Seconds), 
                                                                                        MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "kbits", status.KbitsPerSec.ToString(), MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "framerate", (int)status.FPS, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "dropped_frames", status.DroppedFrames, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "total_frames", status.TotalFrames, MacroDeck.Variables.VariableType.Integer, this, false);
        }

        private void OnTransitionChange(OBSWebsocket sender, string newTransitionName)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current_transition", newTransitionName, MacroDeck.Variables.VariableType.String, this, true);
        }

        private void OnProfileChange(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current_profile", this.obs.GetCurrentProfile(), MacroDeck.Variables.VariableType.String, this, true);
        }

        private void OnSceneChange(OBSWebsocket sender, string newSceneName)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current_scene", newSceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions, true);
            UpdateAllSourceItems();
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            ResetVariables();
           
            if (this.mainWindow != null && !this.mainWindow.IsDisposed && this.statusButton != null)
            {
                this.mainWindow.BeginInvoke(new Action(() =>
                {
                    this.statusButton.BackgroundImage = Properties.Resources.OBS_Offline;
                    this.statusToolTip.SetToolTip(this.statusButton, PluginLanguageManager.PluginStrings.OBSDisconnected);
                }));
            }
        }

        private void OnConnect(object sender, EventArgs e)
        {
            UpdateAllVariables();
            if (this.mainWindow != null && !this.mainWindow.IsDisposed && this.statusButton != null)
            {
                this.mainWindow.BeginInvoke(new Action(() =>
                {
                    this.statusButton.BackgroundImage = Properties.Resources.OBS_Online;
                    this.statusToolTip.SetToolTip(this.statusButton, PluginLanguageManager.PluginStrings.OBSConnected);
                }));
            }
        }

        public override void OpenConfigurator()
        {
            using (var pluginConfig = new PluginConfig())
            {
                pluginConfig.ShowDialog();
            }
        }

        internal void Connect(bool ignoreConnectionError = true)
        {
            if (!this.obs.IsConnected)
            {
                try
                {
                    List<Dictionary<string, string>> credentialsList = PluginCredentials.GetPluginCredentials(this);
                    Dictionary<string, string> credentials = null;
                    if (credentialsList != null && credentialsList.Count > 0)
                    {
                        credentials = credentialsList[0];
                    }
                    if (credentials != null)
                    {
                        this.obs.Connect(credentials["host"], credentials["password"]);
                    }
                }
                catch (AuthFailureException)
                {
                    using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                    {
                        msgBox.ShowDialog(PluginLanguageManager.PluginStrings.AuthenticationFailed, PluginLanguageManager.PluginStrings.InfoWrongPassword, System.Windows.Forms.MessageBoxButtons.OK);
                    }
                    return;
                }
                catch (ErrorResponseException)
                {
                    if (!ignoreConnectionError)
                    {
                        using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                        {
                            msgBox.ShowDialog(PluginLanguageManager.PluginStrings.ConnectionFailed, PluginLanguageManager.PluginStrings.InfoWrongHost, MessageBoxButtons.OK);
                        }
                    }
                }
            } else
            {
                this.obs.Disconnect();
            }
        }
    }
}
