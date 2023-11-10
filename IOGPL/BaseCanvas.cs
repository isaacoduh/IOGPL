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

        public bool isTestingMode = false;

        public int xPosition, yPosition;
        public int xBaseCanvasSize, yBaseCanvasSize;
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
            pen = new Pen(penColor, 1);
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

        public void MoveTo(int x, int y)
        {
            xPosition = x;
            yPosition = y;

            if(isTestingMode == false)
            {
                RenderCursor();
            }
        }

        public void DrawTo(int tX, int tY)
        {
            // TODO: Implement some check and throw exceptions
            if (g != null)
                g.DrawLine(pen, xPosition, yPosition, tX, tY);
            xPosition = tX;
            yPosition = tY;
            if(isTestingMode == false)
            {
                RenderCursor();
            }
        }

        public void Clear()
        {
            if(isTestingMode == true)
            {
                return;
            }

            if (g != null)
            {
                g.Clear(Color.Transparent);
            }

            RenderCursor();
        }

        public void Reset()
        {
            if (isTestingMode == true)
                return;

            if (g != null)
                g.Clear(Color.Transparent);
            xPosition = 25;
            yPosition=25;

            RenderCursor();

        }

        public void Circle(int radius)
        {
            if (radius < 0)
            {
                throw new GPLException("invalid circle size");
            }

            if (g != null)
            {
                g.DrawEllipse(pen, xPosition - radius, yPosition - radius, radius * 2, radius * 2);
            }
            
            RenderCursor();
        }

        public void Rectangle(int width, int  height)
        {
            if(width < 0 || height < 0) 
            {

                throw new GPLException("invalid rectangle size");
            }

            if(g!= null)
            {
                g.DrawRectangle(pen, xPosition - (width/2) , yPosition - (width/2), width, height);
            }

            RenderCursor();
        }

        public void Square(int size)
        {
            if(g != null)
            {
                g.DrawRectangle(pen, xPosition, yPosition, xPosition + size, yPosition + size);
            }

            RenderCursor();
        }

        public void Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if(g != null)
            {
                Point[] points = {new Point(x1,y1), new Point(x2,y2), new Point(x3,y3)};
                g.DrawPolygon(pen, points);
            }

            RenderCursor();
        }

        public void SetPenColor(string color)
        {
            if(isTestingMode == false)
            {
                Color c =  Color.FromName(color);
                if(c != null)
                {
                    this.pen.Color = c;
                }
            }
                
        }

    }
}
