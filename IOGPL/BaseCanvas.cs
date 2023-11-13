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

        public Color defaultPenColor = Color.Black;

        public bool isTestingMode = false;
        public bool fillStatus = false;

        public int xPosition, yPosition;
        public int xBaseCanvasSize, yBaseCanvasSize;
        SolidBrush fillBrush;
        Pen pen;
        public Color penColor;
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
            SetupPen(defaultPenColor);
            fillBrush = new SolidBrush(Color.Black);
            this.parentForm = parentForm;

        }

        public void SetupPen(Color color)
        {
            penColor = color;
            pen = new Pen(color, 1);

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


        /// <summary>
        /// Moves the cursor to the specified coordinates.
        /// </summary>
        /// <param name="x">The new X-coordinate for the cursor.</param>
        /// <param name="y">The new Y-coordinate for the cursor.</param>
        /// <remarks>
        /// This method sets the cursor position to the specified coordinates (x, y).
        /// If the application is not in testing mode, it renders the cursor on the screen.
        /// </remarks>
        /// <param name="x">The new X-coordinate for the cursor.</param>
        /// <param name="y">The new Y-coordinate for the cursor.</param>
        public void MoveTo(int x, int y)
        {
            xPosition = x;
            yPosition = y;

            if(isTestingMode == false)
            {
                RenderCursor();
            }
        }


        /// <summary>
        /// Draws a line from the current cursor position to the specified target coordinates.
        /// </summary>
        /// <param name="tX">The target X-coordinate for the line.</param>
        /// <param name="tY">The target Y-coordinate for the line.</param>
        /// <remarks>
        /// This method draws a line from the current cursor position to the specified target coordinates (tX, tY).
        /// If the application is not in testing mode and a graphics object is available, it uses the graphics object
        /// to draw the line. It then updates the cursor position to the target coordinates.
        /// Finally, if not in testing mode, it renders the cursor on the screen.
        /// </remarks>
        /// <param name="tX">The target X-coordinate for the line.</param>
        /// <param name="tY">The target Y-coordinate for the line.</param>
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


        /// <summary>
        /// Clears the canvas.
        /// </summary>
        /// <remarks>
        /// This method clears the canvas by setting its content to transparent.
        /// If the application is in testing mode, the canvas is not cleared.
        /// After clearing the canvas, it renders the cursor on the screen.
        /// </remarks>
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


        /// <summary>
        /// Resets the canvas and cursor position to their initial state.
        /// </summary>
        /// <remarks>
        /// This method resets the canvas by clearing it and sets the cursor position to the origin (0, 0).
        /// If the application is in testing mode, the canvas is not reset.
        /// After resetting the canvas and cursor position, it renders the cursor on the screen.
        /// </remarks>
        public void Reset()
        {
            if (isTestingMode == true)
                return;

            if (g != null)
                g.Clear(Color.Transparent);
            xPosition = 0;
            yPosition=0;

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
                if (fillStatus)
                {
                    g.FillEllipse(fillBrush, xPosition - radius, yPosition - radius, radius * 2, radius * 2);
                }
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
                if (fillStatus)
                {
                    g.FillRectangle(fillBrush, xPosition - (width / 2), yPosition - (width / 2), width, height);
                }
                g.DrawRectangle(pen, xPosition - (width/2) , yPosition - (width/2), width, height);
            }

            RenderCursor();
        }

        public void Square(int size)
        {
            if(g != null)
            {
                if (fillStatus)
                {
                    g.FillRectangle(fillBrush, xPosition, yPosition, xPosition + size, yPosition + size);
                }
                g.DrawRectangle(pen, xPosition, yPosition, xPosition + size, yPosition + size);
            }

            RenderCursor();
        }

        public void Triangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            if(g != null)
            {
                Point[] points = {new Point(x1,y1), new Point(x2,y2), new Point(x3,y3)};
                if (fillStatus)
                {
                    g.FillPolygon(fillBrush, points);
                }
                g.DrawPolygon(pen, points);
            }

            RenderCursor();
        }

        /// <summary>
        /// Sets the color of the pen used for drawing.
        /// </summary>
        /// <param name="color">The name of the color to set.</param>
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


        public void SetFillStatus()
        {
            this.fillStatus = !fillStatus;
            if (fillStatus)
            {
                this.fillBrush = new SolidBrush(penColor);
            }
        }

    }
}
