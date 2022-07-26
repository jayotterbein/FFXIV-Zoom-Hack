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

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFXIVZoomHack");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "Settings.json");
            if (!File.Exists(path))
            {
                new AppSettings().Save();
            }
            
            Application.Run(new Form1());
        }
    }
}