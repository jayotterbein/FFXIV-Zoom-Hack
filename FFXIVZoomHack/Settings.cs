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
            AutoQuit = false;

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
        public bool AutoQuit { get; set; }
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
                        case "AutoQuit":
                            settings.AutoQuit = bool.Parse(element.Value);
                            break;
                        case "DX9":
                            settings.DX9_StructureAddress = element.Element("StructureAddress")
                                .Value
                                .Split(',')
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(x => int.Parse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
                                .ToArray();
                            settings.DX9_ZoomCurrent = int.Parse(element.Element("ZoomCurrent").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            settings.DX9_ZoomMax = int.Parse(element.Element("ZoomMax").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            settings.DX9_FovCurrent = int.Parse(element.Element("FovCurrent").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            settings.DX9_FovMax = int.Parse(element.Element("FovMax").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            break;
                        case "DX11":
                            settings.DX11_StructureAddress = element.Element("StructureAddress")
                                .Value
                                .Split(',')
                                .Where(x => !string.IsNullOrEmpty(x))
                                .Select(x => int.Parse(x, NumberStyles.HexNumber, CultureInfo.InvariantCulture))
                                .ToArray();
                            settings.DX11_ZoomCurrent = int.Parse(element.Element("ZoomCurrent").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            settings.DX11_ZoomMax = int.Parse(element.Element("ZoomMax").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            settings.DX11_FovCurrent = int.Parse(element.Element("FovCurrent").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            settings.DX11_FovMax = int.Parse(element.Element("FovMax").Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                            break;
                        case "DesiredZoom":
                            settings.DesiredZoom = float.Parse(element.Value, CultureInfo.InvariantCulture);
                            if (settings.DesiredZoom < 1f || settings.DesiredZoom > 1000f)
                            {
                                settings.DesiredZoom = 20f;
                            }
                            break;
                        case "DesiredFov":
                            settings.DesiredFov = float.Parse(element.Value, CultureInfo.InvariantCulture);
                            if (settings.DesiredFov < 0.01f || settings.DesiredFov > 3f)
                            {
                                settings.DesiredFov = 0.78f;
                            }
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
                new XElement("StructureAddress", string.Join(",", DX9_StructureAddress.Select(x => x.ToString("X", CultureInfo.InvariantCulture)))),
                new XElement("ZoomCurrent", DX9_ZoomCurrent.ToString("X", CultureInfo.InvariantCulture)),
                new XElement("ZoomMax", DX9_ZoomMax.ToString("X", CultureInfo.InvariantCulture)),
                new XElement("FovCurrent", DX9_FovCurrent.ToString("X", CultureInfo.InvariantCulture)),
                new XElement("FovMax", DX9_FovMax.ToString("X", CultureInfo.InvariantCulture))
                );
            yield return new XElement("DX11",
                new XElement("StructureAddress", string.Join(",", DX11_StructureAddress.Select(x => x.ToString("X", CultureInfo.InvariantCulture)))),
                new XElement("ZoomCurrent", DX11_ZoomCurrent.ToString("X", CultureInfo.InvariantCulture)),
                new XElement("ZoomMax", DX11_ZoomMax.ToString("X", CultureInfo.InvariantCulture)),
                new XElement("FovCurrent", DX11_FovCurrent.ToString("X", CultureInfo.InvariantCulture)),
                new XElement("FovMax", DX11_FovMax.ToString("X", CultureInfo.InvariantCulture))
                );
            yield return new XElement("LastUpdate", LastUpdate);

            if ((Control.ModifierKeys & (Keys.Control | Keys.Alt | Keys.Shift)) != 0)
            {
                yield break;
            }
            yield return new XElement("AutoApply", AutoApply.ToString(CultureInfo.InvariantCulture));
            yield return new XElement("AutoQuit", AutoQuit.ToString(CultureInfo.InvariantCulture));
            yield return new XElement("DesiredZoom", DesiredZoom.ToString(CultureInfo.InvariantCulture));
            yield return new XElement("DesiredFov", DesiredFov.ToString(CultureInfo.InvariantCulture));
            yield return new XElement("OffsetUpdateLocation", OffsetUpdateLocation);
        }
    }
}