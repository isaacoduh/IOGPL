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
    }
}
