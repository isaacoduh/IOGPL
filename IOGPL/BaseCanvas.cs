﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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



        /// <summary>
        /// Draws a circle on the canvas with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle to be drawn.</param>
        /// <remarks>
        /// This method draws a circle on the canvas with the specified radius. 
        /// If the radius is less than 0, it throws a <see cref="GPLException"/> with the message "invalid circle size."
        /// If the application is in testing mode or no graphics object is available, the circle is not drawn.
        /// After drawing the circle, it renders the cursor on the screen.
        /// </remarks>
        /// <param name="radius">The radius of the circle to be drawn.</param>
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


        /// <summary>
        /// Draws a rectangle on the canvas with the specified width and height.
        /// </summary>
        /// <param name="width">The width of the rectangle to be drawn.</param>
        /// <param name="height">The height of the rectangle to be drawn.</param>
        /// <remarks>
        /// This method draws a rectangle on the canvas with the specified width and height.
        /// If either the width or height is less than 0, it throws a <see cref="GPLException"/> with the message "invalid rectangle size."
        /// If the application is in testing mode or no graphics object is available, the rectangle is not drawn.
        /// After drawing the rectangle, it renders the cursor on the screen.
        /// </remarks>
        /// <param name="width">The width of the rectangle to be drawn.</param>
        /// <param name="height">The height of the rectangle to be drawn.</param>
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

        /// <summary>
        /// Draws a triangle on the canvas using the specified vertex coordinates.
        /// </summary>
        /// <param name="x1">The x-coordinate of the first vertex.</param>
        /// <param name="y1">The y-coordinate of the first vertex.</param>
        /// <param name="x2">The x-coordinate of the second vertex.</param>
        /// <param name="y2">The y-coordinate of the second vertex.</param>
        /// <param name="x3">The x-coordinate of the third vertex.</param>
        /// <param name="y3">The y-coordinate of the third vertex.</param>
        /// <remarks>
        /// This method draws a triangle on the canvas using the specified vertex coordinates.
        /// If the application is in testing mode or no graphics object is available, the triangle is not drawn.
        /// If fillStatus is true, the triangle is filled; otherwise, only the triangle outline is drawn.
        /// After drawing the triangle, it renders the cursor on the screen.
        /// </remarks>
        /// <param name="x1">The x-coordinate of the first vertex.</param>
        /// <param name="y1">The y-coordinate of the first vertex.</param>
        /// <param name="x2">The x-coordinate of the second vertex.</param>
        /// <param name="y2">The y-coordinate of the second vertex.</param>
        /// <param name="x3">The x-coordinate of the third vertex.</param>
        /// <param name="y3">The y-coordinate of the third vertex.</param>
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
        /// Sets the color of the pen used for drawing.!
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

        public void CreateIcon(Icon icon, int x,  int y)
        {
            g.DrawIcon(icon, x, y);
            RenderCursor();
        }

        public void DrawRotatedRectangle(int x, int y, int width, int height, float angle)
        {
            if(isTestingMode == false)
            {
                Rectangle rectangle = new Rectangle(x,y,width,height);
                using(Matrix matrix = new Matrix())
                {
                    matrix.RotateAt(angle, new Point(x + width / 2, y + height / 2));
                    g.Transform = matrix;

                    // Draw the rotated rectangle
                    g.DrawRectangle(Pens.Black, rectangle);

                    // Reset the transformation
                    g.ResetTransform();
                }
                RenderCursor();
            }
        
        }

        /// <summary>
        /// Toggles the fill status for drawing shapes.
        /// </summary>
        /// <remarks>
        /// This method toggles the fill status for drawing shapes on the canvas. 
        /// If the fill status was false, it sets it to true and initializes a new <see cref="SolidBrush"/> with the current pen color.
        /// If the fill status was true, it sets it to false.
        /// </remarks>
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
