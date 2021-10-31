using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Plugins;
using SuchByte.OBSWebSocketPlugin.Actions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SuchByte.OBSWebSocketPlugin {

    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        private string variablePrefix = "OBS ";

        private string[] toggleVariableSuggestions = new string[] { "On", "Off" };
        private string[] sceneSuggestions = new string[] { "" };

        public override string Description => "This plugin can control a SinusBot music bot.";
        public override bool CanConfigure => true;

        public OBSWebsocket OBS { get { return this.obs; } }

        protected OBSWebsocket obs;

        public Main()
        {
            PluginInstance.Main = this;
        }

        public override Image Icon => Properties.Resources.OBS_WebSocket;

        public override void Enable()
        {
            this.Actions = new System.Collections.Generic.List<PluginAction>()
            {
                new SetProfileAction(),
                new SetRecordingStateAction(),
                new SetSceneAction(),
                new SetStreamingStateAction(),
                new SetVirtualCamAction(),
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

            this.obs.StreamStatus += OnStreamData;

            this.Connect();
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
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "connected", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual camera", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "stream time", "0h:0m:0s", MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "Kbits", 0, MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "framerate", 0, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "dropped frames", 0, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "total frames", 0, MacroDeck.Variables.VariableType.Integer, this, false);
        }

        private void UpdateAllVariables()
        {
            Obs_SceneListChanged(this, EventArgs.Empty); // Update the scene suggestions
            //MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", this.obs.GetCurrentTransition().Name, MacroDeck.Variables.VariableType.String, this, false); // TODO
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current profile", this.obs.GetCurrentProfile(), MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current scene", this.obs.GetCurrentScene().Name, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "connected", this.obs.IsConnected ? "On" : "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual camera", this.obs.GetVirtualCamStatus().IsActive ? "On" : "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", this.obs.GetRecordingStatus().IsRecording ? "On" : "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", this.obs.GetStreamingStatus().IsStreaming ? "On" : "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions, false);
        }

        private void OnVirtualCameraStopped(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual camera", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
        }

        private void OnVirtualCameraStarted(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "virtual camera", "On", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
        }


        private void OnRecordingStateChange(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "On", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
                    break;
                case OutputState.Stopped:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "recording", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
                    break;
            }
        }

        private void OnStreamingStateChange(OBSWebsocket sender, OutputState newState)
        {
            switch (newState)
            {
                case OutputState.Started:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "On", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
                    break;
                case OutputState.Stopped:
                    ResetVariables();
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
                    break;
                default:
                    MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "streaming", "Off", MacroDeck.Variables.VariableType.String, this, this.toggleVariableSuggestions);
                    break;
            }
        }


        private void OnStreamData(OBSWebsocket sender, StreamStatus status)
        {
            TimeSpan streamTime = TimeSpan.FromSeconds(status.TotalStreamTime);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "stream time", string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                                        streamTime.Hours,
                                                                                        streamTime.Minutes,
                                                                                        streamTime.Seconds), 
                                                                                        MacroDeck.Variables.VariableType.String, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "Kbits", status.KbitsPerSec.ToString(), MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "framerate", (int)status.FPS, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "dropped frames", status.DroppedFrames, MacroDeck.Variables.VariableType.Integer, this, false);
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "total frames", status.TotalFrames, MacroDeck.Variables.VariableType.Integer, this, false);
        }

        private void OnTransitionChange(OBSWebsocket sender, string newTransitionName)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current transition", newTransitionName, MacroDeck.Variables.VariableType.String, this, true);
        }

        private void OnProfileChange(object sender, EventArgs e)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current profile", this.obs.GetCurrentProfile(), MacroDeck.Variables.VariableType.String, this, true);
        }

        private void OnSceneChange(OBSWebsocket sender, string newSceneName)
        {
            MacroDeck.Variables.VariableManager.SetValue(this.variablePrefix + "current scene", newSceneName, MacroDeck.Variables.VariableType.String, this, this.sceneSuggestions, true);
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            ResetVariables();
        }

        private void OnConnect(object sender, EventArgs e)
        {
            UpdateAllVariables();
        }

        public override void OpenConfigurator()
        {
            // TODO
        }

        internal void Connect()
        {
            if (!this.obs.IsConnected)
            {
                try
                {
                    this.obs.Connect("ws://127.0.0.1:4444", ""); // TODO
                }
                catch (AuthFailureException)
                {
                    using (var msgBox = new MessageBox())
                    {
                        msgBox.ShowDialog("OBS Authentication failed", "Please make sure, you set the correct password", System.Windows.Forms.MessageBoxButtons.OK);
                    }
                    return;
                } catch { }
            } else
            {
                this.obs.Disconnect();
            }
        }
    }
}
