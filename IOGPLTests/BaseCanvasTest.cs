using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    /// <summary>
    /// Unit tests for the <see cref="BaseCanvas"/> class.
    /// </summary>
    [TestClass]
    public class BaseCanvasTest
    {
        /// <summary>
        /// Tests the constructor of <see cref="BaseCanvas"/> with default values.
        /// </summary>
        [TestMethod]
        public void Test_ConstructorWith_DefaultValues()
        {
            BaseCanvas canvas = new BaseCanvas();

            Assert.AreEqual(640, canvas.xBaseCanvasSize);
            Assert.AreEqual(480, canvas.yBaseCanvasSize);
            Assert.IsTrue(canvas.isTestingMode);
        }


        /// <summary>
        /// Tests the <see cref="BaseCanvas.MoveTo"/> method to ensure it updates the position.
        /// </summary>
        [TestMethod]
        public void MoveTo_UpdatesPosition()
        {
            BaseCanvas canvas = new BaseCanvas();
            
            canvas.MoveTo(59, 89);

            Assert.AreEqual(59, canvas.xPosition);
            Assert.AreEqual(89, canvas.yPosition);
        }

        /// <summary>
        /// Tests the <see cref="BaseCanvas.DrawTo"/> method to ensure it updates the position.
        /// </summary>
        [TestMethod]
        public void DrawTo_UpdatesPosition()
        {
            BaseCanvas canvas = new BaseCanvas();
            canvas.MoveTo(40, 40);
            canvas.DrawTo(50, 50);

            Assert.AreEqual(50, canvas.xPosition);
            Assert.AreEqual(50, canvas.yPosition);
        }



        /// <summary>
        /// Tests that an <see cref="GPLException"/> is thrown when an invalid circle command is executed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(GPLException))]
        public void Invalid_CirlceCommand_ThrowsException()
        {
            BaseCanvas canvas = new BaseCanvas();
            canvas.Circle(-4);
        }
    }
}
