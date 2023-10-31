using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Point cursorInitialPosition = new Point(5,5);
        public Form1()
        {
            InitializeComponent();
            InitializeGraphics();
        }



        /// <summary>
        /// Initializes the graphics and sets up the initial display.
        /// </summary>
        /// <remarks>
        /// This method creates a Bitmap and Graphics object for off-screen drawing
        /// to maintain the graphical state. It clears the off-screen bitmap with a
        /// white background and draws a red point (cursor) at coordinates (5, 5).
        /// The PictureBox is updated to display the off-screen bitmap, setting up the
        /// initial cursor position.
        /// </remarks>
        private void InitializeGraphics()
        {
            bitmap = new Bitmap(pBox.Width, pBox.Height);
            graphics = Graphics.FromImage(bitmap);

            graphics.FillEllipse(Brushes.Red, cursorInitialPosition.X - 2, cursorInitialPosition.Y - 2, 5, 5);
            pBox.Image = bitmap;
        }
    }
}
