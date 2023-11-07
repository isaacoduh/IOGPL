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

                    Console.WriteLine($"action {action}");
                    foreach(string i in tokens)
                    {
                        Console.WriteLine(i);
                    }

                    
                    if (parser.Action.Equals("drawTo"))
                    {
                        Command c = new DrawTo(canvas, action, tokens);
                        c.Execute();
                    } else if (parser.Action.Equals("moveTo"))
                    {
                        Command c = new MoveTo(canvas, action, tokens);
                        c.Execute();
                    }

                } catch (Exception ex)
                {

                }
            }
            /*if(e.KeyChar ==(char)Keys.Enter)
            
            {
                
                if (cmdTxtBox.Text == "run")
                {
                    Console.WriteLine("Run Command Called");
                    foreach(string input in rTextBox.Lines)
                    {
                        Console.WriteLine($"{input} \n");
                        CommandParser parser = new CommandParser();
                        parser.Parse(input);

                        string action = parser.Action;
                        string[] tokens = parser.Tokens;

                        switch (action)
                        {
                            case "moveto":
                                if (tokens.Length == 2 && int.TryParse(tokens[0], out int x) && int.TryParse(tokens[1], out int y))
                                {
                                    var moveToCmd = new MoveTo(pBox, x, y);
                                    moveToCmd.Execute();
                                }
                                break;
                            case "drawto":
                                if (tokens.Length == 2
                                    && int.TryParse(tokens[0], out int endx)
                                    && int.TryParse(tokens[1], out int endy)

                                 )
                                {
                                    var drawToCmd = new DrawTo(pBox, defaultX, defaultY, endx, endy);
                                    drawToCmd.Execute();

                                }
                                break;
                                // Handle other command cases
                        }
                    }
                } else
                {
                    string inputCommand = cmdTxtBox.Text;
                    CommandParser parser = new CommandParser();
                    parser.Parse(inputCommand);

                    string action = parser.Action;
                    string[] tokens = parser.Tokens;

                    switch (action)
                    {
                        case "moveto":
                            if (tokens.Length == 2 && int.TryParse(tokens[0], out int x) && int.TryParse(tokens[1], out int y))
                            {
                                defaultX = x;
                                defaultY = y;
                                var moveToCmd = new MoveTo(pBox, x, y);
                                moveToCmd.Execute();
                            }
                        break;
                        case "drawto":
                            if(tokens.Length == 2
                                && int.TryParse(tokens[0], out int endx)
                                && int.TryParse(tokens[1], out int endy)
                                
                             )
                            {
                                var drawToCmd = new DrawTo(pBox, defaultX, defaultY, endx, endy);
                                drawToCmd.Execute();

                            }
                        break;
                            // Handle other command cases
                    }
                }
                

                
            }*/
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
    }
}
