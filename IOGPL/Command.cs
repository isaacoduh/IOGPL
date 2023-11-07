using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IOGPL
{
    public interface ICommand
    {
        void Execute();
    }
    /// <summary>
    /// Abstract Class that defines a common method Execute. That 
    /// is executed by all basic command classes
    /// </summary>
    public class Command : ICommand
    {
        protected BaseCanvas c;
        public string Name;
        public string[] Parameters;

        public Command(BaseCanvas c)
        {
            this.c = c;
        }

        public Command(BaseCanvas c, string name, string[] parameters) : this(c)
        {
            Name = name;
            Parameters = parameters;
        }


        // public abstract void Execute();
        public void Execute()
        {
           switch (Name)
           {
                case "moveto":
                    if(Parameters.Length == 2 && int.TryParse(Parameters[0], out int x) && int.TryParse(Parameters[1], out int y))
                    {
                        c.MoveTo(x, y);
                    }
                break;
                case "drawto":
                    if (Parameters.Length == 2 && int.TryParse(Parameters[0], out int tX) && int.TryParse(Parameters[1], out int tY))
                    {
                        c.DrawTo(tX, tY);
                    } 
                break;
           }
        }
    }
}
