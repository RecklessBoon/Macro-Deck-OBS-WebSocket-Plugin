using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OBSWebSocket5;
using SuchByte.MacroDeck.Logging;
using SuchByte.OBSWebSocketPlugin.Models;

namespace SuchByte.OBSWebSocketPlugin.Controllers
{
    public partial class Connection : IDisposable
    {
        [GeneratedRegex("/^[0-9a-zA-Z]/")]
        private static partial Regex ReplaceNonAlphaNumeric();

        public ConnectionConfig Config { get; }
        public string Name
        {
            get
            {
                return Config.name;
            }
        }
        public string VariableNS => ReplaceNonAlphaNumeric().Replace(Name, "_");

        public Uri Host { 
            get
            {
                return new Uri(Config.host);
            }
        }

        public OBSWebSocket OBS { get; private set; }

        public bool IsConnected => OBS != null && OBS.IsConnected && !OBS.IsDisposed;

        public bool IsDisposed { get; private set; } = false;

        public event EventHandler<EventArgs> Disposed;

        public Connection(ConnectionConfig config)
        {
            Config = config;   
            OBS = new OBSWebSocket();
        }

        public static Connection FromPrev(Connection prev)
        {
            return new Connection(prev.Config);
        }
        
        public Task ConnectAsync()
        {
            if (IsConnected) return Task.CompletedTask;

            try
            {
                if (Config.host == null) return Task.CompletedTask;

                var host = new Uri(Config.host);

                if (Config.password == null || Config.password == "")
                {
                    return OBS.ConnectAsync(host);
                }
                else
                {
                    var password = new OBSWebSocketAuthPassword(Config.password);
                    return OBS.ConnectAsync(host, password);
                }
            }
            catch (Exception e)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error occurred while connecting to OBS instance. Error message: \n{e.Message}");
            }

            return Task.CompletedTask;
        }

        public void SetVariable(string name, string value)
        {
            SetVariable(name, value, Array.Empty<string>());
        }

        public void SetVariable(string name, string value, string[] suggestions)
        {
            MacroDeck.Variables.VariableManager.SetValue(Main.VariablePrefix + VariableNS + "/" + name, value, MacroDeck.Variables.VariableType.String, PluginInstance.Main, suggestions);
        }

        public void SetVariable(string name, int value)
        {
            SetVariable(name, value, Array.Empty<string>());
        }

        public void SetVariable(string name, int value, string[] suggestions)
        {
            MacroDeck.Variables.VariableManager.SetValue(Main.VariablePrefix + VariableNS + "/" + name, value, MacroDeck.Variables.VariableType.Integer, PluginInstance.Main, suggestions);
        }

        public void SetVariable(string name, float value)
        {
            SetVariable(name, value, Array.Empty<string>());
        }

        public void SetVariable(string name, float value, string[] suggestions)
        {
            MacroDeck.Variables.VariableManager.SetValue(Main.VariablePrefix + VariableNS + "/" + name, value, MacroDeck.Variables.VariableType.Float, PluginInstance.Main, suggestions);
        }

        public void SetVariable(string name, bool value)
        {
            SetVariable(name, value, Array.Empty<string>());
        }

        public void SetVariable(string name, bool value, string[] suggestions)
        {
            MacroDeck.Variables.VariableManager.SetValue(Main.VariablePrefix + VariableNS + "/" + name, value, MacroDeck.Variables.VariableType.Bool, PluginInstance.Main, suggestions);
        }

        public void Dispose()
        {
            OBS?.Dispose();
            IsDisposed = true;
            Disposed?.Invoke(this, EventArgs.Empty);
            GC.SuppressFinalize(this);
        }
    }
}
