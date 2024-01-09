 using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NCalc;
using static System.Net.Mime.MediaTypeNames;

namespace IOGPL
{
    public class Proto
    {
        BaseCanvas canvas;
        Form1 form1;
        Graphics graphics;
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
        public bool whileLoopConditionFlag = false;
        public int whileLoopSize = 0;
        public int whileLoopCounter = 0;
        public int whileLoopStart = 0;
        public int whileLoopIterations = 0;
        public bool WhileConditionComplete = true;
        public bool whileConditionMet = true;
        public int whileLoopLimit = 0;
        public string whileLoopStarter = "";
        /// <summary>
        /// This class will be the sandbox for the nextpart of the application
        /// </summary>
        public Proto() { }

        public Proto(BaseCanvas canvas)
        {
            this.canvas = canvas;
        }

        public Proto(BaseCanvas canvas, Form1 form1, Graphics graphics)
        {
            this.canvas = canvas;
            this.form1 = form1;
            this.graphics = graphics;
        }

        public void processProgram(string[] storedProgram)
        {
            SyntaxCheck syntaxCheck = new SyntaxCheck(storedProgram);
            var result = syntaxCheck.validateProgram(storedProgram);
            if(result.IsSyntaxValid == true && result.Errors.Length == 0)
            {
                runProgram(storedProgram);
            } else if(result.IsSyntaxValid == false)
            {
                string[] errorLines = result.Errors;
                string combineErrors = string.Join(Environment.NewLine, errorLines);
                // Assuming bmG is your Graphics object associated with the canvas
                graphics.Clear(Color.DarkGray);
                Font drawFont = new Font("Arial", 6);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Set string format
                StringFormat stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.NoClip;

                // Draw the text on the canvas
                graphics.DrawString(combineErrors, drawFont, drawBrush, 10, 10, stringFormat);

                // Refresh the canvas
                form1.Refresh();
            }
        }

        public void runProgram(string[] storedProgram)
        {
            while (programCounter < storedProgram.Length)
            {
                var line = storedProgram[programCounter++];
                var parts = line.Split(' ');
                var command = parts[0];

                /*if(methodFlag == true)
                {
                    continue;
                }*/

                if (isMethodDefinition == true & command != "endmethod")
                {
                    // we are just defining a method
                    Console.WriteLine("Not Executing as we are just defining a method.....");
                    continue;
                }

                if(command == "while")
                {
                    WhileCommand whileCommand = new WhileCommand(this);
                    whileCommand.Handle(parts, variables, this, ref variableCounter, canvas, false);
                }

                if(command == "endwhile")
                {
                    EndWhileCommand endWhileCommand = new EndWhileCommand();
                    endWhileCommand.Handle(this);
                }

                if (command == "if")
                {
                    
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    Console.WriteLine("...if point");
                    /*IfCommand ifCommand = new IfCommand(this);
                    ifCommand.Handle(parts, variables, this, ref variableCounter,canvas, false);*/
                    CommandFactory factory = new CommandFactory();
                    IProgramCommand ifCommand = factory.CreateCommand("if");
                    ifCommand.Handle(parts, variables, this, ref variableCounter, canvas, false);
                }

                if (command == "method")
                {
                    // split the parameters into methodname and argument
                    string methodName = parts[1].Split(',')[0];
                    string argument = parts[1].Split(',')[1];
                    Console.WriteLine($"Method Name: {methodName}, Method Argument: {argument}");
                    // increment method counter
                    methodNames[methodCounter] = methodName;
                    methodLocation[methodCounter++] = programCounter;
                    methodDictionary[methodName] = argument;
                    methodFlag = true;
                    isMethodDefinition = true;

                    /*if (dontExecute == false)
                    {
                        continue;
                    }
                    MethodCommand methodCommand = new MethodCommand();
                    methodCommand.Handle(parts, this);*/
                }

                if (command == "endmethod" && methodExecuting == false)
                {
                    // set method definition to false
                    methodFlag = true;
                    isMethodDefinition = false;
                    Console.WriteLine($"We have come to end method so is method definition is {isMethodDefinition}");
                }

                if (command == "endmethod" && methodExecuting == true)
                {
                    methodExecuting = false;
                    programCounter = saveProgramCounter;
                    continue;
                }

                if (command == "call")
                {
                    // check if the method exist
                    // methodName, x
                    Console.WriteLine($"Method and Parameter,{parts[1]}");
                    string methodName = parts[1].Split(',')[0];
                    string argument = parts[1].Split(',')[1];
                    Console.WriteLine($"Method {methodName}, Argument:{argument}");
                    // check if the argument declared is actually available

                    if (resolveVarName(argument) != null)
                    {
                        Console.WriteLine($"Argument Exists {resolveVarName(argument)}");
                    }
                    else
                    {
                        Console.WriteLine("Argument is not defined!");
                    }
                    int foundMethod = checkMethod(parts[1].Split(',')[0]);
                    if (foundMethod >= 0)
                    {
                        
                        saveProgramCounter = programCounter + 1;
                        programCounter = methodLocation[foundMethod];

                        
                        // set variable for that one
                        variables[methodDictionary[methodName]] = variables[argument];
                        
                        methodExecuting = true;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"We did not find that method...");
                    }
                }

                if (command == "var")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    VarCommand varCmd = new VarCommand();
                    varCmd.Handle(parts, variables, ref variableCounter);
                }



