using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOGPL;

namespace IOGPLTests
{
    [TestClass]
    public class MethodTestClass
    {
        [TestMethod]
        public void HandleMethodCommand()
        {
            // Arrange
            MethodCommand method = new MethodCommand();
            var proto = new Proto();

            // Act
            method.Handle(new[] { "method", "methodName" }, proto);

            // Assert
            Assert.AreEqual("methodName", method.methodName);
            Assert.AreEqual(1, proto.methodCounter);
            Assert.IsTrue(proto.methodCounter > 0);
            Assert.IsTrue(proto.methodFlag);
        }

        [TestMethod]
        public void HandleMethodParameter() { }

        [TestMethod]
        public void HandleMethodReturnType() { }

        [TestMethod]
        public void HandleMethodParameterType() { }


        /// <summary>
        /// Tests the handling of end method command when method is not executing.
        /// </summary>
        [TestMethod]
        public void Handle_EndMethod_NotExecuting()
        {
            // arrange
            var endMethodCommand = new EndMethodCommand();
            var proto = new Proto { methodExecuting = false, methodFlag = true };

            // act
            endMethodCommand.Handle("endMethod", proto);

            // Assert
            Assert.IsFalse(proto.methodFlag);
        }

        [TestMethod]
        /*public void InvalidMethodName()
        {
            var method = new MethodCommand();
            var proto = new Proto();

            Assert.ThrowsException<GPLException>(() =>
            {
                method.Handle(new[] { "method", "xMethod" }, proto);
            });
        }*/
    }
}
