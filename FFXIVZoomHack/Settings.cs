using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FFXIVZoomHack
{
    [Serializable]
    public class Settings
    {
        private Settings()
        {
            AutoApply = true;

            DesiredZoom = 20;
            DesiredFov = 0.78f;

            OffsetUpdateLocation = @"https://raw.githubusercontent.com/jayotterbein/FFXIV-Zoom-Hack/master/Offsets.xml";
            LastUpdate = "unupdated";
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
                    GetSaveElements().ToArray<object>()
                    )
                );

            using (var fstream = new FileStream(SettingsFile, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fstream, Encoding.UTF8))
            {
                doc.Save(sw, SaveOptions.None);
            }
        }

        private IEnumerable<XElement> GetSaveElements()
        {
            yield return new XElement("DX9",
                new XElement("StructureAddress", string.Join(",", DX9_StructureAddress.Select(x => x.ToString("X")))),
                new XElement("ZoomCurrent", DX9_ZoomCurrent.ToString("X")),
                new XElement("ZoomMax", DX9_ZoomMax.ToString("X")),
                new XElement("FovCurrent", DX9_FovCurrent.ToString("X")),
                new XElement("FovMax", DX9_FovMax.ToString("X"))
                );
            yield return new XElement("DX11",
                new XElement("StructureAddress", string.Join(",", DX11_StructureAddress.Select(x => x.ToString("X")))),
                new XElement("ZoomCurrent", DX11_ZoomCurrent.ToString("X")),
                new XElement("ZoomMax", DX11_ZoomMax.ToString("X")),
                new XElement("FovCurrent", DX11_FovCurrent.ToString("X")),
                new XElement("FovMax", DX11_FovMax.ToString("X"))
                );
            yield return new XElement("LastUpdate", LastUpdate);

            if ((Control.ModifierKeys & (Keys.Control | Keys.Alt | Keys.Shift)) != 0)
            {
                yield break;
            }
            yield return new XElement("AutoApply", AutoApply);
            yield return new XElement("DesiredZoom", DesiredZoom);
            yield return new XElement("DesiredFov", DesiredFov);
            yield return new XElement("OffsetUpdateLocation", OffsetUpdateLocation);
        }
    }
}