using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public partial class Form1 : Form
    {
        // Default cursor position
        //private int defaultX = 5;
        //private int defaultY = 5;
        //private Bitmap bitmap;
        //private Graphics graphics;

        const bool Execute = true;
        const bool DONT_EXECUTE = false;

        Bitmap outputBitmap = new Bitmap(640, 480);
        Bitmap cursorBitmap = new Bitmap(640, 480);

        Graphics bmG;
        BaseCanvas canvas;

        CommandParser parser;
        Color backgroundColor = Color.DarkGray;
        
        private int defaultX = 5;
        private int defaultY = 5;

        //private CommandProcessor processor = new CommandProcessor();/**/


        public Form1()
        {
            InitializeComponent();
            bmG = Graphics.FromImage(outputBitmap);
            canvas = new BaseCanvas(this, Graphics.FromImage(outputBitmap), Graphics.FromImage(cursorBitmap));
            parser = new CommandParser(canvas);
            bmG.Clear(backgroundColor);

            /*Command m = new MoveTo(pBox, defaultX, defaultY);
            m.Execute();*/
            
            /*ICommand moveToCommand = new MoveToCommand(pBox, defaultX, defaultY);
            processor.SetCommand(moveToCommand);
            processor.ExecuteCommand();*/


        }

        private void cmdTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    string inputCommand = cmdTxtBox.Text.Trim();
                    CommandParser parser = new CommandParser();
                    parser.ParseCommand(inputCommand);
                    string action = parser.Action;
                    string[] tokens = parser.Tokens;

                   


                    if (parser.Action.Equals("drawTo"))
                    {
                        Command c = new DrawTo(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("moveTo"))
                    {
                        Command c = new MoveTo(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("clear"))
                    {
                        Command c = new Clear(canvas);
                        c.Execute();
                    } else if (parser.Action.Equals("reset"))
                    {
                        Command c = new Reset(canvas);
                        c.Execute();
                    } else if (parser.Action.Equals("circle"))
                    {
                        Command c = new Circle(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("rect"))
                    {
                        Command c = new Rect(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("square"))
                    {
                        Command c = new Square(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("tri"))
                    {
                        Command c = new Triangle(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("pen"))
                    {
                        Command c = new PenCommand(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("fill"))
                    {
                        Command c = new FillCommand(canvas, action, tokens);
                        c.Execute();
                    }
                    else
                    {
                        throw new InvalidCommandException("Invalid argument entered");
                    }

                } catch (Exception ex)
                {

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void pBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(outputBitmap, 0, 0);
            g.DrawImageUnscaled(cursorBitmap, 0, 0);
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            if(rTextBox != null)
            {
                CommandParser parser = new CommandParser();
               

                foreach(string line in rTextBox.Lines)
                {
                    parser.ParseCommand(line.Trim());
                    string action = parser.Action;
                    string[] tokens = parser.Tokens;

                    if (parser.Action.Equals("drawTo"))
                    {
                        Command c = new DrawTo(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("moveTo"))
                    {
                        Command c = new MoveTo(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("clear"))
                    {
                        Command c = new Clear(canvas);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("reset"))
                    {
                        Command c = new Reset(canvas);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("circle"))
                    {
                        Command c = new Circle(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("rect"))
                    {
                        Command c = new Rect(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("square"))
                    {
                        Command c = new Square(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("tri"))
                    {
                        Command c = new Triangle(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("pen"))
                    {
                        Command c = new PenCommand(canvas, action, tokens);
                        c.Execute();
                    }
                    else if (parser.Action.Equals("fill"))
                    {
                        Command c = new FillCommand(canvas, action, tokens);
                        c.Execute();
                    }
                    else
                    {
                        throw new InvalidCommandException("Invalid argument entered");
                    }
                }
                
            }
        }
    }
}
