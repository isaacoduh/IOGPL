using IOGPL;
namespace IOGPLTests
{
    [TestClass]
    public class ParserTests
    {

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

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void Parse_InvalidMoveToCommand()
        {
            // Arrange
            var parser = new CommandParser();

            // Act
            parser.Parse("movto 100,100");
        }
    }
}