using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public interface IProgramCommand
    {
        void Handle(string[] parts, Dictionary<string, int> variables, Proto proto, ref int variableCounter, BaseCanvas canvas, bool reassignment = false);
    }
}
