using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace FFXIVZoomHack
{
    public static class Program
    {
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            if (!File.Exists(AppSettings.SettingsFile))
            {
                var jsonText = JsonSerializer.Serialize(new AppSettings(), new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                });
                File.WriteAllText(AppSettings.SettingsFile, jsonText);
            }
            Application.Run(new Form1());
        }
    }
}