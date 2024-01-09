using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class EndWhileCommand
    {
        public EndWhileCommand() { }

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
