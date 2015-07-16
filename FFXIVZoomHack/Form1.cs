using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace FFXIVZoomHack
{
    public partial class Form1 : Form
    {
        private static readonly Lazy<Settings> LazySettings = new Lazy<Settings>(Settings.Load);
        private Timer _timer;

        public Form1()
        {
            InitializeComponent();
        }

        private static Settings Settings
        {
            get { return LazySettings.Value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _autoApplyCheckbox.Checked = Settings.AutoApply;
            _autoApplyCheckbox.CheckedChanged += AutoApplyCheckChanged;

            _zoomUpDown.Value = (decimal) Settings.DesiredZoom;
            _zoomUpDown.ValueChanged += NumberChanged;
            _fovUpDown.Value = (decimal) Settings.DesiredFov;
            _fovUpDown.ValueChanged += NumberChanged;

            _timer = new Timer(TimerCallback, null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }

        private void NumberChanged(object sender, EventArgs e)
        {
            Settings.DesiredZoom = (float) _zoomUpDown.Value;
            Settings.DesiredFov = (float) _fovUpDown.Value;
            Settings.Save();
            ApplyChanges();
        }

        private void TimerCallback(object state)
        {
            try
            {
                Invoke(
                    () =>
                    {
                        var activePids = Memory.GetPids()
                            .ToArray();
                        var knownPids = GetCurrentPids();
                        foreach (var pid in activePids.Except(knownPids))
                        {
                            _processList.Items.Add(pid);
                        }

                        for (var i = _processList.Items.Count - 1; i >= 0; i--)
                        {
                            var pid = (int) _processList.Items[i];
                            if (!activePids.Contains(pid))
                            {
                                _processList.Items.RemoveAt(i);
                            }
                        }

                        if (_processList.Items.Count > 0 && _processList.SelectedItem == null)
                        {
                            _processList.SelectedIndex = 0;
                        }

                        if (Settings.AutoApply)
                        {
                            var newPids = activePids.Except(knownPids)
                                .ToArray();
                            if (newPids.Any())
                            {
                                ApplyChanges(newPids);
                            }
                        }
                    });
            }
            catch
            {
                /* something went wrong on the background thread, should find a way to log this..*/
            }
            finally
            {
                _timer.Change(TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
            }
        }

        private IReadOnlyCollection<int> GetCurrentPids()
        {
            return _processList.Items.Cast<int>().ToArray();
        }

        private void ApplyChanges(IEnumerable<int> pids = null)
        {
            foreach (var pid in (pids ?? GetCurrentPids()))
            {
                Memory.Apply(Settings, pid);
            }
        }

        private void AutoApplyCheckChanged(object sender, EventArgs e)
        {
            Settings.AutoApply = !Settings.AutoApply;
            Settings.Save();
            if (Settings.AutoApply)
            {
                ApplyChanges();
            }
        }

        private void Invoke(Action action)
        {
            Delegate d = action;
            Invoke(d);
        }

        private void _gotoProcessButton_Click(object sender, EventArgs e)
        {
            if (_processList.SelectedItem == null)
            {
                return;
            }

            var selectedPid = (int) _processList.SelectedItem;
            using (var process = Process.GetProcessById(selectedPid))
            {
                var handle = process.MainWindowHandle;
                if (handle != IntPtr.Zero)
                {
                    SetForegroundWindow(handle);
                }
            }
        }

        [DllImport("USER32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void _zoomDefaultButton_Click(object sender, EventArgs e)
        {
            _zoomUpDown.Value = 20m;
        }

        private void _fovDefaultButton_Click(object sender, EventArgs e)
        {
            _fovUpDown.Value = .78m;
        }
    }
}