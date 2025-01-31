using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FFXIVZoomHack
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
        public static extern nint OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(nint hObject);

        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(nint hProcess, nint lpBaseAddress, [In, Out] byte[] buffer, nint size, out nint lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(nint hProcess, nint lpBaseAddress, [In, Out] byte[] buffer, nint size, out nint lpNumberOfBytesWritten);
    }

    public class ProcessMemoryReader
    {
        public nint pBytesRead = 0;
        public nint pBytesWritten = 0;
        public Process process { get; set; }
        public nint handle;
        public nint pBaseAddress;
        private nint pEndAddress;
        private nint ModuleSize;

        public ProcessMemoryReader(int Pid)
        {
            process = Process.GetProcessById(Pid);
            if (process == null) throw new ArgumentNullException("Process Not Found ");
            else OpenProcess();
        }

        public ProcessMemoryReader(string pName)
        {
            process = Process.GetProcessesByName(pName).ToList().FirstOrDefault();
            if (process == null) throw new ArgumentNullException("Process Not Found");
            else OpenProcess();
        }

        public bool FindProcess(string name)
        {
            process = Process.GetProcessesByName(name).ToList().FirstOrDefault();
            return (process != null) ? true : false;
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
            pEndAddress = pBaseAddress + process.MainModule.ModuleMemorySize;
            ModuleSize = pEndAddress - pBaseAddress;
        }

        public void CloseHandle()
        {
            int returnValue = Win32MemoryApi.CloseHandle(handle);
            if (returnValue == 0)
            {
                throw new Exception("Closing handle failed.");
            }
        }

        public nint ReadPtr_x64(nint[] PointerChain)
        {
            bool isBase = true;
            nint pBytesRead;
            byte[] buffer = new byte[8];
            nint Pointer = 0;
            foreach (nint Chain in PointerChain)
            {
                if (isBase)
                {
                    _ = Win32MemoryApi.ReadProcessMemory(handle, process.Modules[0].BaseAddress + Chain, buffer, 8, out pBytesRead);
                    Pointer = (nint)BitConverter.ToInt64(buffer);
                    isBase = false;
                }
                else
                {
                    _ = Win32MemoryApi.ReadProcessMemory(handle, Pointer + Chain, buffer, 8, out pBytesRead);
                    Pointer = (nint)BitConverter.ToInt64(buffer);
                }
            }
            return Pointer;
        }

        public byte[] ReadByteArray(nint memoryAddress, nint length)
        {
            byte[] buffer = new byte[length];
            _ = Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, length, out pBytesRead);
            return buffer;
        }

        public short ReadInt16(nint memoryAddress)
        {
            byte[] buffer = new byte[2];
            _ = Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 2, out pBytesRead);
            short i = BitConverter.ToInt16(buffer, 0);
            return i;
        }

        public int ReadInt32(nint memoryAddress)
        {
            byte[] buffer = new byte[4];
            _ = Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 4, out pBytesRead);
            int i = BitConverter.ToInt32(buffer, 0);
            return i;
        }

        public long ReadInt64(nint memoryAddress)
        {
            byte[] buffer = new byte[8];
            _ = Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 8, out pBytesRead);
            long i = BitConverter.ToInt64(buffer, 0);
            return i;
        }

        public float ReadSingle(nint memoryAddress)
        {
            byte[] buffer = new byte[4];
            _ = Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 4, out pBytesRead);
            float i = BitConverter.ToSingle(buffer, 0);
            return i;
        }

        public double ReadDouble(nint memoryAddress)
        {
            byte[] buffer = new byte[8];
            _ = Win32MemoryApi.ReadProcessMemory(handle, memoryAddress, buffer, 8, out pBytesRead);
            double i = BitConverter.ToDouble(buffer, 0);
            return i;
        }

        public void WriteToPtr_x64(long[] PointerChain, byte[] Data)
        {
            nint pBytesWritten;
            nint pBytesRead;
            byte[] buffer = new byte[8];
            bool isbase = true;
            nint Pointer = 0;
            foreach (nint Chain in PointerChain)
            {
                if (isbase)
                {
                    _ = Win32MemoryApi.ReadProcessMemory(handle, process.Modules[0].BaseAddress + Chain, buffer, 8, out pBytesRead);
                    Pointer = (nint)BitConverter.ToInt64(buffer, 0);
                    isbase = false;
                }
                else
                {
                    _ = Win32MemoryApi.ReadProcessMemory(handle, (nint)(Pointer + Chain), buffer, 8, out pBytesRead);
                    Pointer = (nint)BitConverter.ToInt64(buffer, 0);
                }
            }
            _ = Win32MemoryApi.WriteProcessMemory(handle, Pointer, Data, Data.Length, out pBytesWritten);
        }

        public void WriteByteArray(nint memoryAddress, byte[] bytesToWrite)
        {
            pBytesWritten = nint.Zero;
            _ = Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, bytesToWrite, bytesToWrite.Length, out pBytesWritten);
        }

        public void WriteInt16(nint memoryAddress, short i)
        {
            pBytesWritten = nint.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            _ = Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 2, out pBytesWritten);
        }

        public void WriteInt32(nint memoryAddress, int i)
        {
            pBytesWritten = nint.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            _ = Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 4, out pBytesWritten);
        }

        public void WriteInt64(nint memoryAddress, long i)
        {
            pBytesWritten = nint.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            _ = Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 8, out pBytesWritten);
        }

        public void WriteSingle(nint memoryAddress, float i)
        {
            pBytesWritten = nint.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            _ = Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 4, out pBytesWritten);
        }

        public void WriteDouble(nint memoryAddress, double i)
        {
            pBytesWritten = nint.Zero;
            byte[] buffer = BitConverter.GetBytes(i);
            _ = Win32MemoryApi.WriteProcessMemory(handle, memoryAddress, buffer, 8, out pBytesWritten);
        }

        public List<nint> ScanPtrBySig(string pattern = "")
        {
            byte?[] array = pattern2SigArray(pattern);
            List<nint> list = new List<nint>();
            if (pattern == null || pattern.Length % 2 != 0)
            {
                return new List<nint>();
            }
            byte[] ModuleCopy = ReadByteArray(pBaseAddress, ModuleSize);
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
                    nint intPtr = new nint(BitConverter.ToInt32(ModuleCopy, i + array.Length));
                    nint num2 = pBaseAddress + i + array.Length + 4 + intPtr;
                    intPtr = new nint(num2 - (long)process.MainModule.BaseAddress);
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