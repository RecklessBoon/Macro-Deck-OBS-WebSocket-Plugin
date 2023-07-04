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
using SuchByte.OBSWebSocketPlugin.Controllers;
using ToolTip = System.Windows.Forms.ToolTip;
using SuchByte.OBSWebSocketPlugin.Models;
using System.Reflection.Metadata.Ecma335;

namespace SuchByte.OBSWebSocketPlugin
{

    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        public string Name = "OBS-WebSocket Plugin";

        private const string VariablePrefix = "obs_";

        private string[] sceneSuggestions = new string[] { "" };

        public override bool CanConfigure => true;

        public int NumConnected {
            get {
                return Connections.Where(c => c.Value.IsConnected).Select(p => p.Value).Count();
            }
        }
        public Dictionary<string, Connection> Connections = new();
        
        private ContentSelectorButton statusButton = new();

        private readonly ToolTip statusToolTip = new();

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
                    var active = Connections.Where(c => c.Value.IsConnected).Select(pair => pair.Value);
                    if (active.Any())
                    {
                        foreach(var connection in active)
                        {
                            connection.Dispose();
                        }
                    }
                    else
                    {
                        await SetupAndStartAsync();
                        active = Connections.Where(c => c.Value.IsConnected).Select(pair => pair.Value);
                        if (!active.Any())
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

            var numConnected = GetNumConnected();
            
            this.statusButton = new ContentSelectorButton
            {
                BackgroundImage = numConnected > 0 ? Properties.Resources.OBS_Online : Properties.Resources.OBS_Offline,
                BackgroundImageLayout = ImageLayout.Stretch,

            };
            statusToolTip.SetToolTip(statusButton, $"{numConnected} Connection(s) Active");
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
            _ = SetupAndStartAsync();
        }

        public async Task SetupAndStartAsync()
        {
            if (Connections != null)
            {
                foreach (var pair in Connections)
                {
                    pair.Value.Dispose();
                }
            }
            
            var credSet = PluginCredentials.GetPluginCredentials(this);
            var tasks = new List<Task>();
            foreach (var creds in credSet)
            {
                var config = ConnectionConfig.FromCredentials(creds);
                var connection = new Connection(config);
                Connections.Add(config.name, connection);
                tasks.Add(StoreConnectConnectionAsync(connection));
            }

            await Task.WhenAll(tasks);
        }

        public Task StoreConnectConnectionAsync(Connection connection)
        {
            if (Connections.ContainsKey(connection.Name))
            {
                Connections[connection.Name] = connection;
            }
            else
            {
                Connections.Add(connection.Name, connection);
            }
            ResetVariables(connection);
            WireObs(connection);
            return Task.WhenAny(connection.ConnectAsync(), Task.Delay(10000));
        }

        public int GetNumConnected()
        {
            return Connections?.Where(c => c.Value.IsConnected).Select(c => c.Value).Count() ?? 0;
        }

        public void WireObs(Connection conn)
        {
            var obs = conn.OBS;
            
            obs.Connected += (sender, args) => OnConnect(conn);
            obs.Disposed += (sender, args) => OnDisconnect(conn);

            obs.ScenesEvents.CurrentProgramSceneChanged += (sender, args) => OnSceneChange(args, conn);
            obs.ConfigEvents.CurrentProfileChanged += (sender, args) => OnProfileChange(args, conn);
            obs.TransitionsEvents.CurrentSceneTransitionChanged += (sender, args) => OnTransitionChange(args, conn);

            obs.OutputsEvents.StreamStateChanged += (sender, args) => OnStreamingStateChange(args, conn);
            obs.OutputsEvents.RecordStateChanged += (sender, args) => OnRecordingStateChange(args, conn);

            obs.OutputsEvents.VirtualcamStateChanged += (sender, args) => OnVirtualCameraStateChange(args, conn);

            obs.ScenesEvents.SceneListChanged += (sender, args) => OnSceneListChanged(args);

            obs.InputsEvents.InputVolumeChanged += (sender, args) => OnSourceVolumeChanged(args, conn);

            obs.Connected += (sender, args) => {
                _ = Task.Run(async () =>
                {
                    while (obs.IsConnected && !obs.IsDisposed)
                    {
                        var stream = await obs.StreamRequests.GetStreamStatusAsync();
                        OnStreamData(stream, conn);
                        
                        var stats = await obs.GeneralRequests.GetStatsAsync();
                        OnStatsData(stats, conn);

                        await Task.Delay(1000);
                    }
                });
            };

            obs.InputsEvents.InputMuteStateChanged += (sender, args) => OnSourceMuteStateChanged(args, conn);

            obs.SceneItemsEvents.SceneItemEnableStateChanged += (sender, args) => OnSceneItemVisibilityChanged(sender, args, conn);

            obs.OutputsEvents.ReplayBufferStateChanged += (sender, args) => OnReplayBufferStateChanged(args, conn);
        }

