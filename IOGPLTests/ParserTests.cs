using IOGPL;
namespace IOGPLTests
{
    /// <summary>
    /// Contains unit tests for the methods in the <see cref="CommandParser"/> class.
    /// </summary>
    /// <remarks>
    /// These tests cover various scenarios and edge cases to ensure the correctness
    /// of the parsing logic in the <see cref="CommandParser"/> class.
    /// </remarks>
    [TestClass]
    public class ParserTests
    {
        /// <summary>
        /// Tests the parsing of a valid "clear" command.
        /// </summary>
        [TestMethod]
        public void Test_valid_clear_command()
        {
            var parser = new CommandParser();
            parser.ParseCommand("clear");

            Assert.AreEqual("clear", parser.Action);
        }

        /// <summary>
        /// Tests the handling of an invalid "clear" command, expecting an exception of type <see cref="InvalidCommandException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void Test_Invalid_clear_command()
        {
            var parser = new CommandParser();
            parser.ParseCommand("cler");
        }

        [TestMethod]
        public void Test_valid_reset_command()
        {
            var parser = new CommandParser();
            parser.ParseCommand("reset");

            Assert.AreEqual("reset", parser.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void Test_Invalid_Reset_command()
        {
            var parser = new CommandParser();
            parser.ParseCommand("resett");
        }


        /// <summary>
        /// Tests the handling of an invalid action ("MoveTo") command, expecting an exception of type <see cref="InvalidCommandActionException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCommandActionException))]
        public void Test_MoveTo_AsInValidAction()
        {
            var parser = new CommandParser();

            parser.ParseCommand("MoveTo 100,100");
        }

        /// <summary>
        /// Tests the parsing of a valid "moveTo" command.
        /// </summary>
        [TestMethod]
        public void Test_moveTo_AsValidAction()
        {
            var parser = new CommandParser();
            parser.ParseCommand("moveTo 100,100");

            Assert.AreEqual("moveTo", parser.Action);
            CollectionAssert.AreEqual(new string[] { "100", "100" }, parser.Tokens);
        }

        /// <summary>
        /// Tests the handling of an invalid "moveTo" command with an invalid token count, expecting an exception of type <see cref="InvalidTokenCountException"/>.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Test_moveTo_WithInvalidTokenscount() {
            var parser = new CommandParser();
            parser.ParseCommand("moveTo 100");
        }

        [TestMethod]
        public void Test_drawTo_AsValidAction()
        {
            var parser = new CommandParser();
            parser.ParseCommand("drawTo 200,200");

            Assert.AreEqual("drawTo", parser.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandActionException))]
        public void Test_DrawTo_AsInvalidAction()
        {
            var parser = new CommandParser();
            parser.ParseCommand("DrawTo 100,100");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Test_drawTo_WithInvalidTokensCount()
        {
            var parser = new CommandParser();
            parser.ParseCommand("drawTo 200");
        }

        [TestMethod]
        public void Test_circle_asValidCommand()
        {
            var parser = new CommandParser();
            parser.ParseCommand("circle 50");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Test_cirlce_WithInvalidTokensCount()
        {
            var parser = new CommandParser();
            parser.ParseCommand("circle 50,40");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandActionException))]
        public void Test_circle_withInvalidSpelling_asInvalidCommand()
        {
            var parser = new CommandParser();
            parser.ParseCommand("circl 40");
        }

        [TestMethod]
        public void Test_Rect_ValidCommand()
        {
            var parser = new CommandParser();
            parser.ParseCommand("rect 40,40");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandActionException))]
        public void Test_Rect_WithInvalid_Spelling()
        {
            var parser = new CommandParser();
            parser.ParseCommand("rectt 40,40");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Test_Rect_With_InvalidTokensCount()
        {
            var parser = new CommandParser();
            parser.ParseCommand("rect 40,30,40");
        }

        [TestMethod]
        public void Test_Square_With_ValidCommand()
        {
            var parser = new CommandParser();
            parser.ParseCommand("square 40");
            Assert.AreEqual("square", parser.Action);
            CollectionAssert.AreEqual(new string[] { "40" }, parser.Tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandActionException))]
        public void Test_Square_With_InvalidSpelling()
        {
            var parser = new CommandParser();
            parser.ParseCommand("squr 40");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Test_Square_With_InvalidTokensCount()
        {
            var parser = new CommandParser();
            parser.ParseCommand("square 40,30");
        }

        [TestMethod]
        public void Test_Triangle_With_ValidCommand()
        {
            var parser = new CommandParser();
            parser.ParseCommand("tri 40,40,30,40,30,130");
            Assert.AreEqual("tri", parser.Action);
            CollectionAssert.AreEqual(new string[] { "40", "40", "30", "40", "30", "130" }, parser.Tokens);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandActionException))]
        public void Test_Triangle_with_InvalidSpelling()
        {
            var parser = new CommandParser();
            parser.ParseCommand("triang 40,40,30,40,30,130");

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTokenCountException))]
        public void Test_Triangle_With_InvalidTokensCount()
        {
            var parser = new CommandParser();
            parser.ParseCommand("tri 40,30");
        }
    }
}