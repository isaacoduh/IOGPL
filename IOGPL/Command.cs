using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    /// <summary>
    /// Abstract Class that defines a common method Execute. That 
    /// is executed by all basic command classes
    /// </summary>
    public abstract class Command
    {
        protected PictureBox m_pictureBox;
        protected int m_currentX = 1;
        protected int m_currentY = 1;
        private Graphics g;
        private Bitmap bmp;

        public Command(PictureBox pictureBox)
        {
            m_pictureBox = pictureBox;
        }

        protected void SetupPictureBox()
        {
            m_pictureBox.Paint += (sender, e) =>
            {
                using (var pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawEllipse(pen, m_currentX - 2, m_currentY - 2, 5, 5);
                }
            };
        }

        public abstract void Execute();

        protected void RerenderPictureBox()
        {
            m_pictureBox.Invalidate();
        }

        public void setCurrentPosition(int x, int y)
        {
            m_currentX = x;
            m_currentY = y; 
        }
    }
}
