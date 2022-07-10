using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace XIVMemoryReader
{
    public static class Win32MemoryApi
    {
        // constants information can be found in <winnt.h>
        [Flags]
        public enum ProcessAccessType
        {
            PROCESS_TERMINATE = (0x0001),
            PROCESS_CREATE_THREAD = (0x0002),
            PROCESS_SET_SESSIONID = (0x0004),
            PROCESS_VM_OPERATION = (0x0008),
            PROCESS_VM_READ = (0x0010),
            PROCESS_VM_WRITE = (0x0020),
            PROCESS_DUP_HANDLE = (0x0040),
            PROCESS_CREATE_PROCESS = (0x0080),
            PROCESS_SET_QUOTA = (0x0100),
            PROCESS_SET_INFORMATION = (0x0200),
            PROCESS_QUERY_INFORMATION = (0x0400),
            PROCESS_QUERY_LIMITED_INFORMATION = (0x1000)
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, uint size, out IntPtr lpNumberOfBytesWritten);
    }

    public class ProcessMemoryReader
    {
        public IntPtr pBytesRead = IntPtr.Zero;
        public IntPtr pBytesWritten = IntPtr.Zero;
        public Process process { get; set; }
        public IntPtr handle;
        public IntPtr pBaseAddress;
        public IntPtr pEndAddress;
        private long ModuleSize;

        public ProcessMemoryReader(int pid)
        {
            process = Process.GetProcessById(pid);
            if (process == null) throw new ArgumentNullException("Process Not Found");
            else OpenProcess();
        }

        public ProcessMemoryReader(string pName)
        {
            process = Process.GetProcessesByName(pName).ToList().FirstOrDefault();
            if (process == null) throw new ArgumentNullException("Process Not Found");
            else OpenProcess();
        }

        public static bool FindProcess(string name)
        {
            return (Process.GetProcessesByName(name).ToList().FirstOrDefault() != null) ? true : false;
        }

        private void OpenProcess()
        {
            Win32MemoryApi.ProcessAccessType access =
                Win32MemoryApi.ProcessAccessType.PROCESS_QUERY_INFORMATION |
                Win32MemoryApi.ProcessAccessType.PROCESS_VM_READ |
                Win32MemoryApi.ProcessAccessType.PROCESS_VM_WRITE |
                Win32MemoryApi.ProcessAccessType.PROCESS_VM_OPERATION;
            handle = Win32MemoryApi.OpenProcess((uint)access, 1, (uint)process.Id);
            pBaseAddress = process.MainModule.BaseAddress;
            pEndAddress = IntPtr.Add(pBaseAddress, process.MainModule.ModuleMemorySize);
            ModuleSize = (long)pEndAddress - (long)pBaseAddress;
        }

        public void CloseHandle()
        {
            int returnValue = Win32MemoryApi.CloseHandle(handle);
            if (returnValue == 0)
            {
                throw new Exception("Closing handle failed.");
            }
        }

        public long ReadPtr_x64(long[] PointerChain)
        {
            bool isbase = true;
            IntPtr pBytesRead;
            byte[] buffer = new byte[8];
            long Pointer = 0;
            foreach (long Chain in PointerChain)
            {
                if (isbase)
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)((long)process.Modules[0].BaseAddress + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer, 0);
                    isbase = false;
                }
                else
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)(Pointer + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer, 0);
                }
            }
            return Pointer;
        }

        public byte ReadByte(IntPtr Address)
        {
            byte[] buffer = new byte[1];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, 1, out pBytesRead);
            return buffer[0];
        }

        public byte[] ReadByteArray(IntPtr Address, uint length)
        {
            byte[] buffer = new byte[length];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, length, out pBytesRead);
            return buffer;
        }

        public int ReadInt16(IntPtr Address)
        {
            byte[] buffer = new byte[2];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, 2, out pBytesRead);
            short i = BitConverter.ToInt16(buffer, 0);
            return i;
        }

        public int ReadInt32(IntPtr Address)
        {
            byte[] buffer = new byte[4];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, 4, out pBytesRead);
            int i = BitConverter.ToInt32(buffer, 0);
            return i;
        }

        public long ReadInt64(IntPtr Address)
        {
            byte[] buffer = new byte[8];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, 8, out pBytesRead);
            long i = BitConverter.ToInt64(buffer, 0);
            return i;
        }

        public float ReadSingle(IntPtr Address)
        {
            byte[] buffer = new byte[4];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, 4, out pBytesRead);
            float i = BitConverter.ToSingle(buffer, 0);
            return i;
        }

        public double ReadDouble(IntPtr Address)
        {
            byte[] buffer = new byte[8];
            Win32MemoryApi.ReadProcessMemory(handle, Address, buffer, 8, out pBytesRead);
            double i = BitConverter.ToDouble(buffer, 0);
            return i;
        }

        public void WriteToPtr_x64(long[] PointerChain, byte[] Data)
        {
            IntPtr pBytesWritten;
            IntPtr pBytesRead;
            byte[] buffer = new byte[8];
            bool isBaseAddress = true;
            long Pointer = 0;
            foreach (long Chain in PointerChain)
            {
                if (isBaseAddress)
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)((long)process.Modules[0].BaseAddress + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer, 0);
                    isBaseAddress = false;
                }
                else
                {
                    Win32MemoryApi.ReadProcessMemory(handle, (IntPtr)(Pointer + Chain), buffer, 8, out pBytesRead);
                    Pointer = BitConverter.ToInt64(buffer, 0);
                }
            }
            Win32MemoryApi.WriteProcessMemory(handle, (IntPtr)Pointer, Data, (uint)Data.Length, out pBytesWritten);
        }

        public void WriteByte(IntPtr Address, byte byteToWrite)
        {
            byte[] buffer = new byte[1];
            buffer[0] = byteToWrite;
            pBytesWritten = IntPtr.Zero;
            Win32MemoryApi.WriteProcessMemory(handle, Address, buffer, 1, out pBytesWritten);
        }

        public void WriteByteArray(IntPtr Address, byte[] bytesToWrite)
        {
            pBytesWritten = IntPtr.Zero;
            Win32MemoryApi.WriteProcessMemory(handle, Address, bytesToWrite, (uint)bytesToWrite.Length, out pBytesWritten);
        }

        public void WriteInt16(IntPtr Address, short i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, Address, buffer, 2, out pBytesWritten);
        }

        public void WriteInt32(IntPtr Address, int i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, Address, buffer, 4, out pBytesWritten);
        }

        public void WriteInt64(IntPtr Address, long i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, Address, buffer, 8, out pBytesWritten);
        }

        public void WriteSingle(IntPtr Address, float i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, Address, buffer, 4, out pBytesWritten);
        }

        public void WriteDouble(IntPtr Address, double i)
        {
            pBytesWritten = IntPtr.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            Win32MemoryApi.WriteProcessMemory(handle, Address, buffer, 8, out pBytesWritten);
        }

        public List<IntPtr> ScanPtrBySig(string pattern = "")
        {
            byte?[] array = pattern2SigArray(pattern);
            List<IntPtr> list = new List<IntPtr>();
            if (pattern == null || pattern.Length % 2 != 0)
            {
                return new List<IntPtr>();
            }
            byte[] ModuleCopy = ReadByteArray(pBaseAddress, (uint)ModuleSize);
            for (int i = 0; i < ModuleCopy.Length - array.Length - 4 + 1; i++)
            {
                int num = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (!array[j].HasValue)
                    {
                        num++;
                        continue;
                    }
                    if (array[j].Value != ModuleCopy[i + j])
                    {
                        break;
                    }
                    num++;
                }
                if (num == array.Length)
                {
                    IntPtr intPtr = new IntPtr(BitConverter.ToInt32(ModuleCopy, i + array.Length));
                    long num2 = pBaseAddress.ToInt64() + i + array.Length + 4 + intPtr.ToInt64();
                    intPtr = new IntPtr(num2 - (long)process.MainModule.BaseAddress);
                    list.Add(intPtr);
                }
            }
            return list;

            byte?[] pattern2SigArray(string HexString)
            {
                byte?[] buffer = new byte?[HexString.Length / 2];
                for (int i = 0; i < HexString.Length / 2; i++)
                {
                    string text = HexString.Substring(i * 2, 2);
                    if (text == "**")
                    {
                        buffer[i] = null;
                    }
                    else
                    {
                        buffer[i] = Convert.ToByte(text, 16);
                    }
                }
                return buffer;
            }
        }
    }
}