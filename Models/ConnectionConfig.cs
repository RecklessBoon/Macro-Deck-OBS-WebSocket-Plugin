using SuchByte.OBSWebSocketPlugin.Language;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Services.Maps;

namespace SuchByte.OBSWebSocketPlugin.Models
{
    public class ConnectionConfig
    {
        public string name;
        public string host;
        public string password;

        public Dictionary<string, string> ToCredentials()
        {
            return new Dictionary<string, string>
            {
                ["name"] = name,
                ["host"] = host,
                ["password"] = password
            };
        }

        public static ConnectionConfig FromCredentials(Dictionary<string, string> creds)
        {
            if (creds == null) return new ConnectionConfig();
            if (!creds.ContainsKey("host") || !creds.ContainsKey("password"))
            {
                throw new ArgumentException("Credentials must have the keys: [host, password]");
            }

            var config = new ConnectionConfig
            {
                name = creds.ContainsKey("name") ? creds["name"] : PluginLanguageManager.PluginStrings.Default.ToLower(),
                host = creds["host"],
                password = creds["password"]
            };
            return config;
        }
    }
}
