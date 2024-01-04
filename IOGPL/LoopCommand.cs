using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{

    public class LoopCommand
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LoopCommand"/> class.
        /// </summary>
        public LoopCommand()
        {

        }

        /// <summary>
        /// Handles the "loop" statement, by setting a number of iterations, and necessary flags. <see cref="Proto"/>
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="proto"></param>
        public void Handle(string[] parts, Proto proto)
        {
            proto.iterations = int.Parse(parts[1]);
            proto.loopFlag = true;
            proto.loopCounter = 0;
            proto.loopSize = 0;
            proto.loopStart = proto.programCounter;
        }
    }
}
