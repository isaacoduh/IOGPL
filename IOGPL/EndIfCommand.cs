using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    /// <summary>
    /// Represents the command handling logic for "endif" statements.
    /// </summary>
    public class EndIfCommand
    {
        private Proto proto;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndIfCommand"/> class.
        /// </summary>
        /// <param name="proto">The Proto instance for command execution.</param>
        public EndIfCommand(Proto proto)
        {
            this.proto = proto;
        }

        /// <summary>
        /// Handles the "endif" command by updating the executeLinesFlag to true.
        /// </summary>
        /// <param name="command">The command string, expected to be "endif".</param>
        public void Handle(string command)
        {
            if(proto.dontExecute == true)
            {
                proto.dontExecute = false;
            }
            Console.WriteLine("end if command");
        }
    }
}
