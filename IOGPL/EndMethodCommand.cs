using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class EndMethodCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndMethodCommand"/> class.
        /// </summary>
        public EndMethodCommand() { }


        /// <summary>
        /// Initializes a new instance of the <see cref="EndMethodCommand"/> class with a reference to the <see cref="Proto"/> instance.
        /// </summary>
        /// <param name="p">The <see cref="Proto"/> instance.</param>
        public EndMethodCommand(Proto p) { }


        /// <summary>
        /// Handles the end method command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="p">The <see cref="Proto"/> instance.</param>
        public void Handle(string command, Proto p)
        {
            // check if 
            if(command == "endMethod" && p.methodExecuting == false)
            {
                p.methodFlag = false;
            } else if(p.methodExecuting == true)
            {
                p.methodExecuting = false;
                p.programCounter = p.saveProgramCounter;
            }
        }
    }
}
