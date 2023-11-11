using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    [TestClass]
    public class BaseCanvasTest
    {
        [TestMethod]
        public void Test_ConstructorWith_DefaultValues()
        {
            BaseCanvas canvas = new BaseCanvas();

            Assert.AreEqual(640, canvas.xBaseCanvasSize);
            Assert.AreEqual(480, canvas.yBaseCanvasSize);
            Assert.IsTrue(canvas.isTestingMode);
        }

        [TestMethod]
        public void MoveTo_UpdatesPosition()
        {
            BaseCanvas canvas = new BaseCanvas();
            
            canvas.MoveTo(59, 89);

            Assert.AreEqual(59, canvas.xPosition);
            Assert.AreEqual(89, canvas.yPosition);
        }

        [TestMethod]
        public void DrawTo_UpdatesPosition()
        {
            BaseCanvas canvas = new BaseCanvas();
            canvas.MoveTo(40, 40);
            canvas.DrawTo(50, 50);

            Assert.AreEqual(50, canvas.xPosition);
            Assert.AreEqual(50, canvas.yPosition);
        }

        [TestMethod]
        [ExpectedException(typeof(GPLException))]
        public void Invalid_CirlceCommand_ThrowsException()
        {
            BaseCanvas canvas = new BaseCanvas();
            canvas.Circle(-4);
        }
    }
}
