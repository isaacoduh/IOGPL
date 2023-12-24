using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    /// <summary>
    /// Represents a test class for the VarCommand class.
    /// </summary>
    [TestClass]
    public class VarCommandTests
    {
        /// <summary>
        /// Tests the DeclareVariable method when the variable is not declared.
        /// </summary>
        [TestMethod]
        public void DeclareVariable_WithDefault_ValueZero()
        {
            VarCommand cmd = new VarCommand();
            Dictionary<string, int> variables = new Dictionary<string, int>();
            int vCounter = 0;
            string vName = "x";
            string vValue = "";

            // act
            cmd.DeclareVariable(variables, ref vCounter, vName, vValue);

            // Assert
            Assert.IsTrue(variables.ContainsKey(vName));
            Assert.AreEqual(0, variables[vName]);
            Assert.AreEqual(1, vCounter);
        }

        /// <summary>
        /// Tests the DeclareVariable method when the variable is already declared.
        /// </summary>
        [TestMethod]
        public void DeclareVariable_With_WithGivenValue()
        {
            VarCommand cmd = new VarCommand();
            Dictionary<string, int> variables = new Dictionary<string, int>();
            int vCounter = 0;
            string vName = "x";
            string vValue = "10";

            // act
            cmd.DeclareVariable(variables, ref vCounter, vName, vValue);

            // Assert
            Assert.IsTrue(variables.ContainsKey(vName));
            Assert.AreEqual(10, variables[vName]);
            Assert.AreEqual(1, vCounter);
        }


        /// <summary>
        /// Tests the Handle method when reassigning a variable to ensure the variable value is updated.
        /// </summary>

        [TestMethod]
        public void Handle_UpdateValue_OnReassignment()
        {
            // Arrange
            VarCommand cmd = new VarCommand();
            Dictionary<string, int> variables = new Dictionary<string, int>() { { "x", 10 } };
            int vCounter = 1;
            string[] sampleParameters = { "x", "=", "x", "+", "1" };

            // Act
            cmd.Handle(sampleParameters, variables,  ref vCounter, true);

            // Assert
            Assert.AreEqual(11, variables["x"]);
        }
    }
}
