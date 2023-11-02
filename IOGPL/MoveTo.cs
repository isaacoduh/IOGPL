using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class MoveTo : Command
    {
        private PictureBox pictureBox;
        private int m_x, m_y;

        public MoveTo(PictureBox pictureBox, int targetX, int targetY) : base(pictureBox)
        {
           this.m_x = targetX;
            this.m_y = targetY;
            this.pictureBox = pictureBox;
           
        }

        public override void Execute()
        {
           pictureBox.Image?.Dispose();
            Bitmap updatedImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(updatedImage))
            {
                // Draw the new ellipse at the target position
                g.FillEllipse(Brushes.Black, m_x, m_y, 5, 5);

            }

            pictureBox.Image = updatedImage;
        }

    }
}
