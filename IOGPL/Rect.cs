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
    }
}
