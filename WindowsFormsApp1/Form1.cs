using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static RustLegacyAPI.API api;
        public Form1()
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessesByName("rust").FirstOrDefault();
            api = new RustLegacyAPI.API(p);
            InitializeComponent();
        }
        private static IKeyboardMouseEvents m_GlobalHook;
        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Позоция: " + api.Y_Position);
        }
    }
}
