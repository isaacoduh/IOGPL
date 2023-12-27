using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class MethodCommand
    {
        /// <summary>
        /// Gets or sets the method name.
        /// </summary>
        public string methodName { get; set; }

        /// <summary>
        /// Gets or sets the method location.
        /// </summary>
        public int methodLocation { get; set; }
        private Proto proto;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCommand"/> class.
        /// </summary>
        public MethodCommand() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCommand"/> class with a reference to the <see cref="Proto"/> instance.
        /// </summary>
        /// <param name="proto">The <see cref="Proto"/> instance.</param>
        public MethodCommand(Proto proto) {
        this.proto = proto;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCommand"/> class with the method name and location.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <param name="methodLocation">The method location.</param>
        public MethodCommand(string methodName, int methodLocation)
        {
            this.methodName = methodName;
            this.methodLocation = methodLocation;
        }

        /// <summary>
        /// Handles the method command.
        /// </summary>
        /// <param name="parameters">The parameters of the command.</param>
        /// <param name="proto">The <see cref="Proto"/> instance.</param>
        public void Handle(string[] parameters, Proto proto)
        {
            this.methodName = parameters[1];
            proto.methodNames[proto.methodCounter] = parameters[0];
            proto.methodLocation[proto.methodCounter++] = proto.programCounter;
            proto.methodFlag = true;
        }
    }
}
