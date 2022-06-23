using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace FFXIVZoomHack
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!File.Exists(AppSettings.SettingsFile))
            {
                var option = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                };
                var jsonText = JsonSerializer.Serialize(new AppSettings(), option);
                File.WriteAllText(AppSettings.SettingsFile, jsonText);
            }
            Application.Run(new Form1());
        }
    }
}