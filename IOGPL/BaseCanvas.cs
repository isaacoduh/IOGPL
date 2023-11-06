using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class BaseCanvas
    {
        const int xSize = 640;
        const int ySize = 480;

        bool isTestingMode = false;

        public int xPosition, yPosition;
        int xBaseCanvasSize, yBaseCanvasSize;
        Pen pen;
        Color penColor;
        Graphics g, cursorGraphics;
        Form parentForm;

        public BaseCanvas()
        {
            xBaseCanvasSize = xSize;
            yBaseCanvasSize = ySize;
            isTestingMode = true;
        }

        public BaseCanvas(Form parentForm, Graphics g, Graphics cursorGraphics)
        {
            this.g = g;
            this.cursorGraphics = cursorGraphics;
            xBaseCanvasSize = xSize;
            yBaseCanvasSize = ySize;
            xPosition = yPosition = 20;
            pen = new Pen(Color.Black, 1);
            this.parentForm = parentForm;
        }


        public void RenderCursor()
        {
            if (isTestingMode == true)
                return;
            cursorGraphics.Clear(Color.Transparent);
            Pen p = new Pen(Color.Red, 1);
            cursorGraphics.DrawRectangle(p, xPosition - 2, yPosition - 2, 4, 4);
            parentForm.Refresh();
        }

    }
}
