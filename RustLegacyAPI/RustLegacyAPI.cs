using Process.NET;
using Process.NET.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RustLegacyAPI
{
    public class API
    {
        private System.Diagnostics.Process process;
        private bool debug = false;

        public API(System.Diagnostics.Process process)
        {
            this.process = process;

            Console.WriteLine("Адрес игры: 0x" + Offsets.GameHandle.ToString("X"));
        }
        public API(System.Diagnostics.Process process, bool debug)
        {
            this.process = process;
            this.debug = debug;

            Console.WriteLine("Адрес игры: 0x" + Offsets.GameHandle.ToString("X"));
        }
        public float Y_Position
        {
            get
            {

                var processSharp = new ProcessSharp(process, MemoryType.Remote);
                IMemory memory = processSharp.Memory;
                IntPtr clientdll = Offsets.GameHandle;
                IntPtr read = memory.Read<IntPtr>(clientdll);
                clientdll = IntPtr.Subtract(read, 0x6545618D);
                if (debug)
                {
                    Console.WriteLine("Базовый адресс Y позиций: 0x" + clientdll.ToString("X"));
                }
                IntPtr addres = ReadOffset(memory, clientdll, Offsets.Y_Position);
                if (addres != IntPtr.Zero)
                {
                    if (debug)
                    {
                        Console.WriteLine("Готовый адресс Y позиций: 0x" + addres.ToString("X"));
                    }
                    float health = memory.Read<float>(addres);
                    return health;
                }
                else { return 0; }

            }
            set
            {
                var processSharp = new ProcessSharp(process, MemoryType.Remote);
                IMemory memory = processSharp.Memory;
                IntPtr clientdll = Offsets.GameHandle;
                IntPtr read = memory.Read<IntPtr>(clientdll);
                clientdll = IntPtr.Subtract(read, 0x6545618D);
                if (debug)
                {
                    Console.WriteLine("Базовый адресс Y позиций: 0x" + clientdll.ToString("X"));
                }
                IntPtr addres = ReadOffset(memory, clientdll, Offsets.Y_Position);
                if (addres != IntPtr.Zero)
                {
                    if (debug)
                    {
                        Console.WriteLine("Готовый адресс Y позиций: 0x" + addres.ToString("X"));
                    }
                    memory.Write<float>(addres, value);
                }
                else { }
            }
        }
        public IntPtr ReadOffset(IMemory memory, IntPtr address, int[] offset)
        {
            try
            {


                for (int i = 0; i < offset.Count() - 1; i++)
                {
                    address = memory.Read<IntPtr>(address + offset[i]);
                    Console.WriteLine("0x" + address.ToString("X"));
                }
                address += offset[offset.Count() - 1];
                return address;
            }
            catch { return IntPtr.Zero; }
        }
    }

    public static class Offsets
    {
        public static IntPtr getModuleAdress(string modulname, System.Diagnostics.Process proc)
        {
            IntPtr result = IntPtr.Zero;
            for (int i = 0; i < proc.Modules.Count; i++)
            {
                if (proc.Modules[i].ModuleName == modulname)
                {
                    result = proc.Modules[i].BaseAddress;
                    Console.WriteLine(result.ToString("X"));
                    break;
                }
            }
            return result;
        }
        public static IntPtr GameHandle = new IntPtr(0x905A4D);

         public static IntPtr PlayerPosition = new IntPtr(0x037B8838);

        public static int[] Y_Position = new[] { 0x20, 0x28, 0x5C, 0x18, 0x40, 0xC, 0xB4 };
    }
}
