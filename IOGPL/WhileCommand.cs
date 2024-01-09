using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    /// <summary>
    /// Represents the command handling logic for "while" statements.
    /// </summary>
    public class WhileCommand
    {
        private Proto proto;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhileCommand"/> class.
        /// </summary>
        /// <param name="proto">The <see cref="Proto"/> instance for command execution.</param>
        public WhileCommand(Proto proto)
        {
            this.proto = proto;
        }

        /// <summary>
        /// Handles the "while" command by evaluating the condition and updating loop-related properties in the <see cref="Proto"/> instance.
        /// </summary>
        /// <param name="parts">The parts of the command.</param>
        /// <param name="variables">The dictionary of variables.</param>
        /// <param name="proto">The <see cref="Proto"/> instance.</param>
        /// <param name="variableCounter">The reference to the variable counter.</param>
        /// <param name="canvas">The <see cref="BaseCanvas"/> instance.</param>
        /// <param name="reassignment">A flag indicating whether the command involves variable reassignment.</param>
        public void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false)
        {
            // Extract and evaluate the condition parts
            string condition = string.Join(" ", parts.Skip(1)).Trim();
            string[] conditionPart = condition.Split(' ');
            int left = 0;
            int right = 0;
            string comparator = conditionPart[1].ToString();
            bool conditionPass = false;

            // Evaluate the left side of the condition
            if (proto.variables.TryGetValue(conditionPart[0], out int leftValue))
            {
                left = leftValue;
            }
            else if (int.TryParse(conditionPart[0], out int intLeft))
            {
                left = intLeft;
            }

            // Evaluate the right side of the condition
            if (proto.variables.TryGetValue(conditionPart[2], out int rightValue))
            {
                right = rightValue;
            }
            else if (int.TryParse(conditionPart[2], out int intRight))
            {
                right = intRight;
            }

            // Update loop-related properties in the Proto instance
            proto.whileLoopStarter = conditionPart[0];
            proto.whileLoopLimit = right;
            proto.whileLoopConditionFlag = true;
            proto.whileLoopCounter = 0;
            proto.whileLoopSize = 0;
            proto.whileLoopStart = proto.programCounter;
        }
    }
}