        private void OnSceneItemVisibilityChanged(object sender, OBSWebSocket5.Events.SceneItemsEvents.SceneItemEnableStateChangedEventArgs args, Connection connection)
        {
            var self = this;
            _ = Task.Run(async () =>
            {
                if (sender is OBSWebSocket obs)
                {
                    var sceneItems = await obs.SceneItemsRequests.GetSceneItemListAsync(args.SceneName);
                    var sceneItemName = args.SceneItemId.ToString();
                    foreach (var item in sceneItems.SceneItems)
                    {
                        if (item["sceneItemId"]!.ToString().Equals(args.SceneItemId.ToString()) && item["sourceName"] != null)
                        {
                            sceneItemName = item["sourceName"].ToString();
                            break;
                        }
                    }
                    MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + args.SceneName + "/" + sceneItemName, args.SceneItemEnabled ? "True" : "False", MacroDeck.Variables.VariableType.Bool, self, Array.Empty<string>());
                }
            });
        }

        private void OnSourceVolumeChanged(OBSWebSocket5.Events.InputsEvents.InputVolumeChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + args.InputName + " volume_db", args.InputVolumeDb, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
        }

        private void OnSourceMuteStateChanged(OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + args.InputName, args.InputMuted ? "False" : "True", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
        }

        private void OnSceneListChanged(OBSWebSocket5.Events.ScenesEvents.SceneListChangedEventArgs args)
        {
            List<string> scenesList = new();
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

        private void ResetVariables(Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "connected", "False",
                MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "recording", "False",
                MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
            ResetStreamVariables(connection);
        }

        private void ResetStreamVariables(Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "replay_buffer", "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "virtual_camera", "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "streaming", "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "stream_time", "0h:0m:0s", MacroDeck.Variables.VariableType.String, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "kbits", 0, MacroDeck.Variables.VariableType.String, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "framerate", 0, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "dropped_frames", 0, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "total_frames", 0, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
        }

        private void UpdateAllSourceItems()
        {
            _ = Task.Run(async () =>
            {
                foreach (var pair in Connections)
                {
                    var connection = pair.Value;
                    var currentSceneResponse = await connection.OBS.ScenesRequests.GetCurrentProgramSceneAsync();
                    var sceneItemsResponse =
                        await connection.OBS.SceneItemsRequests.GetSceneItemListAsync(currentSceneResponse
                            .CurrentProgramSceneName);

                    foreach (JObject sceneItem in sceneItemsResponse.SceneItems)
                    {
                        OnSceneItemVisibilityChanged(connection.OBS,
                            new OBSWebSocket5.Events.SceneItemsEvents.SceneItemEnableStateChangedEventArgs
                            {
                                SceneName = currentSceneResponse.CurrentProgramSceneName,
                                SceneItemId = sceneItem["sceneItemId"].ToObject<int>(),
                                SceneItemEnabled = sceneItem["sceneItemEnabled"].ToObject<bool>()
                            },
                            connection); // Update source state; Render = visisble
                    }
                }
            });
        }

        private void UpdateAllVariables(Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "connected",
                connection.OBS.IsConnected ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());

            _ = Task.Run(async () =>
            {
                var inputs = await connection.OBS.InputsRequests.GetInputListAsync();

                foreach (JObject input in inputs.Inputs)
                {
                    var inputName = input["inputName"]?.ToString();
                    var muted = await connection.OBS.InputsRequests.GetInputMuteAsync(inputName);
                    if (muted != null)
                    {
                        OnSourceMuteStateChanged(new OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs
                            { 
                                InputName = inputName, 
                                InputMuted = muted.InputMuted 
                            }, 
                            connection
                        ); // Update mute state
                    }
                }

                var scenes = await connection.OBS.ScenesRequests.GetSceneListAsync();
                OnSceneListChanged(new OBSWebSocket5.Events.ScenesEvents.SceneListChangedEventArgs { Scenes = scenes.Scenes }); // Update the scene suggestions
            });
            //MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", this.obs.GetCurrentTransition().Name, MacroDeck.Variables.VariableType.String, this, false); // TODO
            _ = Task.Run(async () =>
            {
                var profiles = await connection.OBS.ConfigRequests.GetProfileListAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "current_profile",
                    profiles.CurrentProfileName, MacroDeck.Variables.VariableType.String, this, Array.Empty<string>());
            });

            _ = Task.Run(async () =>
            {
                var scene = await connection.OBS.ScenesRequests.GetCurrentProgramSceneAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "current_scene",
                    scene.CurrentProgramSceneName, MacroDeck.Variables.VariableType.String, this,
                    this.sceneSuggestions);
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.OutputsRequests.GetReplayBufferStatusAsync();
                if (status != null)
                {
                    MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "replay_buffer",
                        status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this,
                        Array.Empty<string>());
                }
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.OutputsRequests.GetVirtualCamStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "virtual_camera",
                    status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this,
                    Array.Empty<string>());
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.RecordRequests.GetRecordStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "recording",
                    status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this,
                    Array.Empty<string>());
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.StreamRequests.GetStreamStatusAsync();
                MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "streaming",
                    status.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this,
                    Array.Empty<string>());
            });
        }

        private void OnVirtualCameraStateChange(OBSWebSocket5.Events.OutputsEvents.VirtualcamStateChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "virtual_camera", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
        }

        private void OnReplayBufferStateChanged(OBSWebSocket5.Events.OutputsEvents.ReplayBufferStateChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "replay_buffer", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
        }
        
        private void OnRecordingStateChange(OBSWebSocket5.Events.OutputsEvents.RecordStateChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "recording", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
        }

        private void OnStreamingStateChange(OBSWebSocket5.Events.OutputsEvents.StreamStateChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "streaming", args.OutputActive ? "True" : "False", MacroDeck.Variables.VariableType.Bool, this, Array.Empty<string>());
        }

        private void OnStreamData(OBSWebSocket5.Request.StreamRequests.GetStreamStatusResponse status, Connection connection)
        {
            TimeSpan streamTime = TimeSpan.FromSeconds(status.OutputDuration);
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "stream_time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                                         streamTime.Hours,
                                                                                         streamTime.Minutes,
                                                                                         streamTime.Seconds),
                                                                                         MacroDeck.Variables.VariableType.String, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "kbits", (status.OutputBytes * 1024).ToString(), MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "dropped_frames", status.OutputSkippedFrames, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "total_frames", status.OutputTotalFrames, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
        }

        private void OnStatsData(OBSWebSocket5.Request.GeneralRequests.GetStatsResponse stats, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "framerate", (int)stats.ActiveFps, MacroDeck.Variables.VariableType.Integer, this, Array.Empty<string>());
        }

        private void OnTransitionChange(OBSWebSocket5.Events.TransitionsEvents.CurrentSceneTransitionChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "current_transition", args.TransitionName, MacroDeck.Variables.VariableType.String, this, Array.Empty<string>());
        }

        private void OnProfileChange(OBSWebSocket5.Events.ConfigEvents.ProfileChangeEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "current_profile", args.ProfileName, MacroDeck.Variables.VariableType.String, this, Array.Empty<string>());
        }

        private void OnSceneChange(OBSWebSocket5.Events.ScenesEvents.CurrentProgramSceneChangedEventArgs args, Connection connection)
        {
            MacroDeck.Variables.VariableManager.SetValue(VariablePrefix + connection.VariableNS + "/" + "current_scene", args.SceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions);
            UpdateAllSourceItems();
        }

        private void OnDisconnect(Connection connection)
        {
            ResetVariables(connection);

            if (mainWindow != null && !mainWindow.IsDisposed && statusButton != null)
            {
                mainWindow.BeginInvoke(new Action(() =>
                {
                    statusButton.BackgroundImage = Properties.Resources.OBS_Offline;
                    statusToolTip.SetToolTip(statusButton, PluginLanguageManager.PluginStrings.OBSDisconnected);
                }));
            }
        }

        private void OnConnect(Connection connection)
        {
            UpdateAllVariables(connection);
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
            using var pluginConfig = new PluginConfig();
            pluginConfig.ShowDialog();
        }
    }
}
