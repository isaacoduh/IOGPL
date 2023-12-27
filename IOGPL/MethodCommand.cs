using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class MethodCommand
    {
        public string methodName { get; set; }
        public int methodLocation { get; set; }
        private Proto proto;

        public MethodCommand() { }

        public MethodCommand(Proto proto) {
        this.proto = proto;
        }

        public MethodCommand(string methodName, int methodLocation)
        {
            this.methodName = methodName;
            this.methodLocation = methodLocation;
        }

        public void Handle(string[] parameters, Proto proto)
        {
            this.methodName = parameters[1];
            proto.methodNames[proto.methodCounter] = parameters[0];
            proto.methodLocation[proto.methodCounter++] = proto.programCounter;
            proto.methodFlag = true;
        }
    }
}
