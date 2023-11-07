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
            parser.ParseCommand("moveTo 100,100");

            // Assert
            Assert.AreEqual("moveTo", parser.Action);
            CollectionAssert.AreEqual(new string[] { "100", "100" }, parser.Tokens);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void Parse_InvalidMoveToCommand()
        {
            // Arrange
            var parser = new CommandParser();

            // Act
            parser.ParseCommand("movto 100,100");
        }


        [TestMethod]
        public void Parse_ValidDrawToCommand()
        {
            // arrange
            var parser = new CommandParser();

            // act
            parser.ParseCommand("drawTo 200,100");

            Assert.AreEqual("drawTo", parser.Action);
            CollectionAssert.AreEqual(new string[] {"200","100"}, parser.Tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void Parse_InvalidDrawToCommand()
        {
            var parser = new CommandParser();

            parser.ParseCommand("drawwto 200,200");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Parse_InvalidTokenCountDrawCommand_With_One_Token()
        {
            // arrange
            var parser = new CommandParser();

            // act
            parser.ParseCommand("drawTo 100");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Parse_InvalidTokenCountDrawCommand_With_Three_Tokens()
        {
            // arrange
            var parser = new CommandParser();

            // act
            parser.ParseCommand("drawTo 100,100,100");
        }
    }
}