using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Reset : Command
    {
        public Reset(BaseCanvas c) : base(c)
        {
        }

        public override void Execute()
        {
            c.Reset();
        }
    }
}
