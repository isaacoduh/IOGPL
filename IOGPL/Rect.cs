using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Rect : Command
    {
        private int width;
        private int height;
        private string[] parameters;

        public Rect() { }
        public Rect(BaseCanvas c) : base(c)
        {
        }

        public Rect(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            if(parameters.Length == 2 && int.TryParse(parameters[0], out int width) && int.TryParse(parameters[1], out int height))
            {
                this.width = width;
                this.height = height;
                c.Rectangle(width, height);
            }
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, BaseCanvas c)
        {
            
            string[] dimensions = parts[1].Split(',');
            string widthVarName = dimensions[0];
            string heightVarName = dimensions[1];
            if(variables.ContainsKey(widthVarName) || variables.ContainsKey(heightVarName)) 
            {
                int widthValue = variables[widthVarName];
                int heightValue = variables[heightVarName];
                c.Rectangle(widthValue, heightValue);
            } else if(int.TryParse(widthVarName, out int widthValue) && int.TryParse(heightVarName, out int heightValue)) 
            { 
                c.Rectangle(widthValue, heightValue);
            } else
            {
                Console.WriteLine("Syntax Error: Invalid Rect Command");
            }
        }
    }
}
