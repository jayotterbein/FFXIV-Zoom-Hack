using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        public static void Apply(int pid)
        {
            using (var p = Process.GetProcessById(pid))
            {
                var ptr = IntPtr.Zero;
                try
                {
                    ptr = OpenProcess(ProcessFlags, false, pid);
                    if (string.Equals(p.ProcessName, "ffxiv", StringComparison.Ordinal))
                    {
                        ApplyX86(p, ptr);
                    }
                    else if (string.Equals(p.ProcessName, "ffxiv_dx11", StringComparison.Ordinal))
                    {
                        ApplyX64(p, ptr);
                    }
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        CloseHandle(ptr);
                    }
                }
            }
        }

        private static void ApplyX86(Process process, IntPtr ptr)
        {
            var addr = GetAddress(4, process, ptr, new[] {0xECC8D0}, 0xF0);
            var buffer = BitConverter.GetBytes(500.0f);
            IntPtr written;
            if (!(WriteProcessMemory(ptr, addr, buffer, buffer.Length, out written)))
            {
                throw new Exception("Could not write process memory: " + Marshal.GetLastWin32Error());
            }
        }

        private static void ApplyX64(Process process, IntPtr ptr)
        {
            var settings = Settings.Load();

            var addr = GetAddress(8, process, ptr, settings.DX11_StructureAddress, settings.DX11_ZoomMax);
            var buffer = BitConverter.GetBytes(50.0f);
            IntPtr written;
            if (!(WriteProcessMemory(ptr, addr, buffer, buffer.Length, out written)))
            {
                throw new Exception("Could not write process memory: " + Marshal.GetLastWin32Error());
            }
        }

        private static IntPtr GetAddress(byte size, Process process, IntPtr ptr, IEnumerable<int> offsets, int finalOffset)
        {
            var addr = process.MainModule.BaseAddress;
            var buffer = new byte[size];
            foreach (var offset in offsets)
            {
                IntPtr read;
                if (!(ReadProcessMemory(ptr, IntPtr.Add(addr, offset), buffer, buffer.Length, out read)))
                {
                    throw new Exception("Unable to read process memory");
                }
                addr = (size == 8)
                    ? new IntPtr(BitConverter.ToInt64(buffer, 0))
                    : new IntPtr(BitConverter.ToInt32(buffer, 0));
            }
            return IntPtr.Add(addr, finalOffset);
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