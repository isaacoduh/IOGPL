using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    /// <summary>
    /// Represents the command handling logic for "if" statements.
    /// </summary>
    public class IfCommand : IProgramCommand
    {
        private Proto proto;

        public IfCommand() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IfCommand"/> class.
        /// </summary>
        /// <param name="proto">The Proto instance for command execution.</param>

        public IfCommand(Proto proto)
        {
            this.proto = proto;
        }

        /// <summary>
        /// Handles the "if" command by evaluating the condition and updating the executeLinesFlag accordingly.
        /// </summary>
        /// <param name="parts">The parts of the command.</param>
        public void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false)
        {
            string condition = string.Join(" ", parts.Skip(1)).Trim();
            string[] conditionPart = condition.Split(' ');
            int left = 0;
            int right = 0;
            string comparator = conditionPart[1].ToString();
            bool conditionPass = false;
            Console.WriteLine($"Comparator... {comparator}");

            if (proto.variables.TryGetValue(conditionPart[0], out int leftValue))
            {
                left = leftValue;
            }
            else if (int.TryParse(conditionPart[0], out int intLeft))
            {
                left = intLeft;
            }

            // create a function to do the above
            if (proto.variables.TryGetValue(conditionPart[2], out int rightValue))
            {
                right = rightValue;
            }
            else if (int.TryParse(conditionPart[2], out int intRight))
            {
                right = intRight;
            }

            switch (comparator)
            {
                case "<":
                    Console.WriteLine($"<");
                    if (left < right)
                    {
                        conditionPass = true;
                        proto.dontExecute = true;
                        Console.WriteLine("True");
                    }
                    else if(left > right)
                    {
                        conditionPass = false;
                        proto.dontExecute = false;
                        Console.WriteLine("False");
                    }
                    break;
                case ">":
                    Console.WriteLine($">");
                    Console.WriteLine($"<");
                    if (left > right)
                    {
                        // let the executing lines flag just continue
                        conditionPass = true;
                        proto.dontExecute = true;
                        Console.WriteLine("True");
                    }
                    else if(left < right)
                    {
                        conditionPass = false;
                        proto.dontExecute = false;
                        Console.WriteLine("False");
                    }
                    break;
            }

            if(conditionPass == true) {
                proto.dontExecute = false;
            } else if (conditionPass == false)
            {
                proto.dontExecute = true;
            }

        }

        
    }
}
