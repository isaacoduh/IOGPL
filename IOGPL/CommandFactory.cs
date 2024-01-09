using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class CommandFactory
    {
        public IProgramCommand CreateCommand(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "if":
                    return new IfCommand() as IProgramCommand;
                    break;
                case "loop":
                    return new LoopCommand() as IProgramCommand;

                default:
                    return null;
            }
        }
    }
}
