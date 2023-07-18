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
using System.Drawing;
using SuchByte.MacroDeck.GUI.CustomControls.Notifications;
using SuchByte.OBSWebSocketPlugin.GUI.Controls;

namespace SuchByte.OBSWebSocketPlugin
{

    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        public string Name = "OBS-WebSocket Plugin";

        public const string VariablePrefix = "obs_";

        private string[] sceneSuggestions = new string[] { "" };

        public override bool CanConfigure => true;

        public int NumConnected {
            get {
                return Connections.Where(c => c.Value.IsConnected).Select(p => p.Value).Count();
            }
        }
        public Dictionary<string, Connection> Connections = new();
        
        private ObsSelectorButton statusButton = new();
        private ConnectionTogglerList togglerList = new();

        private readonly ToolTip statusToolTip = new();

        private MainWindow mainWindow;

        public Main()
        {
            PluginInstance.Main = this;
            MacroDeck.MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            mainWindow = sender as MainWindow;

            var numConnected = GetNumConnected();
            var buttonWidth = mainWindow.contentButtonPanel.ClientRectangle.Width;

            this.statusButton = new ObsSelectorButton
            {
                AlertText = numConnected.ToString(),
                BackgroundImage = numConnected > 0 ? Properties.Resources.OBS_Online : Properties.Resources.OBS_Offline,
                BackgroundImageLayout = ImageLayout.Zoom,
                Width = buttonWidth,
            };
            statusToolTip.SetToolTip(statusButton, $"{numConnected} Connection(s) Active");
            statusButton.Click += StatusButton_Click;
            mainWindow.contentButtonPanel.Controls.Add(statusButton);
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            if (togglerList?.Visible ?? false)
            {
                RemoveTogglerList();
            }
            else
            {
                ShowTogglerList();
            }
        }

        private void ShowTogglerList()
        {
            togglerList?.Close();
            togglerList = new ConnectionTogglerList(this.Connections.Values.ToList());
            togglerList.StartPosition = FormStartPosition.Manual;
            togglerList.Location = new Point(
                mainWindow.Location.X + mainWindow.contentButtonPanel.Location.X + mainWindow.contentButtonPanel.Width + 4,
                mainWindow.Location.Y + statusButton.Location.Y + statusButton.Height
            );
            togglerList.Deactivate += (object sender, EventArgs args) =>
            {
                RemoveTogglerList();
            };
            togglerList.Show(mainWindow);
        }

        private void RemoveTogglerList()
        {
            togglerList.Close();
            togglerList = null;
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
            Connections = new();
            
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
            togglerList?.AddConnection(connection);
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

        private static void OnSourceVolumeChanged(OBSWebSocket5.Events.InputsEvents.InputVolumeChangedEventArgs args, Connection connection)
        {
            connection.SetVariable(args.InputName + " volume_db", args.InputVolumeDb);
        }

        private static void OnSourceMuteStateChanged(OBSWebSocket5.Events.InputsEvents.InputMuteStateChangedEventArgs args, Connection connection)
        {
            connection.SetVariable(args.InputName, args.InputMuted ? "False" : "True");
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
            connection.SetVariable("connected", "False");
            connection.SetVariable("recording", "False");
            ResetStreamVariables(connection);
        }

        private static void ResetStreamVariables(Connection connection)
        {
            connection.SetVariable("replay_buffer", "False");
            connection.SetVariable("virtual_camera", "False");
            connection.SetVariable("streaming", "False");
            connection.SetVariable("stream_time", "0h:0m:0s");
            connection.SetVariable("kbits", 0);
            connection.SetVariable("framerate", 0);
            connection.SetVariable("dropped_frames", 0);
            connection.SetVariable("total_frames", 0);
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
           connection.SetVariable("connected", connection.OBS.IsConnected ? "True" : "False");

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
                connection.SetVariable("current_profile", profiles.CurrentProfileName);
            });

            _ = Task.Run(async () =>
            {
                var scene = await connection.OBS.ScenesRequests.GetCurrentProgramSceneAsync();
                connection.SetVariable("current_scene", scene.CurrentProgramSceneName, this.sceneSuggestions);
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.OutputsRequests.GetReplayBufferStatusAsync();
                if (status != null)
                {
                    connection.SetVariable("replay_buffer", status.OutputActive ? "True" : "False");
                }
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.OutputsRequests.GetVirtualCamStatusAsync();
                connection.SetVariable("virtual_camera", status.OutputActive ? "True" : "False");
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.RecordRequests.GetRecordStatusAsync();
                connection.SetVariable("recording", status.OutputActive ? "True" : "False");
            });

