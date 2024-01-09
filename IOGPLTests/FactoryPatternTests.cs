using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    [TestClass]
    public class FactoryPatternTests
    {
        [TestMethod]
        public void CreateProgramCommand_IfCommand_ClassFromFactory()
        {
            CommandFactory factory = new CommandFactory();
            IProgramCommand command = factory.CreateCommand("if");

            // Assert
            Assert.IsInstanceOfType(command, typeof(IfCommand));
        }
    }
}
