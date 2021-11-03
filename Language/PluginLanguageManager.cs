/* Some parts of this class are based on @PhoenixWyllow's implementation of translation files, he's using in the Soundboard4MacroDeck2 plugin
 * The link to the original file: https://github.com/PhoenixWyllow/Soundboard4MacroDeck2/blob/main/Services/Localization.cs
 */

using SuchByte.MacroDeck.Language;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SuchByte.OBSWebSocketPlugin.Language
{
    public static class PluginLanguageManager
    {
        public static PluginStrings PluginStrings = new PluginStrings();

        


        public static void Initialize()
        {
            LoadLanguage();
            LanguageManager.LanguageChanged += (s, e) => LoadLanguage();
        }

        private static void LoadLanguage()
        {
            // Getting the current language that is set in Macro Deck
            string languageName = LanguageManager.GetLanguageName();

            try
            {
                using (TextReader reader = new StringReader(GetXMLLanguageResource(languageName)))
                {
                    PluginStrings = (PluginStrings)new XmlSerializer(typeof(PluginStrings)).Deserialize(reader);
                }
            }
            catch
            {
                //fallback - should never occur if things are done properly
                PluginStrings = new PluginStrings();
            }
        }


        private static string GetXMLLanguageResource(string languageName)
        {
            var assembly = typeof(PluginStrings).Assembly;
            if (string.IsNullOrEmpty(languageName)
                || !assembly.GetManifestResourceNames().Any(name => name.EndsWith($"{languageName}.xml")))
            {
                languageName = "English"; //This should always be present as default, otherwise the code goes to fallback implementation.
            }

            string languageFileName = $"SuchByte.OBSWebSocketPlugin.Resources.Languages.{languageName}.xml";

            using var resourceStream = assembly.GetManifestResourceStream(languageFileName);
            using var streamReader = new StreamReader(resourceStream);
            return streamReader.ReadToEnd();
        }
    }
}
