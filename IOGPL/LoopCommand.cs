using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{

    public class LoopCommand : IProgramCommand
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
        public void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false)
        {
            // check if the iterations is a declared variabl
            if (int.TryParse(parts[1], out int result))
            {
                proto.iterations = result;
            } else
            {
                // find the value in the variables
                if (proto.variables.ContainsKey(parts[1]))
                {
                    proto.iterations = proto.variables[parts[1]];
                } else
                {
                    Console.WriteLine("Basically you are just a very weird student!");
                }
            }
            proto.loopFlag = true;
            proto.loopCounter = 0;
            proto.loopSize = 0;
            proto.loopStart = proto.programCounter;
        }
    }
}
