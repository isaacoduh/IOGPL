using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class Proto
    {
        /// <summary>
        /// This class will be the sandbox for the nextpart of the application
        /// </summary>
        public Proto() { }

        public void processProgram(string[] program) 
        { 
            foreach(string i in program)
            {
                Console.WriteLine(i);
            }
        }
    }
}
