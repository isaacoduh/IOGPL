using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    /// <summary>
    /// Abstract Class that defines a common method Execute. That 
    /// is executed by all basic command classes
    /// </summary>
    public abstract class Command
    {
        public abstract void Execute();
    }
}
