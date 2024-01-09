using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class EndWhileCommand
    {
        /// <summary>
     /// Initializes a new instance of the <see cref="EndWhileCommand"/> class.
     /// </summary>
        public EndWhileCommand() { }

        /// <summary>
        /// Handles the "endwhile" command by adjusting loop-related properties in the <see cref="Proto"/> instance.
        /// </summary>
        /// <param name="p">The <see cref="Proto"/> instance.</param>
        public void Handle(Proto p)
        {
            p.whileLoopSize--;
            p.whileLoopConditionFlag = false;
            if (p.variables[p.whileLoopStarter] < p.whileLoopLimit)
            {
                p.programCounter = p.whileLoopStart;
            }
        }

       
    }
}
