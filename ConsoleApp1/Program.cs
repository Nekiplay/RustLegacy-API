using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessesByName("rust").FirstOrDefault();
            RustLegacyAPI.API api = new RustLegacyAPI.API(p);
            Task.Factory.StartNew(() =>
            {
                float temp = api.Y_Position;
                while (true)
                {
                    api.Y_Position = temp;
                    Thread.Sleep(25);
                }
            });
            Console.WriteLine("Y позиция: " + api.Y_Position);
            Console.ReadKey();
        }
    }
}
