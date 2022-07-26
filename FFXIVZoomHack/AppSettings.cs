using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace FFXIVZoomHack
{
    [Serializable]
    public class AppSettings
    {
        public bool AutoApply { get; set; }
        public bool AutoQuit { get; set; }
        public float DesiredFov { get; set; }
        public float DesiredZoom { get; set; }

        public AppSettings()
        {
            AutoApply = true;
            AutoQuit = false;
            DesiredZoom = 20;
            DesiredFov = 0.78f;
        }

        public void Save()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var jsonText = JsonSerializer.Serialize(this, options);
            File.WriteAllText(FilePath, jsonText,System.Text.Encoding.UTF8);
        }

        public static AppSettings Load()
        {
            return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(FilePath, System.Text.Encoding.UTF8));
        }

        public static string FilePath
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFXIVZoomHack", "Settings.json");
                return path;
            }
        }
    }
}