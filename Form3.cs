using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban_Windows_Forms
{
    public partial class Form3 : Form
    {
        
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["ok"], 33, 6);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["cancel"], 17, 6);
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["level"], 390 , 22);

            e.Graphics.DrawImage((Owner as Form1).digitslist[trackBar1.Value/10], 430, 22);
            e.Graphics.DrawImage((Owner as Form1).digitslist[trackBar1.Value % 10], 437, 22);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
