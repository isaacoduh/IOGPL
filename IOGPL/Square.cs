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
    }
}
