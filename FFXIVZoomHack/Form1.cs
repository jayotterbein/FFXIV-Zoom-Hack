using FFXIVZoomHack.Properties;
using ProcessMemoryApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private AppSettings settings;

        Dictionary<ProcessMemoryReader, ProcessModuleAddress> ProcessCollection;

        //DX11 Only
        const long pCurrentZoom = 0x114;
        const long pMinZoom = 0x118;
        const long pMaxZoom = 0x11C;
        const long pCurrentFOV = 0x120;
        const long pMinFOV = 0x124;
        const long pMaxFOV = 0x128;

        public Form1()
        {
            settings = JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(AppSettings.Path));
            ProcessCollection = new Dictionary<ProcessMemoryReader, ProcessModuleAddress>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //just need call once

            _autoApplyCheckbox.Checked = settings.AutoApply;
            _autoQuitCheckbox.Checked = settings.AutoQuit;
            _zoomUpDown.Value = (decimal)settings.DesiredZoom;
            _fovUpDown.Value = (decimal)settings.DesiredFov;
        }

        private void FovChanged(object sender, EventArgs e)
        {
            settings.DesiredFov = (float)_fovUpDown.Value;
            SettingSave(this.settings);
            ApplyChanges();
        }

        private void ZoomChanged(object sender, EventArgs e)
        {
            settings.DesiredZoom = (float)_zoomUpDown.Value;
            SettingSave(this.settings);
            ApplyChanges();
        }

        private void Timer1Tick(object sender, EventArgs args)
        {
            ReadyIndicator.Image = ProcessCollection.Count == 0 ? Resources.RedLight : Resources.GreenLight;

            try
            {
                List<int> activePids = Process.GetProcessesByName("ffxiv_dx11").ToList().Select(x => x.Id).ToList();

                for (int i = 0; i < activePids.Count; i++)
                {
                    //Add new process
                    if (!ProcessCollection.Keys.Select(x => x.process.Id).Contains(activePids[i]))
                    {
                        ProcessMemoryReader mReader = new ProcessMemoryReader(activePids[i]);
                        ProcessModuleAddress module = new ProcessModuleAddress();
                        module.pBaseOffset = (long)mReader.ScanPtrBySig("48833D********007411488B0D********4885C97405E8********488D0D")[0];
                        module.pModule = mReader.ReadInt64((IntPtr)((long)mReader.process.Modules[0].BaseAddress + module.pBaseOffset));
                        ProcessCollection.Add(mReader, module);
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
                foreach (ProcessMemoryReader mReader in ProcessCollection.Keys)
                {
                    if (!activePids.Contains(mReader.process.Id))
                    {
                        ProcessCollection.Remove(mReader);
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

                if (settings.AutoApply&& activePids.Count() != 0)
                {
                    ApplyChanges();
                }

                if (activePids.Count() == 0 && settings.AutoQuit)
                {
                    Close();
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
            byte[] ZoomBytes = BitConverter.GetBytes(Convert.ToSingle(_zoomUpDown.Value));
            byte[] FOVBytes = BitConverter.GetBytes(Convert.ToSingle(_fovUpDown.Value));

            var result = Parallel.ForEach(ProcessCollection.Keys, (mReader) => {
                mReader.WriteByteArray((IntPtr)(ProcessCollection[mReader].pModule + pMinZoom), BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray((IntPtr)(ProcessCollection[mReader].pModule + pMaxZoom), ZoomBytes);

                mReader.WriteByteArray((IntPtr)(ProcessCollection[mReader].pModule + pMinFOV), BitConverter.GetBytes(Convert.ToSingle(0.01)));
                mReader.WriteByteArray((IntPtr)(ProcessCollection[mReader].pModule + pMaxFOV), FOVBytes);
                mReader.WriteByteArray((IntPtr)(ProcessCollection[mReader].pModule + pCurrentFOV), FOVBytes);
            });
        }

        public void SettingSave(AppSettings settings)
        {
            var option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            string JsonText = JsonSerializer.Serialize(settings, option);

            File.WriteAllText(AppSettings.Path, JsonText);
        }

        private void AutoApplyCheckChanged(object sender, EventArgs e)
        {
            settings.AutoApply = _autoApplyCheckbox.Checked;
            SettingSave(settings);
            if (settings.AutoApply && ProcessCollection.Keys.Count() != 0)
            {
                ApplyChanges();
            }
        }

        private void AutoQuitCheckChanged(object sender, EventArgs e)
        {
            settings.AutoQuit = _autoQuitCheckbox.Checked;
            SettingSave(settings);
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
    }
}