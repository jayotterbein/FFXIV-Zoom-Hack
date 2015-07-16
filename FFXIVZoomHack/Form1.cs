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
            _timer = new Timer(TimerCallback, null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
            _autoApplyCheckbox.Checked = Settings.AutoApply;

            _autoApplyCheckbox.CheckedChanged += AutoApplyCheckChanged;
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
                        var knownPids = _processList.Items.Cast<int>()
                            .ToArray();
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
                                UpdateZoom(newPids);
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

        private static void UpdateZoom(IEnumerable<int> pids)
        {
            foreach (var pid in pids)
            {
                Memory.Apply(Settings, pid);
            }
        }

        private static void AutoApplyCheckChanged(object sender, EventArgs e)
        {
            Settings.AutoApply = !Settings.AutoApply;
            Settings.Save();
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
    }
}