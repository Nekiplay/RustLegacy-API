using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessesByName("rust").FirstOrDefault();
            RustLegacyAPI.API api = new RustLegacyAPI.API(p);
            //Console.WriteLine("Здововье: " + api.Health);
            //Console.WriteLine("Еды: " + api.Food);
            Console.WriteLine("Y позиция: " + api.Y_Position);
            Console.ReadKey();
        }
    }
}
