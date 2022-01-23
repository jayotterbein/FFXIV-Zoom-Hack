using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace FFXIVZoomHack
{
    [Serializable]
    public class Settings
    {
        private Settings()
        {
            AutoApply = true;
            AutoQuit = false;
            DesiredZoom = 20;
            DesiredFov = 0.78f;
        }

        
        public bool AutoApply { get; set; }
        public bool AutoQuit { get; set; }
        public float DesiredFov { get; set; }
        public float DesiredZoom { get; set; }

        private static string SettingsPath
        {
            get
            {
                var path = new FileInfo(Application.ExecutablePath).Directory.ToString() + @"\Setting.json";
                if (!File.Exists(path))
                {
                    Settings settings = new Settings();
                    settings.Save();
                }
                return path;
            }
        }

        public static Settings Load()
        {
            var settings = new Settings();
            try
            {
                settings = JsonSerializer.Deserialize<Settings>(SettingsPath);
            }
            catch { }
            return settings;
        }


        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            string JsonText = JsonSerializer.Serialize(this, options);

            File.WriteAllText(SettingsPath, JsonText);
        }

    }
}