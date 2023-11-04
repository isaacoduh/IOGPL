using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class DrawTo : Command
    {
        private PictureBox pictureBox;
        private int startx, starty;
        private int endx, endy;

        public DrawTo(PictureBox pictureBox, int startx, int starty, int endx, int endy) : base(pictureBox)
        {
            this.pictureBox = pictureBox;
            this.startx = startx;
            this.starty = starty;
            this.endx = endx;
            this.endy = endy;
        }

        public override void Execute()
        {
            
            using (Graphics g = pictureBox.CreateGraphics())
            {
                using (Pen p = new Pen(Color.Red))
                {
                    g.DrawLine(p, startx, starty, endx, endy);
                }
            }
        }
    }
}
