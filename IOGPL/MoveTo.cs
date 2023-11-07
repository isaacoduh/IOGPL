using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public class MoveTo : Command
    {
        private string[] parameters;
        public MoveTo(BaseCanvas c) : base(c)
        {
        }

        public MoveTo(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }

        public override void Execute()
        {
            Console.WriteLine("This is MoveTO Command");
            if (Parameters.Length == 2 && int.TryParse(Parameters[0], out int x) && int.TryParse(Parameters[1], out int y))
            {
                c.MoveTo(x, y);
            }
        }
    }
}
