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

            if (!File.Exists(AppSettings.Path))
            {
                File.Create(AppSettings.Path).Dispose();
                AppSettings _settings = new AppSettings();
                var option = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                };
                string JsonText = JsonSerializer.Serialize(_settings, option);
                File.WriteAllText(AppSettings.Path, JsonText);
            }
            Application.Run(new Form1());
        }
    }
}