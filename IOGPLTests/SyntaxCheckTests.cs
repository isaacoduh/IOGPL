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
            var syntaxCheck = new SyntaxCheck();
            var storedProgram = new string[] { "xmoveTo" };

            // act
            var result =  syntaxCheck.validateProgram(storedProgram);
            

            // assert
            Assert.IsFalse(result.IsSyntaxValid);
            Assert.AreEqual(1, result.Errors.Length);
        }

        [TestMethod]
        public void ValidateProgram_LoopWithNegativeIterations_ReturnsError()
        {
            // Arrange
            string[] program = { "loop -1", "circle x", "endloop" };
            SyntaxCheck syntaxCheck = new SyntaxCheck();

            // Act
            var result = syntaxCheck.validateProgram(program);

            // Assert
            Assert.IsFalse(result.IsSyntaxValid);
            Assert.IsTrue(result.Errors.Any(error => error.Contains("Invalid loop iterations")));
        }

       

        

        
    }
}
