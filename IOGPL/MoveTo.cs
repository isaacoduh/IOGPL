using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class MoveTo : Command
    {
        private string[] parameters;

        public MoveTo() { }
        public MoveTo(BaseCanvas c) : base(c)
        {
        }

        public MoveTo(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            Console.WriteLine("This is MoveTO Command");
            if (Parameters.Length == 2 && int.TryParse(Parameters[0], out int x) && int.TryParse(Parameters[1], out int y))
            {
                c.MoveTo(x, y);
            }
        }

        public void Handle(string[] parts, Dictionary<string,int> variables, BaseCanvas c)
        {
            string[] coords = parts[1].Split(',');
            string xVarName = coords[0];
            string yVarName = coords[1];
            if(variables.ContainsKey(xVarName) || variables.ContainsKey(yVarName))
            {
                int xValue = variables[xVarName];
                int yValue = variables[yVarName];
                c.MoveTo(xValue, yValue);
            } else if (int.TryParse(xVarName, out int xValue) && int.TryParse(yVarName, out int yValue))
            {
                c.MoveTo(xValue, yValue);
            } else
            {
                Console.WriteLine("Syntax Error: Invalid MoveTo Command");
            }
        }
    }
}
