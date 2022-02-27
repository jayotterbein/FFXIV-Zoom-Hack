using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

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

        public static string Path
        {
            get
            {
                var path = new FileInfo(Application.ExecutablePath).Directory.ToString() + @"\Setting.json";
                return path;
            }
        }

        

    }
}