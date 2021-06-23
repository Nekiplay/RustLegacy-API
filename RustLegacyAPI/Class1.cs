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
        System.Diagnostics.Process process;
        public API(System.Diagnostics.Process process)
        {
            this.process = process;
        }
        public float Health
        {
            get
            {
                var processSharp = new ProcessSharp(process, MemoryType.Remote);
                IMemory memory = processSharp.Memory;
                IntPtr clientdll = Offsets.PlayerModule;
                //Console.WriteLine("Базовый адресс: 0x" + clientdll.ToString("X"));
                IntPtr addres = ReadOffset(memory, clientdll, Offsets.Health);
                //Console.WriteLine("Адресс здоровья: 0x" + addres.ToString("X"));
                float health = memory.Read<float>(addres);
                return health;
            }
        }
        public float Food
        {
            get
            {
                var processSharp = new ProcessSharp(process, MemoryType.Remote);
                IMemory memory = processSharp.Memory;
                IntPtr clientdll = Offsets.PlayerModule;
                //Console.WriteLine("Базовый адресс: 0x" + clientdll.ToString("X"));
                IntPtr addres = ReadOffset(memory, clientdll, Offsets.Food);
                //Console.WriteLine("Адресс здоровья: 0x" + addres.ToString("X"));
                float health = memory.Read<float>(addres);
                return health;
            }
        }
        public float Y_Position
        {
            get
            {
                var processSharp = new ProcessSharp(process, MemoryType.Remote);
                IMemory memory = processSharp.Memory;
                IntPtr clientdll = Offsets.PlayerPosition;
                Console.WriteLine("Базовый адресс: 0x" + clientdll.ToString("X"));
                IntPtr addres = ReadOffset(memory, clientdll, Offsets.Y_Position);
                float health = memory.Read<float>(addres);
                return health;
            }
            set
            {
                var processSharp = new ProcessSharp(process, MemoryType.Remote);
                IMemory memory = processSharp.Memory;
                IntPtr clientdll = Offsets.PlayerPosition;
                IntPtr addres = ReadOffset(memory, clientdll, Offsets.Y_Position);
                memory.Write<float>(addres, value);
            }
        }
        public IntPtr ReadOffset(IMemory memory, IntPtr address, int[] offset)
        {
            for (int i = 0; i < offset.Count() - 1; i++)
            {
                address = memory.Read<IntPtr>(address + offset[i]);
                Console.WriteLine("0x" + address.ToString("X"));
            }
            address += offset[offset.Count() - 1];
            return address;
        }
        public IntPtr ReadOffset(IMemory memory, IntPtr address, int offset)
        {
            IntPtr v0 = memory.Read<IntPtr>(address + offset);
            return v0;
        }
    }

    public static class Offsets
    {
        public static IntPtr PlayerModule = new IntPtr(0x448D36E8);
        public static int[] Health = new[] { 0x38, 0x8, 0x50, 0x250, 0x8, 0x24, 0x94 };
        public static int[] Food = new[] { 0x38, 0x8, 0x48, 0x8, 0x554, 0x34, 0x1F4 };

         public static IntPtr PlayerPosition = new IntPtr(0x036C2880);

        public static int[] Y_Position = new[] { 0x8, 0x0, 0x10, 0x4, 0x0, 0x0, 0x4C0 };
    }
}
