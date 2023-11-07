using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class DrawTo : Command
    {
        private string[] parameters;
        public DrawTo(BaseCanvas c) : base(c)
        {
        }

        public DrawTo(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            Console.WriteLine("This is DrawToCommand");
            if (Parameters.Length == 2 && int.TryParse(Parameters[0], out int tX) && int.TryParse(Parameters[1], out int tY))
            {
                c.DrawTo(tX, tY);
            }
        }
    }
}
