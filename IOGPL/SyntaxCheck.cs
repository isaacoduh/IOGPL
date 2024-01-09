using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class SyntaxCheck
    {
        public Dictionary<string, int> variables = new Dictionary<string, int>();
        public Dictionary<string, string> methodDictionary = new Dictionary<string, string>();
        public int programCounter = 0;
        int variableCounter = 0;
        public bool methodFlag = false;
        public bool methodExecuting = false;
        public int methodCounter = 0;
        public int saveProgramCounter = 0;
        public string[] methodNames = new string[3];
        public int[] methodLocation = new int[3];
        public bool executeLinesFlag = true;
        public bool dontExecute = false;
        public bool loopFlag = false;
        public int loopSize = 0;
        public int loopCounter = 0;
        public int loopStart = 0;
        public int iterations = 0;
        public bool isMethodDefinition = false;
        public string[] program;

        public SyntaxCheck()
        {

        }

        public class SyntaxCheckResult
        {
            public bool IsSyntaxValid { get; set; }
            public string[] Errors { get; set; }
        }

        public SyntaxCheck(string[] program) 
        {
            
            this.program = program;
        }

        public SyntaxCheckResult validateProgram(string[] program)
        {
            List<string> errors = new List<string>();
            while(programCounter < program.Length)
            {
                var line = program[programCounter++];
                var parts = line.Split(' ');
                var command = parts[0];

                if (parts[0] == "var")
                {
                    if (parts.Length == 4)
                    {
                        string variableName = parts[1];
                        int value;
                        if (!variables.ContainsKey(variableName) && int.TryParse(parts[3], out value))
                        {
                            variables.Add(variableName, value);
                        }
                        else
                        {
                            // Handle error: Variable already declared or invalid assignment
                            string message = $"Invalid variable declaration: {line}";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                    else
                    {
                        // Handle error: Invalid variable declaration
                        string message = $"Invalid variable declaration: {line}";
                        errors.Add($"Syntax error on line {programCounter}: {message} \n");
                    }
                }
                else if (parts.Contains("="))
                {
                    string variableName = parts[0];
                    if (!variables.ContainsKey(variableName))
                    {
                        // Handle error: Variable not declared
                        string message = $"Unknown Command {parts[0].Trim()}";
                        errors.Add($"Syntax error on line {programCounter}: {message} \n");
                    }
                    else
                    {
                        // Handle assignment to existing variable
                        // Additional logic for expression parsing can be added here
                        if (parts.Length == 4)
                        {
                            string message = $"Unknown Command {parts[0].Trim()}";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                }
                else if (!parts.Contains("=") && !checkValidKeyword(command))
                {
                    string message = $"Unknown Command {command.Trim()}";
                    errors.Add($"Syntax error on line {programCounter}: {message} \n");
                }
                else if (parts[0] == "loop")
                {
                    int iterationValue;
                    if (int.TryParse(parts[1], out iterationValue))
                    {
                        if (iterationValue < 1)
                        {
                            string message = $"Invalid loop iterations";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                    else
                    {
                        if (!variables.ContainsKey(parts[1]))
                        {
                            string message = $"iteration value not declared";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                }
                else if (parts[0] == "call")
                {
                    string methodName = parts[1].Split(',')[0];
                    int foundMethod = checkMethod(parts[1].Split(',')[0]);
                    if (foundMethod < 0)
                    {
                        string message = $"method with name {methodName} not found";
                        errors.Add($"Syntax error on line {programCounter}: {message} \n");
                    }
                }

            }
            if (errors.Count > 0)
            {
                return new SyntaxCheckResult { IsSyntaxValid = false, Errors = errors.ToArray() };
            }
            return new SyntaxCheckResult { IsSyntaxValid = true, Errors = Array.Empty<string>() };
        }

        private int checkMethod(string methodName)
        {
            for (int i = 0; i < methodCounter; i++)
            {
                if (methodNames[i] == methodName)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool checkValidKeyword(string keyWord)
        {
            string[] validKeyWords = { "var", "method", "loop", "endloop", "if", "endif", "circle", "pen", "fill", "tri", "drawTo", "moveTo", "rect", "square", "endmethod", "call" };
            return validKeyWords.Contains(keyWord);
        }

        /*public syntaxCheck checkProgram(string[] storedProgram)
        {
            List<string> errors = new List<string>();

            while (programCounter < storedProgram.Length)
            {
                var line = storedProgram[programCounter++];
                var parts = line.Split(' ');
                var command = parts[0];

                if (parts[0] == "var")
                {
                    if (parts.Length == 4)
                    {
                        string variableName = parts[1];
                        int value;
                        if (!variables.ContainsKey(variableName) && int.TryParse(parts[3], out value))
                        {
                            variables.Add(variableName, value);
                        }
                        else
                        {
                            // Handle error: Variable already declared or invalid assignment
                            string message = $"Invalid variable declaration: {line}";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                    else
                    {
                        // Handle error: Invalid variable declaration
                        string message = $"Invalid variable declaration: {line}";
                        errors.Add($"Syntax error on line {programCounter}: {message} \n");
                    }
                }
                else if (parts.Contains("="))
                {
                    string variableName = parts[0];
                    if (!variables.ContainsKey(variableName))
                    {
                        // Handle error: Variable not declared
                        string message = $"Unknown Command {parts[0].Trim()}";
                        errors.Add($"Syntax error on line {programCounter}: {message} \n");
                    }
                    else
                    {
                        // Handle assignment to existing variable
                        // Additional logic for expression parsing can be added here
                        if (parts.Length == 4)
                        {
                            string message = $"Unknown Command {parts[0].Trim()}";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                }
                else if (!parts.Contains("=") && !checkValidKeyword(command))
                {
                    string message = $"Unknown Command {command.Trim()}";
                    errors.Add($"Syntax error on line {programCounter}: {message} \n");
                }
                else if (parts[0] == "loop")
                {
                    int iterationValue;
                    if (int.TryParse(parts[1], out iterationValue))
                    {
                        if (iterationValue < 1)
                        {
                            string message = $"Invalid loop iterations";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                    else
                    {
                        if (!variables.ContainsKey(parts[1]))
                        {
                            string message = $"iteration value not declared";
                            errors.Add($"Syntax error on line {programCounter}: {message} \n");
                        }
                    }
                }
                else if (parts[0] == "call")
                {
                    string methodName = parts[1].Split(',')[0];
                    int foundMethod = checkMethod(parts[1].Split(',')[0]);
                    if (foundMethod < 0)
                    {
                        string message = $"method with name {methodName} not found";
                        errors.Add($"Syntax error on line {programCounter}: {message} \n");
                    }
                }
            }

            if (errors.Count > 0)
            {
                return new syntaxCheck { IsSyntaxValid = false, Errors = errors.ToArray() };
            }

            return new syntaxCheck { IsSyntaxValid = true, Errors = Array.Empty<string>() };
        }*/

    }
}
