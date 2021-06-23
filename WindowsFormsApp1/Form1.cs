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
        static System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessesByName("rust").FirstOrDefault();
        RustLegacyAPI.API api = new RustLegacyAPI.API(p);
        public Form1()
        {
            InitializeComponent();
        }
        private static IKeyboardMouseEvents m_GlobalHook;
        private void Form1_Load(object sender, EventArgs e)
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
        }
        float temp = 0;
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (temp == 0)
                {
                    temp = api.Y_Position;
                }
                
                Console.WriteLine("Позоция: 0x" + temp);
                api.Y_Position = temp;
                Thread.Sleep(50);
            }

        }
        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                temp = 0;
            }
        }
    }
}
