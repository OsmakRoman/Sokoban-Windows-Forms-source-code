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
    public partial class Form1 : Form
    {
        public SokobanCore game;
        public List<Bitmap> digitslist;

        public Dictionary<string, Bitmap> images;
        
        public Form1()
        {
            InitializeComponent();

            game = new SokobanCore();
            game.EndOfLevelEvent += EndOfLevelEventHandler;

            digitslist = new List<Bitmap>
            {
                Properties.Resources._0,
                Properties.Resources._1,
                Properties.Resources._2,
                Properties.Resources._3,
                Properties.Resources._4,
                Properties.Resources._5,
                Properties.Resources._6,
                Properties.Resources._7,
                Properties.Resources._8,
                Properties.Resources._9

            };
            foreach (Bitmap item in digitslist)
            {
                item.MakeTransparent(Color.White);
            }

            images = new Dictionary<string, Bitmap>
            {
                ["up"] = Properties.Resources.up,
                ["left"] = Properties.Resources.left,
                ["right"] = Properties.Resources.right,
                ["down"] = Properties.Resources.down,
                ["choose"] = Properties.Resources.choose,
                ["undo"] = Properties.Resources.undo,
                ["reset"] = Properties.Resources.reset,
                ["logo"] = Properties.Resources.logo,
                ["level"] = Properties.Resources.level,
                ["ok"] = Properties.Resources.ok,
                ["cancel"] = Properties.Resources.cancel,
                ["reset_level"] = Properties.Resources.reset_level,
                ["yes"] = Properties.Resources.yes,
                ["no"] = Properties.Resources.no,
                ["congratulations"] = Properties.Resources.congratulations,
                ["completed"] = Properties.Resources.completed
            };

            foreach (Bitmap item in images.Values)
            {
                item.MakeTransparent(Color.White);
            }

        
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;           

            grfx.DrawImage(images["logo"], 598, 29);

            grfx.DrawImage(images["level"], 626, 152);

            grfx.DrawImage(digitslist[game.GetCurrentLevel() % 10], 674, 152);
            grfx.DrawImage(digitslist[game.GetCurrentLevel()/10], 667, 152);

            grfx.DrawRectangle(Pens.Black, 10, 10, 580, 400);

            int n= (int)(29 - game.Play.SizeX) / 2;
            int m = (int)(20 - game.Play.SizeY) / 2;

            for (int i = 0; i <= game.Play.SizeY - 1; i++)
                for (int j = 0; j <= game.Play.SizeX - 1; j++)
                {
                    Rectangle rect = new Rectangle(10 + n * 20 + j * 20, 10 + m * 20 + i * 20, 20, 20);

                    switch (game.Play.Blocks[i * game.Play.SizeX + j])
                    {
                        case '@':
                            grfx.FillRectangle(Brushes.Yellow, rect);
                            break;
                        case 'X':
                            grfx.FillRectangle(Brushes.Black, rect);
                            break;
                        case '*':
                            grfx.FillRectangle(Brushes.Red, rect);
                            break;
                        case '.':
                            grfx.FillRectangle(Brushes.LightGray, rect);
                            break;
                        case '&':
                            grfx.FillRectangle(Brushes.LimeGreen, rect);
                            break;
                        case ' ':
                            break;
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (game.TryMoveUp())
                Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (game.TryMoveLeft())
                Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (game.TryMoveRight())
                Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (game.TryMoveDown())
                Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.trackBar1.Value = game.GetCurrentLevel();
            form3.Owner = this;
            form3.ShowDialog();

            if (form3.DialogResult == DialogResult.OK)
            {
                game.SetLevel(form3.trackBar1.Value);
                Invalidate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (game.MoveUndo())
                Invalidate();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.ShowDialog();
            if (form2.DialogResult == DialogResult.OK)
            {
                game.ResetLevel();
                Invalidate();
            }
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["up"], 2, 2);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["left"], 2, 2);
        }

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["right"], 2, 2);
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["down"], 2, 2);
        }

        private void button5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["choose"], 16, 6);
        }

        private void button6_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["undo"], 24, 6);
        }

        private void button7_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(images["reset"], 21, 6);
        }

        private void EndOfLevelEventHandler()
        {          
            Invalidate();
            Form4 form4 = new Form4();
            form4.Owner = this;
            form4.ShowDialog();
            game.SetLevel (game.GetCurrentLevel() + 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    button1.PerformClick();
                    break;
                case Keys.Left:
                    button2.PerformClick();
                    break;
                case Keys.Right:
                    button3.PerformClick();
                    break;
                case Keys.Down:
                    button4.PerformClick();
                    break;
            }

        }
    }
    
}
