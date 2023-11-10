using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class PenCommand : Command
    {
        private Color color;
        private string[] parameters;

        public PenCommand(BaseCanvas c) : base(c)
        {
        }

        public PenCommand(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            if(parameters.Length == 1)
            {
                this.color = Color.FromName(parameters[0]);
                c.SetPenColor(parameters[0]);
            }
        }
    }
}
