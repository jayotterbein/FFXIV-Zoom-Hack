using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FFXIVZoomHack
{
    [Serializable]
    public class Settings
    {
        private static string SettingsFile
        {
            get
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFXIVZoomHack");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return Path.Combine(path, "Settings.xml");
            }
        }

        public static Settings Load()
        {
            if (File.Exists(SettingsFile))
            {
                using (var fstream = File.OpenRead(SettingsFile))
                {
                    try
                    {
                        throw new NotImplementedException();
                    }
                    catch
                    {
                        /* xml failed to load, lose settings */
                    }
                }
            }
            return new Settings();
        }

        public void Save()
        {
            using (var fstream = File.OpenWrite(SettingsFile))
            {
                throw new NotImplementedException();
            }
        }

        private Settings()
        {
            AutoApply = true;
            DX9_StructureAddress = new int[0];
            DX9_ZoomCurrent = new int[0];
            DX9_ZoomMax = new int[0];
            DX9_FovCurrent = new int[0];
            DX9_FovMax = new int[0];
            DX11_StructureAddress = new int[0];
            DX11_ZoomCurrent = new int[0];
            DX11_ZoomMax = new int[0];
            DX11_FovCurrent = new int[0];
            DX11_FovMax = new int[0];
        }

        public bool AutoApply { get; set; }

        [XmlIgnore]
        public int[] DX9_StructureAddress { get; set; }
        [XmlIgnore]
        public int[] DX9_ZoomCurrent { get; set; }
        [XmlIgnore]
        public int[] DX9_ZoomMax { get; set; }
        [XmlIgnore]
        public int[] DX9_FovCurrent { get; set; }
        [XmlIgnore]
        public int[] DX9_FovMax { get; set; }

        [XmlIgnore]
        public int[] DX11_StructureAddress { get; set; }
        [XmlIgnore]
        public int[] DX11_ZoomCurrent { get; set; }
        [XmlIgnore]
        public int[] DX11_ZoomMax { get; set; }
        [XmlIgnore]
        public int[] DX11_FovCurrent { get; set; }
        [XmlIgnore]
        public int[] DX11_FovMax { get; set; }
    }
}