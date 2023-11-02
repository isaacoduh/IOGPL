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
        private int m_x;
        private int m_y;

        public MoveTo(PictureBox pictureBox, int x, int y) : base(pictureBox)
        {
            m_x = x;
            m_y = y;
        }

        public override void Execute()
        {
            setCurrentPosition(m_x, m_y);
        }

    }
}
