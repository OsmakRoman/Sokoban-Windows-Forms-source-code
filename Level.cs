using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Windows_Forms
{
    public class Level
    { 
        public int SizeX { get; }
        public int SizeY { get; }
        public int PusherX { get; set; }
        public int PusherY { get; set; }
        public string Blocks { get; set; }

        public Level(int sx, int sy, int px, int py, string bl)
        {
            SizeX = sx;
            SizeY = sy;
            PusherX = px;
            PusherY = py;
            Blocks = bl;
        }

        public Level (Level obj)
        {
            this.SizeX = obj.SizeX;
            this.SizeY = obj.SizeY;
            this.PusherX = obj.PusherX;
            this.PusherY = obj.PusherY;
            this.Blocks = obj.Blocks;
        }

      


    }
}
