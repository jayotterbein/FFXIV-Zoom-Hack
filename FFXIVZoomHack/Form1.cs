using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using FFXIVZoomHack.Properties;
using Timer = System.Threading.Timer;

namespace FFXIVZoomHack
{
    public partial class Form1 : Form
    {
        private static readonly Lazy<Settings> LazySettings = new Lazy<Settings>(Settings.Load);
        private Timer _timer;

        private static Settings Settings
        {
            get { return LazySettings.Value; }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _timer = new Timer(TimerCallback, null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
            _autoApplyCheckbox.Checked = Settings.AutoApply;
        }

        private void TimerCallback(object state)
        {
            try
            {
                Invoke(
                    () =>
                    {
                        var activePids = Memory.GetPids().ToArray();
                        var knownPids = _processList.Items.Cast<int>().ToArray();
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
                            var newPids = activePids.Except(knownPids).ToArray();
                            if (newPids.Any())
                            {
                                UpdateZoom(newPids);
                            }
                        }
                    });
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
                Memory.Apply(pid);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoApply = !Settings.AutoApply;
            Settings.Save();
        }

        private void Invoke(Action action)
        {
            Delegate d = action;
            Invoke(d);
        }
    }
}