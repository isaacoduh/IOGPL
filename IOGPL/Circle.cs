using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Circle : Command
    {
        private int radius;
        private string[] parameters;

        public Circle() { }
        public Circle(BaseCanvas c) : base(c)
        {
        }

        public Circle(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters) 
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            if (parameters.Length == 1 && int.TryParse(parameters[0], out int r))
            {
                this.radius = r;
                c.Circle(radius);
            }
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, BaseCanvas c)
        {
            Console.WriteLine($"This has a circle handle");
            // circle x
            /*string value = parts[1];
            if (!string.IsNullOrEmpty(value))
            {
                // check if the value is found then print
                
            }*/
            if(parts.Length < 2)
            {
                Console.WriteLine("Syntax Error: Circle command is missing a radius value");
                return;
            }
            string varName = parts[1];
            if(variables.ContainsKey(varName))
            {
                int varValue = variables[varName];
                Console.WriteLine($"Circle with variable: {varName} = {varValue}");
                this.radius = varValue;
                c.Circle(radius);
            } else if(int.TryParse(varName, out int intValue))
            {
                Console.WriteLine($"Circle with integer value {intValue}");
                this.radius = intValue;
                c.Circle(radius);
            }
            else
            {
                Console.WriteLine("Syntax error: Invalid circle command value");

            }
        }
    }
}
