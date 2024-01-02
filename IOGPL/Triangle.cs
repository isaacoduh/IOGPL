using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Triangle : Command
    {
        private int x1, y1, x2, y2, x3, y3;
        private string[] parameters;

        public Triangle() { }
        public Triangle(BaseCanvas c) : base(c)
        {
        }

        public Triangle(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            if(parameters.Length == 6 && int.TryParse(parameters[0], out int x1)
                        && int.TryParse(parameters[1], out int y1)
                        && int.TryParse(parameters[2], out int x2)
                        && int.TryParse(parameters[3], out int y2)
                        && int.TryParse(parameters[4], out int x3)
                        && int.TryParse(parameters[5], out int y3))
            {
                this.x1 = x1;
                this.x2 = x2;
                this.x3 = x3;
                this.y1 = y1;
                this.y2 = y2;
                this.y3 = y3;

                c.Triangle(x1, y1, x2, y2, x3, y3);
            }
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, BaseCanvas c)
        {
            string[] points = parts[1].Split(',');
            string x1Varname = points[0];
            string y1Varname = points[1];
            string x2Varname = points[2];
            string y2Varname = points[3];
            string x3Varname = points[4];
            string y3Varname = points[5];
            if(variables.ContainsKey(x1Varname) || variables.ContainsKey(y1Varname) || variables.ContainsKey(x2Varname) || variables.ContainsKey(y2Varname) || variables.ContainsKey(x3Varname) || variables.ContainsKey(y3Varname))
            {
                int x1Value = variables[x1Varname];
                int y1Value = variables[y1Varname];
                int x2Value = variables[x2Varname];
                int y2Value = variables[y2Varname];
                int x3Value = variables[x3Varname];
                int y3Value = variables[y3Varname];
                c.Triangle(x1Value, y1Value, x2Value, y2Value, x3Value, y3Value);
            } else if(int.TryParse(x1Varname, out int x1Value) 
                && int.TryParse(y1Varname, out int y1Value) 
                && int.TryParse(x2Varname, out int x2Value)
                && int.TryParse(y2Varname, out int y2Value)
                && int.TryParse(x3Varname, out int x3Value)
                && int.TryParse(y3Varname, out int y3Value))
            {
                c.Triangle(x1Value, y1Value, x2Value, y2Value, x3Value, y3Value);
            } else
            {
                Console.WriteLine("Syntax Error: Invalid Triangle Command");
            }
        }
    }
}
