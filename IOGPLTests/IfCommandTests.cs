using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    [TestClass]
    public class IfCommandTests
    {
        /// <summary>
        /// Test that the dont execute flag remains false <see cref="Proto.dontExecute"/>
        /// </summary>
        [TestMethod]
        public void IfCommand_DontExecute_RemainsFalse_WhenCondition_IsMet()
        {
            // Arrage
            var proto = new Proto();
            var ifCmd = new IfCommand(proto);
            int counter = 0;
            proto.variables["num1"] = 10;

            // act
            //ifCmd.Handle(new[] { "if", "num1", "<", "100" });
            ifCmd.Handle(new[] { "if", "num1", "<", "100" }, new Dictionary<string, int>(), proto, ref counter, null);

            // assert
            Assert.IsFalse(proto.dontExecute);
        }

        /// <summary>
        /// Tests set the dontExecute flag when condition is false <see cref="Proto.dontExecute"/>
        /// </summary>
        [TestMethod]
        public void IfCommand_DontExecute_SetsToTrue_WhenConditionFails()
        {
            var proto = new Proto();
            var ifCmd = new IfCommand(proto);
            int counter = 0;

            proto.variables["num1"] = 150;

            //ifCmd.Handle(new[] { "if", "num1", "<", "100" });
            ifCmd.Handle(new[] { "if", "num1", "<", "100" }, new Dictionary<string, int>(), proto, ref counter, null);

            // Assert
            Assert.IsTrue(proto.dontExecute);
        }

        /// <summary>
        /// Tests the Behavior of toggling the <see cref="Proto.dontExecute"/> at the Endif command
        /// </summary>
        [TestMethod]
        public void EndIfCommand_SetsTogglesExecuteflag()
        {
            var proto = new Proto();
            proto.dontExecute = true;
            var endifCmd = new EndIfCommand(proto);

            endifCmd.Handle("endif");
            Assert.IsFalse(proto.dontExecute);
        }
    }
}
