using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class EndMethodCommand
    {
        public EndMethodCommand() { }

        public EndMethodCommand(Proto p) { }
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
