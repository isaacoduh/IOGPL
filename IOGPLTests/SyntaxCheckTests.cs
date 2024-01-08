using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    [TestClass]
    public class SyntaxCheckTests
    {
        /// <summary>
        /// Test to handle when an invalid command used!
        /// </summary>
        [TestMethod]
        public void ThrowSyntaxError_OnInvalidCommand()
        {
            var proto = new Proto();
            var storedProgram = new string[] { "xmoveTo" };

            // act
            proto.analyzeProgram(storedProgram);

            // assert
            Assert.IsFalse(proto.isOk);
            StringAssert.Contains(proto.errors, "Syntax error on line 1: Unknown Command xmoveTo");
        }

        /// <summary>
        /// Test to handle Syntax checking but with wrong loop variable
        /// </summary>
        [TestMethod]
        public void ThrowSyntaxError_OnInvalidLoopCommand_withUndeclared_Variable() 
        {
            var proto = new Proto();
            var storedProgram = new string[]
            {
                "var radius = 10",
                "loop c",
                "circle size",
                "endloop"
            };

            // act
            proto.analyzeProgram(storedProgram);

            // assert
            Assert.IsFalse(proto.isOk);
            StringAssert.Contains(proto.errors, "Syntax error on line 2: iteration value not declared");
        }

        /// <summary>
        /// Test to handle Syntax checking wrong loop iterations i.e. less than 1
        /// </summary>
        [TestMethod]
        public void ThrowSyntaxError_OnInvalidLoopCommand_with_Zero_Or_Negative_Iteration()
        {
            var proto = new Proto();
            var storedProgram = new string[]
            {
                "var radius = 10",
                "loop 0",
                "circle size",
                "endloop"
            };

            //act
            proto.analyzeProgram(storedProgram);

            // assert
            Assert.IsFalse(proto.isOk);
            StringAssert.Contains(proto.errors, "Syntax error on line 2: Invalid loop iterations");
        }

        /// <summary>
        /// Syntax checking wrong - when call command but with wrong method or missing method
        /// </summary>

        [TestMethod]
        public void ThrowSyntaxError_OnInvalidCall_with_MethodNotFound()
        {
            var proto = new Proto();
            var storedProgram = new string[]
            {
                "var radius = 10",
                "method basecircle,m",
                "circle size",
                "endmethod",
                "call basesquare"
            };


            // act
            proto.analyzeProgram(storedProgram);

            // assert
            Assert.IsFalse(proto.isOk);
            StringAssert.Contains(proto.errors, "Syntax error on line 5: method with name basesquare not found");
        }
    }
}
