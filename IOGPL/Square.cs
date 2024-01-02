using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Square : Command
    {
        private int size;
        private string[] parameters;

        public Square() { }

        public Square(BaseCanvas c) : base(c)
        {
        }

        public Square(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            if (parameters.Length == 1 && int.TryParse(parameters[0], out int s))
            {
                this.size = s;
                c.Square(s);
            }
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, BaseCanvas c)
        {
            if(parts.Length < 2)
            {
                Console.WriteLine("Syntax Error: Square command is missing a value");
            }
            string varName = parts[1];
            if(variables.ContainsKey(varName))
            {
                int varValue = variables[varName];
                this.size = varValue;
                c.Square(size);
            } else if(int.TryParse(varName, out int intValue))
            {
                this.size = intValue;
                c.Square(size);
            } else
            {
                Console.WriteLine("Syntax Error: Invalid Square Command Value");
            }
        }
    }
}
