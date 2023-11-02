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
        

        
        private int defaultX = 5;
        private int defaultY = 5;

        private CommandProcessor processor = new CommandProcessor();


        public Form1()
        {
            InitializeComponent();

            Command m = new MoveTo(pBox, defaultX, defaultY);
            m.Execute();
            
            /*ICommand moveToCommand = new MoveToCommand(pBox, defaultX, defaultY);
            processor.SetCommand(moveToCommand);
            processor.ExecuteCommand();*/


        }

        private void cmdTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                string inputCommand = cmdTxtBox.Text;
                ActionParser parser = new ActionParser();
                parser.Parse(inputCommand);

                string action = parser.Action;
                string[] tokens = parser.Tokens;

                switch (action)
                {
                    case "moveto":
                        if (tokens.Length == 2 && int.TryParse(tokens[0], out int x) && int.TryParse(tokens[1], out int y))
                        {
                            var moveToCmd = new MoveTo(pBox, x, y);
                            moveToCmd.Execute();

                            /*ICommand moveToCommand = new MoveToCommand(pBox, x, y);
                            processor.SetCommand(moveToCommand);
                            processor.ExecuteCommand();*/
                        }
                        break;
                        // Handle other command cases
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*using(Graphics g = Graphics.FromImage(pBox.Image))
            {
                g.FillEllipse(Brushes.Black, defaultX, defaultY, 5, 5);
            }*/

            //pBox.Invalidate();
        }
    }
}
