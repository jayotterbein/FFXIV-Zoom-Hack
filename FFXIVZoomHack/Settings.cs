using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FFXIVZoomHack
{
    [Serializable]
    public class Settings
    {
        private Settings()
        {
            AutoApply = true;

            DX9_StructureAddress = new[] {0xECC8D0};
            DX9_ZoomMax = 0xF0;

            DX11_StructureAddress = new[] {0x147F680};
            DX11_ZoomCurrent = 0xF8;
            DX11_ZoomMax = 0x100;
            DX11_FovCurrent = 0x104;
            DX11_FovMax = 0x108;

            DesiredZoom = 20;
            DesiredFov = 0.78f;

            OffsetUpdateLocation = @"https://raw.githubusercontent.com/jayotterbein/FFXIV-Zoom-Hack/master/Offsets.xml";
            LastUpdate = "2015-07-17 - patch 3.05";
        }

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

        public bool AutoApply { get; set; }
        public float DesiredFov { get; set; }
        public float DesiredZoom { get; set; }

        public int[] DX9_StructureAddress { get; set; }
        public int DX9_ZoomCurrent { get; set; }
        public int DX9_ZoomMax { get; set; }
        public int DX9_FovCurrent { get; set; }
        public int DX9_FovMax { get; set; }

        public int[] DX11_StructureAddress { get; set; }
        public int DX11_ZoomCurrent { get; set; }
        public int DX11_ZoomMax { get; set; }
        public int DX11_FovCurrent { get; set; }
        public int DX11_FovMax { get; set; }

        public string OffsetUpdateLocation { get; set; }
        public string LastUpdate { get; set; }

        public static Settings Load(string location = null)
        {
            var settings = new Settings();
            try
            {
                var doc = XDocument.Load(location ?? SettingsFile);
                var root = doc.Element("Root");
                foreach (var element in root.Elements())
                {
                    switch (element.Name.LocalName)
                    {
                        case "AutoApply":
                            settings.AutoApply = bool.Parse(element.Value);
                            break;
                        case "DX9":
                            settings.DX9_StructureAddress = element.Element("StructureAddress")
                                .Value
                                .Split(',')
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(x => int.Parse(x, NumberStyles.HexNumber))
                                .ToArray();
                            settings.DX9_ZoomCurrent = int.Parse(element.Element("ZoomCurrent").Value, NumberStyles.HexNumber);
                            settings.DX9_ZoomMax = int.Parse(element.Element("ZoomMax").Value, NumberStyles.HexNumber);
                            settings.DX9_FovCurrent = int.Parse(element.Element("FovCurrent").Value, NumberStyles.HexNumber);
                            settings.DX9_FovMax = int.Parse(element.Element("FovMax").Value, NumberStyles.HexNumber);
                            break;
                        case "DX11":
                            settings.DX11_StructureAddress = element.Element("StructureAddress")
                                .Value
                                .Split(',')
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(x => int.Parse(x, NumberStyles.HexNumber))
                                .ToArray();
                            settings.DX11_ZoomCurrent = int.Parse(element.Element("ZoomCurrent").Value, NumberStyles.HexNumber);
                            settings.DX11_ZoomMax = int.Parse(element.Element("ZoomMax").Value, NumberStyles.HexNumber);
                            settings.DX11_FovCurrent = int.Parse(element.Element("FovCurrent").Value, NumberStyles.HexNumber);
                            settings.DX11_FovMax = int.Parse(element.Element("FovMax").Value, NumberStyles.HexNumber);
                            break;
                        case "DesiredZoom":
                            settings.DesiredZoom = float.Parse(element.Value);
                            break;
                        case "DesiredFov":
                            settings.DesiredFov = float.Parse(element.Value);
                            break;
                        case "LastUpdate":
                            settings.LastUpdate = element.Value;
                            break;
                    }
                }
            }
            catch
            {
                /* xml failed to load, lose settings */
            }
            return settings;
        }

        public void Save()
        {
            var doc = new XDocument(
                new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                new XElement("Root",
                    new XElement("AutoApply", AutoApply),
                    new XElement("DX9",
                        new XElement("StructureAddress", string.Join(",", DX9_StructureAddress.Select(x => x.ToString("X")))),
                        new XElement("ZoomCurrent", DX9_ZoomCurrent.ToString("X")),
                        new XElement("ZoomMax", DX9_ZoomMax.ToString("X")),
                        new XElement("FovCurrent", DX9_FovCurrent.ToString("X")),
                        new XElement("FovMax", DX9_FovMax.ToString("X"))
                        ),
                    new XElement("DX11",
                        new XElement("StructureAddress", string.Join(",", DX11_StructureAddress.Select(x => x.ToString("X")))),
                        new XElement("ZoomCurrent", DX11_ZoomCurrent.ToString("X")),
                        new XElement("ZoomMax", DX11_ZoomMax.ToString("X")),
                        new XElement("FovCurrent", DX11_FovCurrent.ToString("X")),
                        new XElement("FovMax", DX11_FovMax.ToString("X"))
                        ),
                    new XElement("DesiredZoom", DesiredZoom),
                    new XElement("DesiredFov", DesiredFov),
                    new XElement("OffsetUpdateLocation", OffsetUpdateLocation),
                    new XElement("LastUpdate", LastUpdate)
                    )
                );

            using (var fstream = new FileStream(SettingsFile, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fstream, Encoding.UTF8))
            {
                doc.Save(sw, SaveOptions.None);
            }
        }
    }
}