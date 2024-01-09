using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Rotate
    {
        Proto proto;
        public Rotate(Proto proto) {
            this.proto = proto;
        }

        public void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false)
        {
            string[] ops = parts[1].Split(',');
            int x = ResolveValue(ops[1]);
            int y = ResolveValue(ops[2]);
            int width = ResolveValue(ops[3]);
            int height = ResolveValue(ops[4]);
            int angle = ResolveValue(ops[5]);
            canvas.DrawRotatedRectangle(x, y, width, height, Convert.ToSingle(angle));
        }

        private int ResolveValue(string input)
        {
            // Assuming input is either a variable or a constant value
            if (proto.variables.ContainsKey(input))
            {
                return proto.variables[input];
            }

            if (int.TryParse(input, out int constantValue))
            {
                return constantValue;
            }

            Console.WriteLine($"Invalid value: {input}");
            return 0;
        }
    }
}
