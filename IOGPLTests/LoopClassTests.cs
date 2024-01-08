using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    [TestClass]
    public class LoopClassTests
    {
        /// <summary>
        /// Tests the <see cref="LoopCommand.Handle"/> method to ensure it sets iterations and flags correctly.
        /// </summary>
        [TestMethod]
        public void LoopCommand_SetsIterations_LoopFlags()
        {
            // arrange
            var proto = new Proto();
            var loopCmd = new LoopCommand();
            string[] parts = { "loop", "9" };

            // act
            loopCmd.Handle(parts, proto);

            // assert
            Assert.AreEqual(9, proto.iterations);
            Assert.IsTrue(proto.loopFlag);
            Assert.AreEqual(0, proto.loopCounter);
            Assert.AreEqual(0, proto.loopSize);
            Assert.AreEqual(0, proto.loopStart);
        }

        /// <summary>
        /// Tests the <see cref="LoopCommand.Handle"/> method to ensure it sets iterations and flags correctly.
        /// </summary>
        [TestMethod]
        public void LoopCommand_SetIterations_LoopFlag_with_Declared_Variable()
        {
            var proto = new Proto();
            var loopCmd = new LoopCommand();
            proto.variables["i"] = 10;
            string[] parts = { "loop", "i" };

            // act
            loopCmd.Handle(parts, proto);

            // assert
            Assert.AreEqual(10, proto.iterations);
        }

        /// <summary>
        /// Tests the <see cref="EndLoopCommand.Handle"/> method to ensure it decreases loop size.
        /// </summary>
        [TestMethod]
        public void EndLoopCommand_DecreasesLoopSize()
        {
            var proto = new Proto();
            var endLoopCmd = new EndLoopCommand();
            proto.loopSize = 2;
            proto.loopCounter = 1;
            proto.iterations = 3;
            proto.loopStart = 5;

            endLoopCmd.Handle(proto);

            
            Assert.AreEqual(3, proto.iterations);
            Assert.IsFalse(proto.loopFlag);
            Assert.AreEqual(2, proto.loopCounter);
            Assert.AreEqual(5, proto.programCounter);
            Assert.AreEqual(1, proto.loopSize);
        }
    }
}
