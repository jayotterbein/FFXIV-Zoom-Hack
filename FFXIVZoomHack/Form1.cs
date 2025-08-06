using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using FFXIVZoomHack.Properties;

namespace FFXIVZoomHack
{
    public struct ProcessModuleAddress
    {
        public nint pBaseOffset;
        public nint pModule;
    }

    public partial class Form1 : Form
    {
        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(nint hWnd);

        private readonly AppSettings _settings;
        private readonly Dictionary<ProcessMemoryReader, ProcessModuleAddress> _processCollection;

        private bool _shouldQuitNextTimeProcessEmpty;

        //DX11 x86_64 Only

        //Pre 7.3 offsets
        //private const nint pCurrentZoom = 0x114;
        //private const nint pMinZoom = 0x118;
        //private const nint pMaxZoom = 0x11C;
        //private const nint pCurrentFOV = 0x120;
        //private const nint pMinFOV = 0x124;
        //private const nint pMaxFOV = 0x128;

        //7.3 offsets
        private const nint pCurrentZoom = 0x124;
        private const nint pMinZoom = 0x128;
        private const nint pMaxZoom = 0x12C;
        private const nint pCurrentFOV = 0x130;
        private const nint pMinFOV = 0x134;
        private const nint pMaxFOV = 0x138;

        private NotifyIcon _notifyIcon;

        public Form1()
        {
            _settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(AppSettings.SettingsFile));
            _processCollection = new Dictionary<ProcessMemoryReader, ProcessModuleAddress>();
            InitializeComponent();

            _notifyIcon = new NotifyIcon(components);
            _notifyIcon.Text = "FFXIV Zoom Hack";
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            _notifyIcon.BalloonTipText = "Double click to open app";
            _notifyIcon.BalloonTipTitle = "FFXIV Zoom Hack";
            _notifyIcon.ShowBalloonTip(1000);
            using (var icon = GetType().Assembly.GetManifestResourceStream($"{GetType().Namespace}.zoom_kNq_icon.ico"))
            {
                _notifyIcon.Icon = new Icon(icon);
            }

            _notifyIcon.DoubleClick += (sender, args) =>
            {
                Show();
                WindowState = FormWindowState.Normal;
                _notifyIcon.Visible = false;
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //just need call once

            _shouldQuitNextTimeProcessEmpty = false;

            _autoApplyCheckbox.Checked = _settings.AutoApply;
            _autoQuitCheckbox.Checked = _settings.AutoQuit;
            _zoomUpDown.Value = (decimal)_settings.DesiredZoom;
            _fovUpDown.Value = (decimal)_settings.DesiredFov;
        }

        private void FovChanged(object sender, EventArgs e)
        {
            _settings.DesiredFov = (float)_fovUpDown.Value;
            SettingSave(_settings);
            ApplyChanges();
        }

        private void ZoomChanged(object sender, EventArgs e)
        {
            _settings.DesiredZoom = (float)_zoomUpDown.Value;
            SettingSave(_settings);
            ApplyChanges();
        }

