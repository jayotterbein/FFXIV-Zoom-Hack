using FFXIVZoomHack.Properties;
using ProcessMemoryApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FFXIVZoomHack
{
    public struct ProcessModuleAddress
    {
        public long pBaseOffset;
        public long pModule;
    }

    public partial class Form1 : Form
    {
        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private readonly AppSettings _settings;
        private readonly Dictionary<ProcessMemoryReader, ProcessModuleAddress> _processCollection;

        private bool _shouldQuitNextTimeProcessEmpty;

        //DX11 Only
        const long pCurrentZoom = 0x114;
        const long pMinZoom = 0x118;
        const long pMaxZoom = 0x11C;
        const long pCurrentFOV = 0x120;
        const long pMinFOV = 0x124;
        const long pMaxFOV = 0x128;

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
                        var module = new ProcessModuleAddress();
                        // 00 A0 6F 4B F6 7F 00 00 28 00 00 00 00 00 00 00 48 D4 5A 4B F6 7F 00 00 04 00 00 00 00 00 51 00 00 00 00 00 14 00 40 C0 00 00 00 00 00 00 00 00
                        // 00 A0 6F 4B F6 7F 00 00 28 ** 00 00 00 00 00 00 48 D4 5A 4B F6 7F 00 00 ** ** ** ** ** ** ** 00 00 00 00 00 ** ** ** ** 00 00 00 00 00 00 00 00
                        // 00 A0 6F 4B F6 7F 00 00 00 00 00 00 00 00 00 00 48 D4 5A 4B F6 7F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 E0 6D 73 AB E0 01 00 00 E0 6D 73 AB E0 01
                        // 00 A0 6F 4B F6 7F 00 00 28 00 00 00 00 00 00 00 48 D4 5A 4B F6 7F 00 00 04 00 00 00 3C 00 0B 00 00 00 00 00 0F 00 40 C0 00 00 00 00 00 00 00 00 D0 26 50 1A E5 01 00 00 D0 26 50 1A E5 01
                        module.pBaseOffset = (long)mReader.ScanPtrBySig("00A06F4BF67F0000****00000000000048D45A4BF67F0000")[0];
                        module.pModule = mReader.ReadInt64((IntPtr)((long)mReader.process.Modules[0].BaseAddress + module.pBaseOffset));
                        _processCollection.Add(mReader, module);
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

            Parallel.ForEach(_processCollection.Keys, mReader => {
                mReader.WriteByteArray((IntPtr)(_processCollection[mReader].pModule + pMinZoom), BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray((IntPtr)(_processCollection[mReader].pModule + pMaxZoom), ZoomBytes);

                mReader.WriteByteArray((IntPtr)(_processCollection[mReader].pModule + pMinFOV), BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray((IntPtr)(_processCollection[mReader].pModule + pMaxFOV), FOVBytes);
                mReader.WriteByteArray((IntPtr)(_processCollection[mReader].pModule + pCurrentFOV), FOVBytes);
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
