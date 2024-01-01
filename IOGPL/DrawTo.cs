using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class DrawTo : Command
    {
        private string[] parameters;

        public DrawTo() { }
        public DrawTo(BaseCanvas c) : base(c)
        {
        }

        public DrawTo(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            Console.WriteLine("This is DrawToCommand");
            if (Parameters.Length == 2 && int.TryParse(Parameters[0], out int tX) && int.TryParse(Parameters[1], out int tY))
            {
                c.DrawTo(tX, tY);
            }
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, BaseCanvas c)
        {
            string[] coords = parts[1].Split(','); 
            string xVarName = coords[0];
            string yVarName = coords[1];
            if (variables.ContainsKey(xVarName) || variables.ContainsKey(yVarName))
            {
                int xValue = variables[xVarName];
                int yValue = variables[yVarName];
                c.DrawTo(xValue, yValue);
            }
            else if (int.TryParse(xVarName, out int xValue) && int.TryParse(yVarName, out int yValue))
            {
               
                c.DrawTo(xValue, yValue);
            } else
            {
                Console.WriteLine("Syntax Error: Invalid drawto Command");
            }
        }
    }
}
