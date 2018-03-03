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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["ok"], 33, 6);
        }

        private void Form4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["congratulations"], 41, 21);

            e.Graphics.DrawImage((Owner as Form1).images["level"], 36, 39);

            e.Graphics.DrawImage((Owner as Form1).images["completed"], 95, 39);

            e.Graphics.DrawImage((Owner as Form1).digitslist[(Owner as Form1).game.GetCurrentLevel() / 10], 76, 39);
            e.Graphics.DrawImage((Owner as Form1).digitslist[(Owner as Form1).game.GetCurrentLevel() % 10], 83, 39);
        }
    }
}