            _ = Task.Run(async () =>
            {
                var status = await connection.OBS.StreamRequests.GetStreamStatusAsync();
                connection.SetVariable("streaming", status.OutputActive ? "True" : "False");
            });
        }

        private static void OnVirtualCameraStateChange(OBSWebSocket5.Events.OutputsEvents.VirtualcamStateChangedEventArgs args, Connection connection)
        {
            connection.SetVariable("virtual_camera", args.OutputActive ? "True" : "False");
        }

        private static void OnReplayBufferStateChanged(OBSWebSocket5.Events.OutputsEvents.ReplayBufferStateChangedEventArgs args, Connection connection)
        {
            connection.SetVariable("replay_buffer", args.OutputActive ? "True" : "False");
        }
        
        private static void OnRecordingStateChange(OBSWebSocket5.Events.OutputsEvents.RecordStateChangedEventArgs args, Connection connection)
        {
            connection.SetVariable("recording", args.OutputActive ? "True" : "False");
        }

        private static void OnStreamingStateChange(OBSWebSocket5.Events.OutputsEvents.StreamStateChangedEventArgs args, Connection connection)
        {
            connection.SetVariable("streaming", args.OutputActive ? "True" : "False");
        }

        private static void OnStreamData(OBSWebSocket5.Request.StreamRequests.GetStreamStatusResponse status, Connection connection)
        {
            TimeSpan streamTime = TimeSpan.FromMilliseconds(status.OutputDuration);
            connection.SetVariable("stream_time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                streamTime.Hours,
                streamTime.Minutes,
                streamTime.Seconds)
            );
            connection.SetVariable("output_mb", (status.OutputBytes / (1024.0f * 1024.0f)).ToString());
            connection.SetVariable("dropped_frames", status.OutputSkippedFrames);
            connection.SetVariable("total_frames", status.OutputTotalFrames);
        }

        private static void OnStatsData(OBSWebSocket5.Request.GeneralRequests.GetStatsResponse stats, Connection connection)
        {
            connection.SetVariable("framerate", (int)stats.ActiveFps);
            connection.SetVariable("cpu_usage", stats.CpuUsage);
        }

        private static void OnTransitionChange(OBSWebSocket5.Events.TransitionsEvents.CurrentSceneTransitionChangedEventArgs args, Connection connection)
        {
            connection.SetVariable("current_transition", args.TransitionName);
        }

        private static void OnProfileChange(OBSWebSocket5.Events.ConfigEvents.ProfileChangeEventArgs args, Connection connection)
        {
            connection.SetVariable("current_profile", args.ProfileName);
        }

        private void OnSceneChange(OBSWebSocket5.Events.ScenesEvents.CurrentProgramSceneChangedEventArgs args, Connection connection)
        {
            connection.SetVariable("current_scene", args.SceneName, this.sceneSuggestions);
            UpdateAllSourceItems();
        }

        private void OnDisconnect(Connection connection)
        {
            ResetVariables(connection);

            var numConnected = GetNumConnected();
            if (mainWindow != null && !mainWindow.IsDisposed && statusButton != null)
            {
                mainWindow.BeginInvoke(new Action(() =>
                {
                    statusButton.BackgroundImage = numConnected > 0 ? Properties.Resources.OBS_Online : Properties.Resources.OBS_Offline;
                    statusToolTip.SetToolTip(statusButton, PluginLanguageManager.PluginStrings.OBSDisconnected);
                }));
            }
            this.statusButton.AlertText = numConnected.ToString();
        }

        private void OnConnect(Connection connection)
        {
            UpdateAllVariables(connection);
            var numConnected = GetNumConnected();
            if (mainWindow != null && !mainWindow.IsDisposed && statusButton != null)
            {
                mainWindow.BeginInvoke(new Action(() =>
                {
                    statusButton.BackgroundImage = Properties.Resources.OBS_Online;
                    statusToolTip.SetToolTip(statusButton, PluginLanguageManager.PluginStrings.OBSConnected);
                }));
            }
            this.statusButton.AlertText = numConnected.ToString();
        }

        public override void OpenConfigurator()
        {
            using var pluginConfig = new PluginConfig();
            pluginConfig.ShowDialog();
        }
    }
}