                if (command == "loop")
                {
                    
                    CommandFactory factory = new CommandFactory();
                    IProgramCommand loopCommand = factory.CreateCommand("loop");
                    loopCommand.Handle(parts, variables, this, ref variableCounter, canvas, false);
                }

                if (command == "endloop")
                {
                    EndLoopCommand endLoopCmd = new EndLoopCommand();
                    endLoopCmd.Handle(this);
                }

                if (command == "circle")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    Circle circle = new Circle();
                    circle.Handle(parts, variables, canvas);
                }

                if (command == "drawTo")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    DrawTo drawTo = new DrawTo();
                    drawTo.Handle(parts, variables, canvas);
                }

                if (command == "moveTo")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    MoveTo moveTo = new MoveTo();
                    moveTo.Handle(parts, variables, canvas);
                }

                if (command == "rect")
                {

                    if (dontExecute == true)
                    {
                        continue;
                    }
                    Rect rect = new Rect();
                    rect.Handle(parts, variables, canvas);
                }

                if (command == "square")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    Square square = new Square();
                    square.Handle(parts, variables, canvas);
                }

                if (command == "tri")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    Triangle tri = new Triangle();
                    tri.Handle(parts, variables, canvas);
                }

                if (command == "pen")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    PenCommand penCmd = new PenCommand();
                    penCmd.Handle(parts, canvas);
                }

                if (command == "fill")
                {
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    FillCommand fillCmd = new FillCommand();
                    fillCmd.Handle(parts, canvas);
                }

                

                if (command != "var" && parts.Contains("="))
                {
                    VarCommand vc = new VarCommand();
                    vc.Handle(parts, variables, ref variableCounter, true);
                }

                if (command == "endif")
                {
                    if (dontExecute == false)
                    {
                        continue;
                    }
                    EndIfCommand endIfCommand = new EndIfCommand(this);
                    endIfCommand.Handle(command);
                }

                if (loopFlag == true)
                {
                    loopSize++;
                }
            }


        }

        /*public void displaySyntaxErrors(string text)
        {
            bmG.Clear(Color.DarkGray);
            Font drawFont = new Font("Arial", 6);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            //Set string format
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.NoClip;

            bmG.DrawString(text, drawFont, drawBrush, 10, 10, stringFormat);
            _form.Refresh();
        }*/
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

        private int resolveVarName(string varName)
        {
            return variables[varName];
        }
        
       

       

        public void processVariable(string input)
        {
            // check for valid variable declaration
            if (validVariable(input))
            {
                // extract variable name
                string variableName = parseVariableName(input);
                int variableValue = parseVariableValue(input);

                variables[variableName] = variableValue;

                Console.WriteLine($"Variable {variableName} declared with value: {variableValue}");
            } else
            {
                Console.WriteLine("Invalid variable declaration");
            }
        }

        public bool validVariable(string input)
        {
            return input.Contains("var") && input.Contains("=");
        }

        public string parseVariableName(string input)
        {
            string[] parts = input.Split('=');

            if (parts.Length >= 2)
            {
                return parts[0].Substring(4).Trim();
            }
            else
            {
                Console.WriteLine($"Error: Invalid variable declaration. Unable to parse variable name in '{input}'.");
                return string.Empty;
            }
        }




        public int parseVariableValue(string input)
        {
            string valueStr = input.Split('=')[1].Trim();
            if (valueStr.Contains("+"))
            {
                return parseVariableExpression(valueStr);
            }
            else if (int.TryParse(valueStr, out int value))
            {
                return value;
            } else
            {
                // Throw exception invalid value for a variable
                return 0;
            }
        }

        public int parseVariableExpression(string expression)
        {

            try
            {
                string variableName = parseVariableName(expression);
                int variableValue = parseVariableValue(expression);

                // add the current variable to the parameters
                variables[variableName] = variableValue;

                Expression e = new Expression(expression);
                foreach (var variable in variables)
                {
                    e.Parameters[variable.Key] = variable.Value;
                }
                return Convert.ToInt32(e.Evaluate());

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error evaluating expression: {e.Message}");
                return 0;
            }
        }
    }

    
    
}
