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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["yes"], 29, 6);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["no"], 33, 6);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage((Owner as Form1).images["reset_level"], 63, 25);
        }
    }
}
