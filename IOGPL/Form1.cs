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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;

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

        private void syntaxBtn_Click(object sender, EventArgs e)
        {
            bool isOk = false;
            String errors = "";
           
            // check syntax to on command line
            if (cmdTxtBox.Text != "")
            {
                string inputCommand = cmdTxtBox.Text.Trim();
                CommandParser parser = new CommandParser();
                try
                {
                   isOk =  parser.CheckCommandSyntax(inputCommand);

                }catch(Exception ex)
                {
                    errors += ex.Message;
                    
                }
                cmdTxtBox.Clear();
            } else if(rTextBox.Text != null)
            {
                var result = parser.CheckProgramSyntax(rTextBox.Lines);
                if (result.IsSyntaxValid)
                {
                    isOk = true;
                }
                else
                {
                    string[] errorLines = result.Errors;
                    string combineErrors = string.Join(Environment.NewLine, errorLines);
                    writeToScreen(combineErrors);

                }
                /*try
                {
                    
                } catch(Exception ex)
                {
                    errors += ex.Message;
                }*/
                rTextBox.Clear();
            }

            if (isOk)
            {
                writeToScreen("Syntax Check Ok");
            } else {
                writeToScreen(errors);
            }

           
            
        }

        /// <summary>
        /// Handles the KeyPress event for the command text box.
        /// </summary>
        /// <param name="sender">The object that raised the event (command text box).</param>
        /// <param name="e">A KeyPressEventArgs that contains the event data.</param>
        /// <remarks>
        /// This method is responsible for processing key presses in the command text box.
        /// It checks for the Enter key and, if pressed, interprets the entered command and
        /// executes the corresponding action on the canvas.
        /// </remarks>

        private void cmdTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            String errors = "";
            if(e.KeyChar == (char)Keys.Enter)
            {
                // Check if the command entered is only "run" and check if the rich text box 
                // holding the program is not empty. Then proceed to process the program.
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
            Font drawFont = new Font("Arial", 6);
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


        /// <summary>
        /// Processes a program represented as an array of strings.
        /// </summary>
        /// <param name="program">Array of strings representing the program to be processed.</param>
        /// <remarks>
        /// This method iterates through each line of the program, parses the command using
        /// the <see cref="CommandParser"/> class, and executes the corresponding command
        /// on the canvas based on the parsed action and tokens.
        /// </remarks>
        /// <param name="program">Array of strings representing the program to be processed.</param>
        /// <exception cref="InvalidCommandException">Thrown when an invalid command is encountered.</exception>
        /// <seealso cref="CommandParser"/>
        /// <seealso cref="DrawTo"/>
        /// <seealso cref="MoveTo"/>
        /// <seealso cref="Clear"/>
        /// <seealso cref="Reset"/>
        /// <seealso cref="Circle"/>
        /// <seealso cref="Rect"/>
        /// <seealso cref="Square"/>
        /// <seealso cref="Triangle"/>
        /// <seealso cref="PenCommand"/>
        /// <seealso cref="FillCommand"/>

        private void ProcessProgram(string[] program)
        {
            
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
                }
            }

            

        }

        private void processStoredProgram(string[] storedProgram)
        {
            //Proto p = new Proto(canvas, bmG, this);
            Proto p = new Proto(canvas, this, bmG);
            p.processProgram(storedProgram);

            /*var result = p.checkProgram(storedProgram);
            if (result.IsSyntaxValid)
            {
                isOk = true;
                p.processProgram(storedProgram);
                Console.WriteLine("This is the path to work!");
            }
            else
            {
                string[] errorLines = result.Errors;
                string combineErrors = string.Join(Environment.NewLine, errorLines);
                writeToScreen(combineErrors);
            }*/



        }

        private void displayErrorsToScreen(string text)
        {
            // Assuming bmG is your Graphics object associated with the canvas
            bmG.Clear(Color.DarkGray);
            Font drawFont = new Font("Arial", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Set string format
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.NoClip;

            // Draw the text on the canvas
            bmG.DrawString(text, drawFont, drawBrush, 10, 10, stringFormat);

            // Refresh the canvas
            Refresh();
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            
            if (rTextBox != null)
            {
                //ProcessProgram(rTextBox.Lines);
                processStoredProgram(rTextBox.Lines);
                rTextBox.Clear();
                cmdTxtBox.Clear();
            }
        }


        /// <summary>
        /// Saves the program from the RichTextBox to a file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the program to.</param>
        /// <remarks>
        /// This method writes each line from the <see cref="rTextBox"/> to the specified file.
        /// </remarks>
        /// <param name="fileName">The name of the file to save the program to.</param>

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


        /// <summary>
        /// Loads a program from a file into the RichTextBox.
        /// </summary>
        /// <param name="fileName">The name of the file to load the program from.</param>
        /// <remarks>
        /// This method reads the contents of the specified file and sets it as the text
        /// of the <see cref="rTextBox"/>.
        /// </remarks>
        /// <param name="fileName">The name of the file to load the program from.</param>

        private void LoadProgramFromFile(string fileName)
        {
            if(File.Exists(fileName))
            {
                rTextBox.Text = File.ReadAllText(fileName);
            }
        }


        /// <summary>
        /// Event handler for the "Save" button click.
        /// </summary>
        /// <param name="sender">The object that raised the event (the "Save" button).</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        /// <remarks>
        /// Displays a SaveFileDialog to prompt the user for the destination file,
        /// then calls <see cref="SaveProgramToFile"/> to save the program.
        /// </remarks>
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

        /// <summary>
        /// Event handler for the "Load" button click.
        /// </summary>
        /// <param name="sender">The object that raised the event (the "Load" button).</param>
        /// <param name="e">An EventArgs that contains the event data.</param>
        /// <remarks>
        /// Displays an OpenFileDialog to prompt the user to select a program file,
        /// then calls <see cref="LoadProgramFromFile"/> to load the program.
        /// </remarks>
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

       
    }

   
}
