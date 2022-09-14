using Newtonsoft.Json.Linq;
using OBSWebSocket5;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Actions;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OBSWebSocket4Client = OBSWebsocketDotNet.OBSWebsocket;
using OBSWebSocket5Client = OBSWebSocket5.OBSWebSocket;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SuchByte.OBSWebSocketPlugin
{

    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public enum OBSWebSocketVersionType
    {
        OBS_WEBSOCKET_AUTO,
        OBS_WEBSOCKET_V4,
        OBS_WEBSOCKET_V5
    }

    public class Main : MacroDeckPlugin
    {
        private string variablePrefix = "obs_";

        private string[] sceneSuggestions = new string[] { "" };

        public override string Description => "Control Open Broadcaster Software with Macro Deck.";
        public override bool CanConfigure => true;

        public OBSWebSocket4Client OBS { get { return this.obs4; } }
        public OBSWebSocket4Client OBS4 { get { return this.obs4; } }

        protected OBSWebSocket4Client obs4;

        public OBSWebSocket5Client OBS5 { get { return this.obs5; } }
        protected OBSWebSocket5Client obs5;

        public bool IsConnected => (OBS4?.IsConnected ?? false) || (OBS5?.IsConnected ?? false);

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
                OpenConfigurator();
            }
            else
            {
                _ = Task.Run(async () =>
                {
                    if (IsConnected)
                    {
                        Disconnect();
                    }
                    else
                    {
                        await SetupAndStartAsync();
                        if (!IsConnected)
                        {
                            OpenConfigurator();
                        }
                    }
                });
            }
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            mainWindow = sender as MainWindow;

            this.statusButton = new ContentSelectorButton
            {
                BackgroundImage = IsConnected ? Properties.Resources.OBS_Online : Properties.Resources.OBS_Offline,
                BackgroundImageLayout = ImageLayout.Stretch,

            };
            statusToolTip.SetToolTip(statusButton, "OBS " + (IsConnected ? " Connected" : "Disconnected"));
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
                new SetFilterStateAction(),
                new SetTextValueAction(),
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
            _ = SetupAndStartAsync();
        }

        public async Task SetupAndStartAsync()
        {
            var versionType = OBSWebSocketVersionType.OBS_WEBSOCKET_AUTO;
            Enum.TryParse<OBSWebSocketVersionType>(PluginConfiguration.GetValue(this, "versionType"), out versionType);

            switch (versionType)
            {
                case OBSWebSocketVersionType.OBS_WEBSOCKET_V4:
                    WireOBS4();
                    break;
                case OBSWebSocketVersionType.OBS_WEBSOCKET_V5:
                    WireOBS5();
                    break;
                case OBSWebSocketVersionType.OBS_WEBSOCKET_AUTO:
                default:
                    await AutoWireAsync();
                    break;
            };

            await ConnectAsync();

            if (!IsConnected)
            {
                obs4 = null;
                obs5 = null;
            }
        }

        protected async Task AutoWireAsync()
        {
            var host = GetHost();
            var address = host != null ? new Uri(host) : null;

            var ct = new CancellationToken();
            var timeoutFound = int.TryParse(PluginConfiguration.GetValue(this, "timeout"), out int timeout);
            timeout = timeoutFound ? timeout : PluginConfig.DEFAULT_TIMEOUT;
            var obs5Running = address != null && await OBSWebSocket5Client.IsInstalledAndOpenAsync(address, ct, timeout*1000);
            if (obs5Running)
            {
                WireOBS5();
            } else { 
                WireOBS4();
            }
        }

        internal string GetHost(int version = 5)
        {
            var creds = PluginCredentials.GetPluginCredentials(this).FirstOrDefault();
            return creds?["host"]?.ToString() ?? "ws://127.0.0.1:4455";
        }

        protected void WireOBS4()
        {
            obs4 = new OBSWebSocket4Client();
            obs4.Connected += OnConnect;
            obs4.Disconnected += OnDisconnect;

            obs4.SceneChanged += OnSceneChange;
            obs4.ProfileChanged += OnProfileChange;
            // this.obs.TransitionChanged += OnTransitionChange; // TODO

            obs4.StreamingStateChanged += OnStreamingStateChange;
            obs4.RecordingStateChanged += OnRecordingStateChange;

            obs4.VirtualCameraStarted += OnVirtualCameraStarted;
            obs4.VirtualCameraStopped += OnVirtualCameraStopped;

            obs4.SceneListChanged += Obs_SceneListChanged;

            obs4.SourceVolumeChanged += Obs_SourceVolumeChanged;

            obs4.StreamStatus += OnStreamData;

            obs4.SourceMuteStateChanged += Obs_SourceMuteStateChanged;

            obs4.SceneItemVisibilityChanged += Obs_SceneItemVisibilityChanged;

            obs4.ReplayBufferStateChanged += Obs_ReplayBufferStateChanged;
        }

        protected void ClipOBS4()
        {
            obs4.Disconnect();
            obs4 = null;
        }

        protected void WireOBS5()
        {
            obs5 = new OBSWebSocket5Client();

            obs5.Connected += OnConnect;
            obs5.Disposed += OnDisconnect;

            obs5.ScenesEvents.CurrentProgramSceneChanged += OnSceneChange5;
            obs5.ConfigEvents.CurrentProfileChanged += OnProfileChange5;
            obs5.TransitionsEvents.CurrentSceneTransitionChanged += OnTransitionChange5;

            obs5.OutputsEvents.StreamStateChanged += OnStreamingStateChange5;
            obs5.OutputsEvents.RecordStateChanged += OnRecordingStateChange5;

            obs5.OutputsEvents.VirtualcamStateChanged += OnVirtualCameraStateChange5;

            obs5.ScenesEvents.SceneListChanged += Obs_SceneListChanged5;

            obs5.InputsEvents.InputVolumeChanged += Obs_SourceVolumeChanged5;

            while (obs5 != null && obs5.IsConnected && !obs5.IsDisposed)
            {
                _ = Task.Run(async () =>
                {
                    var results = await obs5.StreamRequests.GetStreamStatusAsync();
                    OnStreamData5(results);
                });
                _ = Task.Run(async () =>
                {
                    var results = await obs5.GeneralRequests.GetStatsAsync();
                    OnStatsData5(results);
                });
            }

            obs5.InputsEvents.InputMuteStateChanged += Obs_SourceMuteStateChanged5;

            obs5.SceneItemsEvents.SceneItemEnableStateChanged += Obs_SceneItemVisibilityChanged5;

            obs5.OutputsEvents.ReplayBufferStateChanged += Obs_ReplayBufferStateChanged5;
        }

        protected void ClipOBS5()
        {
            obs5?.Dispose();
            obs5 = null;
        }

        private void Obs_SceneItemVisibilityChanged(OBSWebSocket4Client sender, string sceneName, string itemName, bool isVisible)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + sceneName + "/" + itemName, isVisible ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void Obs_SceneItemVisibilityChanged5(object sender, OBSWebSocket5.Events.SceneItemsEvents.SceneItemEnableStateChangedEventArgs args)
        {
            var self = this;
            _ = Task.Run(async () =>
            {
                var sceneItems = await obs5.SceneItemsRequests.GetSceneItemListAsync(args.SceneName);
                string sceneItemName = args.SceneItemId.ToString();
                foreach(JObject item in sceneItems.SceneItems)
                {
                    if (item["sceneItemId"].Equals(args.SceneItemId) && item["sourceName"] != null)
                    {
                        sceneItemName = item["sourceName"].ToString();
                        break;
                    }
                }
                MacroDeck.Variables.VariableManager.SetValue(variablePrefix + args.SceneName + "/" + sceneItemName, args.SceneItemEnabled ? "True" : "False", MacroDeck.Variables.VariableType.Bool, self, new string[0]);
            });
        }

        private void Obs_SourceVolumeChanged(OBSWebSocket4Client sender, string sourceName, float volume)
        {
            volume = PluginInstance.Main.OBS4.GetVolume(sourceName, true).Volume; // get volume in dB
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + sourceName + " volume_db", (int)volume, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void Obs_SourceVolumeChanged5(object sender, OBSWebSocket5.Events.InputsEvents.InputVolumeChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + args.InputName + " volume_db", args.InputVolumeDb, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void Obs_SourceMuteStateChanged(OBSWebSocket4Client sender, string sourceName, bool muted)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + sourceName, muted ? "False" : "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void Obs_SourceMuteStateChanged5(object sender, OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + args.InputName, args.InputMuted ? "False" : "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void Obs_SceneListChanged(object sender, EventArgs e)
        {
            List<string> scenesList = new List<string>();
            foreach (OBSScene scene in obs4.ListScenes())
            {
                scenesList.Add(scene.Name);
            }
            sceneSuggestions = scenesList.ToArray();
        }

        private void Obs_SceneListChanged5(object sender, OBSWebSocket5.Events.ScenesEvents.SceneListChangedEventArgs args)
        {
            List<string> scenesList = new List<string>();
            foreach (object scene in args.Scenes)
            {
                try
                {
                    var name = scene.GetType().GetProperty("Name");
                    if (name == null) continue;
                    scenesList.Add(name.ToString());
                }
                catch (Exception) { continue; }
            }
            sceneSuggestions = scenesList.ToArray();
        }

        private void ResetVariables()
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "connected", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "virtual_camera", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "stream_time", "0h:0m:0s", MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "kbits", 0, MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "framerate", 0, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "dropped_frames", 0, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "total_frames", 0, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void UpdateAllSourceItems()
        {
            if (obs4 != null) UpdateAllSourceItemsOBS4();
            else if (obs5 != null) UpdateAllSourceItemsOBS5();
        }

        private void UpdateAllSourceItemsOBS4()
        {
            foreach (var sceneItem in this.obs4.GetCurrentScene().Items)
            {
                Obs_SceneItemVisibilityChanged(this.obs4, this.obs4.GetCurrentScene().Name, sceneItem.SourceName, sceneItem.Render); // Update source state; Render = visisble
            }
        }

        private void UpdateAllSourceItemsOBS5()
        {
            _ = Task.Run(async () =>
            {
                var currentSceneResponse = await obs5.ScenesRequests.GetCurrentProgramSceneAsync();
                var sceneItemsResponse = await obs5.SceneItemsRequests.GetSceneItemListAsync(currentSceneResponse.CurrentProgramSceneName);

                foreach (JObject sceneItem in sceneItemsResponse.SceneItems)
                {
                    Obs_SceneItemVisibilityChanged5(obs5, new OBSWebSocket5.Events.SceneItemsEvents.SceneItemEnableStateChangedEventArgs { SceneName = currentSceneResponse.CurrentProgramSceneName, SceneItemId = sceneItem["sceneItemId"].ToObject<int>(), SceneItemEnabled = sceneItem["sceneItemEnabled"].ToObject<bool>() }); // Update source state; Render = visisble
                }
            });
        }

        private void UpdateAllVariables()
        {
            UpdateAllSourceItems();
            if (obs4 != null) UpdateAllVariablesOBS4();
            else if (obs5 != null) UpdateAllVariablesOBS5();
        }

        private void UpdateAllVariablesOBS4()
        {
            foreach (var audioSource in obs4.GetSpecialSources().Values)
            {
                Obs_SourceMuteStateChanged(obs4, audioSource, obs4.GetMute(audioSource)); // Update mute state
            }
            Obs_SceneListChanged(this, EventArgs.Empty); // Update the scene suggestions
            //MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", this.obs.GetCurrentTransition().Name, MacroDeck.Variables.VariableType.String, this, false); // TODO
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_profile", obs4.GetCurrentProfile(), MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_scene", obs4.GetCurrentScene().Name, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "connected", obs4.IsConnected ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", obs4.GetReplayBufferStatus() ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "virtual_camera", obs4.GetVirtualCamStatus().IsActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", obs4.GetRecordingStatus().IsRecording ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", obs4.GetStreamingStatus().IsStreaming ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void UpdateAllVariablesOBS5()
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "connected", obs5.IsConnected ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);

            _ = Task.Run(async () =>
            {
                var inputs = await obs5.InputsRequests.GetInputListAsync();
                
                foreach (JObject input in inputs.Inputs)
                {
                    var inputName = input["inputName"]?.ToString();
                    var muted = await obs5.InputsRequests.GetInputMuteAsync(inputName);
                    if (muted != null)
                    {
                        Obs_SourceMuteStateChanged5(obs5, new OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs { InputName = inputName, InputMuted = muted.InputMuted }); // Update mute state
                    }
                }
                var scenes = await obs5.ScenesRequests.GetSceneListAsync();
                Obs_SceneListChanged5(this, new OBSWebSocket5.Events.ScenesEvents.SceneListChangedEventArgs { Scenes = scenes.Scenes }); // Update the scene suggestions
            });
            //MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", this.obs.GetCurrentTransition().Name, MacroDeck.Variables.VariableType.String, this, false); // TODO
            _ = Task.Run(async () =>
            {
                var profiles = await obs5.ConfigRequests.GetProfileListAsync();
                MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_profile", profiles.CurrentProfileName, MacroDeck.Variables.VariableType.String, this, new string[0]);
            });

            _ = Task.Run(async () =>
            {
                var scene = await obs5.ScenesRequests.GetCurrentProgramSceneAsync();
                MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_scene", scene.CurrentProgramSceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
            });

            _ = Task.Run(async () =>
            {
                var status = await obs5.OutputsRequests.GetReplayBufferStatusAsync();
                if (status != null)
                {
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                }
            });

            _ = Task.Run(async () =>
            {
                var status = await obs5.OutputsRequests.GetVirtualCamStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "virtual_camera", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            });

            _ = Task.Run(async () =>
            {
                var status = await obs5.RecordRequests.GetRecordStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            });

            _ = Task.Run(async () =>
            {
                var status = await obs5.StreamRequests.GetStreamStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            });
        }

        private void OnVirtualCameraStopped(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "virtual_camera", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnVirtualCameraStarted(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "virtual_camera", "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnVirtualCameraStateChange5(object sender, OBSWebSocket5.Events.OutputsEvents.VirtualcamStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "virtual_camera", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void Obs_ReplayBufferStateChanged(OBSWebSocket4Client sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
                case OutputState.Stopped:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
            }
        }

        private void Obs_ReplayBufferStateChanged5(object sender, OBSWebSocket5.Events.OutputsEvents.ReplayBufferStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "replay_buffer", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }


        private void OnRecordingStateChange(OBSWebSocket4Client sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
                case OutputState.Stopped:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
            }
        }

        private void OnRecordingStateChange5(object sender, OBSWebSocket5.Events.OutputsEvents.RecordStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "recording", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnStreamingStateChange(OBSWebSocket4Client sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
                case OutputState.Stopped:
                    ResetVariables();
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                    break;
            }
        }

        private void OnStreamingStateChange5(object sender, OBSWebSocket5.Events.OutputsEvents.StreamStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "streaming", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }


        private void OnStreamData(OBSWebSocket4Client sender, StreamStatus status)
        {
            TimeSpan streamTime = TimeSpan.FromSeconds(status.TotalStreamTime);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "stream_time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                                         streamTime.Hours,
                                                                                         streamTime.Minutes,
                                                                                         streamTime.Seconds),
                                                                                         MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "kbits", status.KbitsPerSec.ToString(), MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "framerate", (int)status.FPS, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "dropped_frames", status.DroppedFrames, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "total_frames", status.TotalFrames, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void OnStreamData5(OBSWebSocket5.Request.StreamRequests.GetStreamStatusResponse status)
        {
            TimeSpan streamTime = TimeSpan.FromSeconds(status.OutputDuration);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "stream_time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                                         streamTime.Hours,
                                                                                         streamTime.Minutes,
                                                                                         streamTime.Seconds),
                                                                                         MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "kbits", (status.OutputBytes * 1024).ToString(), MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "dropped_frames", status.OutputSkippedFrames, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "total_frames", status.OutputTotalFrames, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void OnStatsData5(OBSWebSocket5.Request.GeneralRequests.GetStatsResponse stats)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "framerate", (int)stats.ActiveFps, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void OnTransitionChange(OBSWebSocket4Client sender, string newTransitionName)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_transition", newTransitionName, MacroDeck.Variables.VariableType.String, this, new string[0]);
        }

        private void OnTransitionChange5(object sender, OBSWebSocket5.Events.TransitionsEvents.CurrentSceneTransitionChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_transition", args.TransitionName, MacroDeck.Variables.VariableType.String, this, new string[0]);
        }

        private void OnProfileChange(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_profile", this.obs4.GetCurrentProfile(), MacroDeck.Variables.VariableType.String, this, new string[0]);
        }

        private void OnProfileChange5(object sender, OBSWebSocket5.Events.ConfigEvents.ProfileChangeEventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_profile", e.ProfileName, MacroDeck.Variables.VariableType.String, this, new string[0]);
        }

        private void OnSceneChange(OBSWebSocket4Client sender, string newSceneName)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_scene", newSceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
            UpdateAllSourceItems();
        }

        private void OnSceneChange5(object sender, OBSWebSocket5.Events.ScenesEvents.CurrentProgramSceneChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(variablePrefix + "current_scene", args.SceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
            UpdateAllSourceItems();
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            ResetVariables();

            if (mainWindow != null && !mainWindow.IsDisposed && statusButton != null)
            {
                mainWindow.BeginInvoke(new Action(() =>
                {
                    statusButton.BackgroundImage = Properties.Resources.OBS_Offline;
                    statusToolTip.SetToolTip(statusButton, PluginLanguageManager.PluginStrings.OBSDisconnected);
                }));
            }
        }

        private void OnConnect(object sender, EventArgs e)
        {
            UpdateAllVariables();
            if (mainWindow != null && !mainWindow.IsDisposed && statusButton != null)
            {
                mainWindow.BeginInvoke(new Action(() =>
                {
                    statusButton.BackgroundImage = Properties.Resources.OBS_Online;
                    statusToolTip.SetToolTip(statusButton, PluginLanguageManager.PluginStrings.OBSConnected);
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

        internal Task ConnectAsync(bool ignoreConnectionError = true)
        {
            if (obs4 != null)
            {
                ConnectOBS4(ignoreConnectionError);
                return Task.CompletedTask;
            }
            else if (obs5 != null)
            {
                return ConnectOBS5Async(ignoreConnectionError);
            }

            return Task.CompletedTask;
        }

        internal void ConnectOBS4(bool ignoreConnectionError = true)
        {
            if (!obs4.IsConnected)
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
                        obs4.Connect(credentials["host"], credentials["password"]);
                    }
                }
                catch (OBSWebsocketDotNet.AuthFailureException)
                {
                    using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                    {
                        msgBox.ShowDialog(PluginLanguageManager.PluginStrings.AuthenticationFailed, PluginLanguageManager.PluginStrings.InfoWrongPassword, System.Windows.Forms.MessageBoxButtons.OK);
                    }
                }
                catch (OBSWebsocketDotNet.ErrorResponseException)
                {
                    if (!ignoreConnectionError)
                    {
                        using (var msgBox = new MacroDeck.GUI.CustomControls.MessageBox())
                        {
                            msgBox.ShowDialog(PluginLanguageManager.PluginStrings.ConnectionFailed, PluginLanguageManager.PluginStrings.InfoWrongHost, MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        internal Task ConnectOBS5Async(bool ignoreConnectionError = true)
        {
            if (obs5 == null) WireOBS5();

            if (!obs5.IsConnected)
            {
                try
                {
                    var host = GetHost();
                    var address = host != null ? new Uri(host) : null;
                    if (address == null) return Task.CompletedTask;

                    var creds = PluginCredentials.GetPluginCredentials(this).FirstOrDefault();
                    var password = creds?["password"]?.ToString();

                    if (password == null)
                    {
                        return obs5.ConnectAsync(address);
                    } else
                    {
                        return obs5.ConnectAsync(address, new OBSWebSocketAuthPassword(password));
                    }
                }
                catch (Exception) { ClipOBS5(); }
            }

            return Task.CompletedTask;
        }

        internal void Disconnect()
        {
            DisconnectOBS4();
            DisconnectOBS5();
            OnDisconnect(null, EventArgs.Empty);
        }

        internal void DisconnectOBS4()
        {
            if (obs4?.IsConnected ?? false)
            {
                obs4?.Disconnect();
            }
            obs4 = null;
        }

        internal void DisconnectOBS5()
        {
            if (!obs5?.IsDisposed ?? false)
            {
                obs5?.Dispose();
            }
            obs5 = null;
        }
    }
}
