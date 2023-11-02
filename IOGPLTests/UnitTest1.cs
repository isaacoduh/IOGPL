using IOGPL;
namespace IOGPLTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Parse_ValidMovetoCommand()
        {
            // Arrange
            var parser = new CommandParser();

            // Act
            parser.Parse("moveto 100,100");

            // Assert
            Assert.AreEqual("moveto", parser.Action);
            CollectionAssert.AreEqual(new string[] { "100", "100" }, parser.Tokens);

        }
    }
}