using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FFXIVZoomHack
{
    public static class Memory
    {
        private const ProcessAccessFlags ProcessFlags =
            ProcessAccessFlags.VirtualMemoryRead
            | ProcessAccessFlags.VirtualMemoryWrite
            | ProcessAccessFlags.VirtualMemoryOperation
            | ProcessAccessFlags.QueryInformation;

        public static IEnumerable<int> GetPids()
        {
            foreach (var p in Process.GetProcesses())
            {
                if (string.Equals(p.ProcessName, "ffxiv", StringComparison.Ordinal)
                    || string.Equals(p.ProcessName, "ffxiv_dx11", StringComparison.Ordinal))
                {
                    yield return p.Id;
                }
                p.Dispose();
            }
        }

        public static void Apply(Settings settings, int pid)
        {
            if (string.Equals(settings.LastUpdate, "unupdated", StringComparison.Ordinal))
            {
                MessageBox.Show("Memory offsets need updating, click the update offsets button.");
                return;
            }

            try
            {
                Process.EnterDebugMode();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get debugging rights: " + ex.Message, ex);
            }

            using (var p = Process.GetProcessById(pid))
            {
                var hProcess = IntPtr.Zero;
                try
                {
                    hProcess = OpenProcess(ProcessFlags, false, pid);
                    if (string.Equals(p.ProcessName, "ffxiv_dx11", StringComparison.Ordinal))
                    {
                        ApplyX64(settings, hProcess);
                    }
                }
                finally
                {
                    if (hProcess != IntPtr.Zero)
                    {
                        CloseHandle(hProcess);
                    }
                }
            }
        }

        private static void ApplyX64(Settings settings, IntPtr hProcess)
        {
            var addr = GetAddress(8, hProcess, settings.DX11_StructureAddress, settings.DX11_ZoomMax);
            Write(settings.DesiredZoom, hProcess, addr);
            addr = GetAddress(8, hProcess, settings.DX11_StructureAddress, settings.DX11_ZoomCurrent);
            Write(settings.DesiredZoom, hProcess, addr);

            addr = GetAddress(8, hProcess, settings.DX11_StructureAddress, settings.DX11_FovCurrent);
            Write(settings.DesiredFov, hProcess, addr);
            addr = GetAddress(8, hProcess, settings.DX11_StructureAddress, settings.DX11_FovMax);
            Write(settings.DesiredFov, hProcess, addr);
        }

        private static void Write(float value, IntPtr hProcess, IntPtr address)
        {
            var buffer = BitConverter.GetBytes(value);
            if (!WriteProcessMemory(hProcess, address, buffer, buffer.Length, out var written))
            {
                throw new Exception("Could not write process memory: " + Marshal.GetLastWin32Error());
            }
        }

        private static IntPtr GetAddress(byte size, IntPtr hProcess, IEnumerable<int> offsets, int finalOffset)
        {
            var addr = GetBaseAddress(hProcess);
            var buffer = new byte[size];
            foreach (var offset in offsets)
            {
                if (!ReadProcessMemory(hProcess, IntPtr.Add(addr, offset), buffer, buffer.Length, out var read))
                {
                    throw new Exception("Unable to read process memory");
                }
                addr = (size == 8)
                    ? new IntPtr(BitConverter.ToInt64(buffer, 0))
                    : new IntPtr(BitConverter.ToInt32(buffer, 0));
            }
            return IntPtr.Add(addr, finalOffset);
        }

        private static IntPtr GetBaseAddress(IntPtr hProcess)
        {
            var hModules = new IntPtr[1024];
            var uiSize = (uint) (Marshal.SizeOf(typeof(IntPtr)) * hModules.Length);
            var gch = GCHandle.Alloc(hModules, GCHandleType.Pinned);
            try
            {
                var pModules = gch.AddrOfPinnedObject();
                if (EnumProcessModules(hProcess, pModules, uiSize, out var cbNeeded) != 1)
                {
                    throw new Exception("Could not enumerate modules: " + Marshal.GetLastWin32Error());
                }

                var mainModule = IntPtr.Zero;
                var modulesLoaded = (int) (cbNeeded / Marshal.SizeOf(typeof(IntPtr)));
                for (var i = 0; i < modulesLoaded; i++)
                {
                    var moduleFilenameBuilder = new StringBuilder(1024);
                    if (GetModuleFileNameEx(hProcess, hModules[i], moduleFilenameBuilder, moduleFilenameBuilder.Capacity) == 0)
                    {
                        throw new Exception("Could not get module filename: " + Marshal.GetLastWin32Error());
                    }

                    var moduleFilename = moduleFilenameBuilder.ToString();
                    if (!string.IsNullOrEmpty(moduleFilename) && moduleFilename.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        mainModule = hModules[i];
                        break;
                    }
                }

                if (mainModule == IntPtr.Zero)
                {
                    throw new Exception("Could not find module for executable");
                }

                if (!GetModuleInformation(hProcess, mainModule, out var moduleInfo, (uint) Marshal.SizeOf<ModuleInfo>()))
                {
                    throw new Exception("Could not get module information from process" + Marshal.GetLastWin32Error());
                }

                return moduleInfo.lpBaseOfDll;
            }
            finally
            {
                gch.Free();
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("psapi.dll", SetLastError = true)]
        private static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out ModuleInfo lpmodinfo, uint cb);

        [DllImport("psapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int EnumProcessModules(IntPtr hProcess, [Out] IntPtr lphModule, uint cb, out uint lpcbNeeded);

        [DllImport("psapi.dll", SetLastError = true)]
        private static extern int GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpFilename, int nSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct ModuleInfo
        {
             public IntPtr lpBaseOfDll;
             public uint SizeOfImage;
             public IntPtr EntryPoint;
        }

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }
    }
}