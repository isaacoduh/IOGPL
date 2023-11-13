using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
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
        }

        private void cmdTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            String errors = "";
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (cmdTxtBox.Text.Equals("run"))
                {
                    if(rTextBox != null)
                    {
                        ProcessProgram(rTextBox.Lines);
                        rTextBox.Clear();
                        cmdTxtBox.Clear();
                    }
                } else
                {
                    try
                    {
                        string inputCommand = cmdTxtBox.Text.Trim();
                        CommandParser parser = new CommandParser();
                        parser.ParseCommand(inputCommand);
                        string action = parser.Action;
                        string[] tokens = parser.Tokens;
                        cmdTxtBox.Text = "";



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
                    catch (Exception ex)
                    {
                        errors += ex.Message;
                    }
                }

                if(errors != "")
                {
                    writeToScreen(errors);
                }
                //Refresh();
            }
        }


        /// <summary>
        /// Write text to screen. I.E show text on Graphics layout.
        /// </summary>
        /// <param name="text"></param>
        private void writeToScreen(String text)
        {
            bmG.Clear(Color.DarkGray);
            Font drawFont = new Font("Arial", 8);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            //Set string format
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.NoClip;

            bmG.DrawString(text, drawFont, drawBrush, 10, 10, stringFormat);
            Refresh();
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

        private void ProcessProgram(string[] program)
        {
            String errors = "";
            CommandParser parser = new CommandParser();
            foreach(string line in program)
            {
                try
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
                catch(Exception ex) {
                    errors += ex.Message;
                }
            }

            if (errors != "")
            {
                writeToScreen(errors);
            }

        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            String errors = "";
            if (rTextBox != null)
            {
                ProcessProgram(rTextBox.Lines);
                rTextBox.Clear();
                cmdTxtBox.Clear();
            }
            if(errors != null)
            {
                writeToScreen(errors);
            }
        }

        private void SaveProgramToFile(string fileName)
        {
            using(StreamWriter sw  = new StreamWriter(fileName))
            {
                foreach(string line in rTextBox.Lines)
                {
                    sw.WriteLine(line);
                }
            }
        }

        private void LoadProgramFromFile(string fileName)
        {
            if(File.Exists(fileName))
            {
                rTextBox.Text = File.ReadAllText(fileName);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "IOGPL Files (*.iogpl)|*.iogpl|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                SaveProgramToFile(fileName);
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "IOGPL Files (*.iogpl)|*.iogpl|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                LoadProgramFromFile(fileName);
            }
        }

        private void syntaxBtn_Click(object sender, EventArgs e)
        {
            // check syntax to on command line

        }
    }

   
}
