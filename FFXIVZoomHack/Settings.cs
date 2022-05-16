using System;
using System.IO;

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

        public static string SettingsFile
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFXIVZoomHack");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return Path.Combine(path, "Settings.json");
            }
        }
    }
}