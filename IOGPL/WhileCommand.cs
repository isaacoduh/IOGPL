using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class WhileCommand
    {
        private Proto proto;
        public WhileCommand(Proto proto) { this.proto = proto; }

        public void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false)
        {
            string condition = string.Join(" ", parts.Skip(1)).Trim();
            string[] conditionPart = condition.Split(' ');
            int left = 0;
            int right = 0;
            string comparator = conditionPart[1].ToString();
            bool conditionPass = false;

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
            proto.whileLoopStarter = conditionPart[0];
            proto.whileLoopLimit = right;

            proto.whileLoopConditionFlag = true;
            proto.whileLoopCounter = 0;
            proto.whileLoopSize = 0;
            proto.whileLoopStart = proto.programCounter;

            
        }

    }
}