        private void Timer1Tick(object sender, EventArgs args)
        {
            ReadyIndicator.Image = _processCollection.Count == 0 ? Resources.RedLight : Resources.GreenLight;

            try
            {
                var activePids = Process.GetProcessesByName("ffxiv_dx11").ToList().Select(x => x.Id).ToList();

                for (var i = 0; i < activePids.Count; i++)
                {
                    //Add new process
                    if (!_processCollection.Keys.Select(x => x.process.Id).Contains(activePids[i]))
                    {
                        var mReader = new ProcessMemoryReader(activePids[i]);
                        var moduleInfo = new ProcessModuleAddress();

                        //OLD
                        //module.pBaseOffset = (long)mReader.ScanPtrBySig("48833D********007411488B0D********4885C97405E8********488D0D")[0];

                        //For Reference: The desired pointer follows the scan signature.
                        //Below is an example of the assembly.  Wildcards are needed for address references.

                        //29CB6429AE8 - 48 8D 0D E14BF601 - lea rcx,[29CB838E6D0]
                        //29CB6429AEF - E8 8C530100 - call 29CB643EE80
                        //29CB6429AF4 - 48 39 35 250B1502 - cmp[29CB857A620],rsi
                        //29CB6429AFB - 74 11 - je 29CB6429B0E
                        //29CB6429AFD - 48 8B 0D 040B1502 - mov rcx,[29CB857A608]
                        //29CB6429B04 - 48 85 C9 - test rcx,rcx
                        //29CB6429B07 - 74 05 - je 29CB6429B0E
                        //29CB6429B09 - E8 528E0200 - call 29CB6452960
                        //29CB6429B0E - 48 8D 0D 5BAC1402 - lea rcx,[29CB8574770]

                        moduleInfo.pBaseOffset = mReader.ScanPtrBySig("488D0D********E8********483935********7411488B0D********4885C97405E8********488D0D")[0];
                        moduleInfo.pModule = new nint(mReader.ReadInt64(mReader.process.Modules[0].BaseAddress + moduleInfo.pBaseOffset));

                        //Was useful, but not needed for now.
                        //labelPointer.Text = "Pointer Found at: "+((long)mReader.process.Modules[0].BaseAddress + module.pBaseOffset).ToString("X");

                        _processCollection.Add(mReader, moduleInfo);
                    }
                    //update combo box
                    try
                    {
                        _processList.Items[i] = activePids[i];
                    }
                    catch
                    {
                        _processList.Items.Add(activePids[i]);
                    }
                }

                //delete closed process
                foreach (ProcessMemoryReader mReader in _processCollection.Keys)
                {
                    if (!activePids.Contains(mReader.process.Id))
                    {
                        _processCollection.Remove(mReader);
                    }
                }

                //clear combo box
                while (true)
                {
                    if (_processList.Items.Count == activePids.Count) break;
                    else _processList.Items.RemoveAt(_processList.Items.Count - 1);
                }

                if (_processList.Items.Count > 0 && _processList.SelectedItem == null)
                {
                    _processList.SelectedIndex = 0;
                }

                if (_settings.AutoApply && activePids.Count != 0)
                {
                    ApplyChanges();
                }

                if (!activePids.Any() && _settings.AutoQuit && _shouldQuitNextTimeProcessEmpty)
                {
                    Close();
                }
                else if (activePids.Any())
                {
                    _shouldQuitNextTimeProcessEmpty = true;
                }
            }
            catch (Exception ex)
            {
                var logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FFXIVZoomHack", "log.txt");
                using (var sw = File.AppendText(logFile))
                {
                    sw.WriteLine(ex.Message);
                }
            }
        }

        private void ApplyChanges()
        {
            var ZoomBytes = BitConverter.GetBytes(Convert.ToSingle(_zoomUpDown.Value));
            var FOVBytes = BitConverter.GetBytes(Convert.ToSingle(_fovUpDown.Value));

            Parallel.ForEach(_processCollection.Keys, mReader =>
            {
                mReader.WriteByteArray(_processCollection[mReader].pModule + pMinZoom, BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray(_processCollection[mReader].pModule + pMaxZoom, ZoomBytes);

                mReader.WriteByteArray(_processCollection[mReader].pModule + pMinFOV, BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray(_processCollection[mReader].pModule + pMaxFOV, FOVBytes);
                mReader.WriteByteArray(_processCollection[mReader].pModule + pCurrentFOV, FOVBytes);
            });
        }

        private static void SettingSave(AppSettings settings)
        {
            var option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var jsonText = JsonSerializer.Serialize(settings, option);
            File.WriteAllText(AppSettings.SettingsFile, jsonText);
        }

        private void AutoApplyCheckChanged(object sender, EventArgs e)
        {
            _settings.AutoApply = _autoApplyCheckbox.Checked;
            SettingSave(_settings);
            if (_settings.AutoApply && _processCollection.Keys.Count() != 0)
            {
                ApplyChanges();
            }
        }

        private void AutoQuitCheckChanged(object sender, EventArgs e)
        {
            _settings.AutoQuit = _autoQuitCheckbox.Checked;
            SettingSave(_settings);
        }

        private void _gotoProcessButton_Click(object sender, EventArgs e)
        {
            if (_processList.SelectedItem == null)
            {
                return;
            }

            var selectedPid = (int)_processList.SelectedItem;
            using (var process = Process.GetProcessById(selectedPid))
            {
                var handle = process.MainWindowHandle;
                if (handle != IntPtr.Zero)
                {
                    SetForegroundWindow(handle);
                }
            }
        }

        private void _zoomDefaultButton_Click(object sender, EventArgs e)
        {
            _zoomUpDown.Value = 20m;
        }

        private void _fovDefaultButton_Click(object sender, EventArgs e)
        {
            _fovUpDown.Value = .78m;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                _notifyIcon.Visible = true;
            }
        }
    }
}
