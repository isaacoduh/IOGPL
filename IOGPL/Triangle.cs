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
    }
}
