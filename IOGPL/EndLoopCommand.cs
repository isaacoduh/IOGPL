using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class EndLoopCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndLoopCommand"/> class.
        /// </summary>
        public EndLoopCommand() { }

        /// <summary>
        /// Handles the "endloop" statement, decreasing the loop size and resetting the program counter if needed.
        /// </summary>
        /// <param name="proto">An instance of the <see cref="Proto"/> class.</param>
        public void Handle(Proto p)
        {
            p.loopSize--;
            p.loopFlag = false;
            if(p.loopCounter++ < p.iterations)
            {
                p.programCounter = p.loopStart;
            }
        }
    }
}
