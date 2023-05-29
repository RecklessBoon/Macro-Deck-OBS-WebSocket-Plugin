using Newtonsoft.Json.Linq;
using OBSWebSocket5;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Actions;
using SuchByte.OBSWebSocketPlugin.GUI;
using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SuchByte.OBSWebSocketPlugin
{

    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        private const string VariablePrefix = "obs_";

        private string[] sceneSuggestions = new string[] { "" };

        public override bool CanConfigure => true;
        
        public OBSWebSocket Obs => this.OBS;
        protected OBSWebSocket OBS;

        public bool IsConnected => Obs?.IsConnected ?? false;

        private ContentSelectorButton statusButton = new ContentSelectorButton();

        private readonly ToolTip statusToolTip = new ToolTip();

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
            mainWindow?.contentButtonPanel.Controls.Add(statusButton);
        }
        
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
            WireObs();

            await ConnectAsync();

            if (!IsConnected)
            {
                OBS = null;
            }
        }
        
        internal string GetHost(int version = 5)
        {
            var creds = PluginCredentials.GetPluginCredentials(this).FirstOrDefault();
            return creds?["host"]?.ToString() ?? "ws://127.0.0.1:4455";
        }

        protected void WireObs()
        {
            OBS = new OBSWebSocket();

            OBS.Connected += OnConnect;
            OBS.Disposed += OnDisconnect;

            OBS.ScenesEvents.CurrentProgramSceneChanged += OnSceneChange;
            OBS.ConfigEvents.CurrentProfileChanged += OnProfileChange;
            OBS.TransitionsEvents.CurrentSceneTransitionChanged += OnTransitionChange;

            OBS.OutputsEvents.StreamStateChanged += OnStreamingStateChange;
            OBS.OutputsEvents.RecordStateChanged += OnRecordingStateChange;

            OBS.OutputsEvents.VirtualcamStateChanged += OnVirtualCameraStateChange;

            OBS.ScenesEvents.SceneListChanged += OnSceneListChanged;

            OBS.InputsEvents.InputVolumeChanged += OnSourceVolumeChanged;

            while (OBS != null && OBS.IsConnected && !OBS.IsDisposed)
            {
                _ = Task.Run(async () =>
                {
                    var results = await OBS.StreamRequests.GetStreamStatusAsync();
                    OnStreamData(results);
                });
                _ = Task.Run(async () =>
                {
                    var results = await OBS.GeneralRequests.GetStatsAsync();
                    OnStatsData(results);
                });
            }

            OBS.InputsEvents.InputMuteStateChanged += OnSourceMuteStateChanged;

            OBS.SceneItemsEvents.SceneItemEnableStateChanged += OnSceneItemVisibilityChanged;

            OBS.OutputsEvents.ReplayBufferStateChanged += OnReplayBufferStateChanged;
        }

        protected void ClipObs()
        {
            OBS?.Dispose();
            OBS = null;
        }

        private void OnSceneItemVisibilityChanged(object sender, OBSWebSocket5.Events.SceneItemsEvents.SceneItemEnableStateChangedEventArgs args)
        {
            var self = this;
            _ = Task.Run(async () =>
            {
                var sceneItems = await OBS.SceneItemsRequests.GetSceneItemListAsync(args.SceneName);
                string sceneItemName = args.SceneItemId.ToString();
                foreach(JObject item in sceneItems.SceneItems)
                {
                    if (item["sceneItemId"].ToString().Equals(args.SceneItemId.ToString()) && item["sourceName"] != null)
                    {
                        sceneItemName = item["sourceName"].ToString();
                        break;
                    }
                }
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + args.SceneName + "/" + sceneItemName, args.SceneItemEnabled ? "True" : "False", MacroDeck.Variables.VariableType.Bool, self, new string[0]);
            });
        }

        private void OnSourceVolumeChanged(object sender, OBSWebSocket5.Events.InputsEvents.InputVolumeChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + args.InputName + " volume_db", args.InputVolumeDb, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void OnSourceMuteStateChanged(object sender, OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + args.InputName, args.InputMuted ? "False" : "True", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnSceneListChanged(object sender, OBSWebSocket5.Events.ScenesEvents.SceneListChangedEventArgs args)
        {
            List<string> scenesList = new List<string>();
            foreach (JObject scene in args.Scenes)
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
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "connected", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "recording", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            ResetStreamVariables();
        }

        private void ResetStreamVariables()
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "virtual_camera", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "stream_time", "0h:0m:0s", MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "kbits", 0, MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "framerate", 0, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "dropped_frames", 0, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "total_frames", 0, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void UpdateAllSourceItems()
        {
            _ = Task.Run(async () =>
            {
                var currentSceneResponse = await OBS.ScenesRequests.GetCurrentProgramSceneAsync();
                var sceneItemsResponse = await OBS.SceneItemsRequests.GetSceneItemListAsync(currentSceneResponse.CurrentProgramSceneName);

                foreach (JObject sceneItem in sceneItemsResponse.SceneItems)
                {
                    OnSceneItemVisibilityChanged(OBS, new OBSWebSocket5.Events.SceneItemsEvents.SceneItemEnableStateChangedEventArgs { SceneName = currentSceneResponse.CurrentProgramSceneName, SceneItemId = sceneItem["sceneItemId"].ToObject<int>(), SceneItemEnabled = sceneItem["sceneItemEnabled"].ToObject<bool>() }); // Update source state; Render = visisble
                }
            });
        }

        private void UpdateAllVariables()
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "connected", OBS.IsConnected ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);

            _ = Task.Run(async () =>
            {
                var inputs = await OBS.InputsRequests.GetInputListAsync();
                
                foreach (JObject input in inputs.Inputs)
                {
                    var inputName = input["inputName"]?.ToString();
                    var muted = await OBS.InputsRequests.GetInputMuteAsync(inputName);
                    if (muted != null)
                    {
                        OnSourceMuteStateChanged(OBS, new OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs { InputName = inputName, InputMuted = muted.InputMuted }); // Update mute state
                    }
                }
                var scenes = await OBS.ScenesRequests.GetSceneListAsync();
                OnSceneListChanged(this, new OBSWebSocket5.Events.ScenesEvents.SceneListChangedEventArgs { Scenes = scenes.Scenes }); // Update the scene suggestions
            });
            //MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", this.obs.GetCurrentTransition().Name, MacroDeck.Variables.VariableType.String, this, false); // TODO
            _ = Task.Run(async () =>
            {
                var profiles = await OBS.ConfigRequests.GetProfileListAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "current_profile", profiles.CurrentProfileName, MacroDeck.Variables.VariableType.String, this, new string[0]);
            });

            _ = Task.Run(async () =>
            {
                var scene = await OBS.ScenesRequests.GetCurrentProgramSceneAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "current_scene", scene.CurrentProgramSceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
            });

            _ = Task.Run(async () =>
            {
                var status = await OBS.OutputsRequests.GetReplayBufferStatusAsync();
                if (status != null)
                {
                    MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "replay_buffer", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
                }
            });

            _ = Task.Run(async () =>
            {
                var status = await OBS.OutputsRequests.GetVirtualCamStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "virtual_camera", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            });

            _ = Task.Run(async () =>
            {
                var status = await OBS.RecordRequests.GetRecordStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "recording", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            });

            _ = Task.Run(async () =>
            {
                var status = await OBS.StreamRequests.GetStreamStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "streaming", status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
            });
        }

        private void OnVirtualCameraStateChange(object sender, OBSWebSocket5.Events.OutputsEvents.VirtualcamStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "virtual_camera", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnReplayBufferStateChanged(object sender, OBSWebSocket5.Events.OutputsEvents.ReplayBufferStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "replay_buffer", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }
        
        private void OnRecordingStateChange(object sender, OBSWebSocket5.Events.OutputsEvents.RecordStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "recording", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnStreamingStateChange(object sender, OBSWebSocket5.Events.OutputsEvents.StreamStateChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "streaming", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, new string[0]);
        }

        private void OnStreamData(OBSWebSocket5.Request.StreamRequests.GetStreamStatusResponse status)
        {
            TimeSpan streamTime = TimeSpan.FromSeconds(status.OutputDuration);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "stream_time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                                         streamTime.Hours,
                                                                                         streamTime.Minutes,
                                                                                         streamTime.Seconds),
                                                                                         MacroDeck.Variables.VariableType.String, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "kbits", (status.OutputBytes * 1024).ToString(), MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "dropped_frames", status.OutputSkippedFrames, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "total_frames", status.OutputTotalFrames, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void OnStatsData(OBSWebSocket5.Request.GeneralRequests.GetStatsResponse stats)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "framerate", (int)stats.ActiveFps, MacroDeck.Variables.VariableType.Integer, this, new string[0]);
        }

        private void OnTransitionChange(object sender, OBSWebSocket5.Events.TransitionsEvents.CurrentSceneTransitionChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "current_transition", args.TransitionName, MacroDeck.Variables.VariableType.String, this, new string[0]);
        }

        private void OnProfileChange(object sender, OBSWebSocket5.Events.ConfigEvents.ProfileChangeEventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "current_profile", e.ProfileName, MacroDeck.Variables.VariableType.String, this, new string[0]);
        }

        private void OnSceneChange(object sender, OBSWebSocket5.Events.ScenesEvents.CurrentProgramSceneChangedEventArgs args)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + "current_scene", args.SceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
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
            if (OBS == null) WireObs();

            if (!OBS.IsConnected)
            {
                try
                {
                    var host = GetHost();
                    var address = host != null ? new Uri(host) : null;
                    if (address == null) return Task.CompletedTask;

                    var creds = PluginCredentials.GetPluginCredentials(this).FirstOrDefault();
                    var password = creds?["password"]?.ToString();

                    if (String.IsNullOrEmpty(password))
                    {
                        return OBS.ConnectAsync(address);
                    } else
                    {
                        return OBS.ConnectAsync(address, new OBSWebSocketAuthPassword(password));
                    }
                }
                catch (Exception) { ClipObs(); }
            }

            return Task.CompletedTask;
        }

        internal void Disconnect()
        {
            if (!OBS?.IsDisposed ?? false)
            {
                OBS?.Dispose();
            }
            OBS = null;
            OnDisconnect(null, EventArgs.Empty);
        }
    }
}
