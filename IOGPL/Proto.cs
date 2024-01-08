 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace IOGPL
{
    public class Proto
    {
        BaseCanvas canvas;
        public Dictionary<string, int> variables = new Dictionary<string, int>();
        public Dictionary<string, string> methodDictionary = new Dictionary<string, string>();
        public int programCounter = 0;
        int variableCounter = 0;
       public  bool methodFlag = false;
        public bool methodExecuting = false;
       public int methodCounter = 0;
        public int saveProgramCounter = 0;
        public string[] methodNames = new string[3];
       public  int[] methodLocation = new int[3];
       public bool executeLinesFlag = true;
        public bool dontExecute = false;
        public bool loopFlag = false;
        public int loopSize = 0;
        public int loopCounter = 0;
        public int loopStart = 0;
        public int iterations = 0;
        public bool isMethodDefinition = false;
        public bool isOk = true;
        public String errors = "";
        /// <summary>
        /// This class will be the sandbox for the nextpart of the application
        /// </summary>
        public Proto() { }

        public Proto(BaseCanvas canvas)
        {
            this.canvas = canvas;
        }

        private bool checkValidKeyword(string keyWord)
        {
            string[] validKeyWords = { "var", "method", "loop", "endloop", "if", "endif", "circle","pen","fill","tri","drawTo","moveTo","rect","square","endmethod","call" };
            return validKeyWords.Contains(keyWord);
        }

        private void syntaxReport(string message, string line)
        {
            isOk = false;
            errors += $"Syntax error on line {programCounter}: {message} \n";
        }

        public void analyzeProgram(string[] storedProgram)
        {
            while(programCounter < storedProgram.Length)
            {
                var line = storedProgram[programCounter++];
                var parts = line.Split(' ');
                var command = parts[0];

                // declare existing variables
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
                            syntaxReport(message, line);
                        }
                    }
                    else
                    {
                        // Handle error: Invalid variable declaration
                        string message = $"Invalid variable declaration: {line}";
                        syntaxReport(message, line);
                    }
                }
                else if (parts.Contains("="))
                {
                    string variableName = parts[0];
                    if (!variables.ContainsKey(variableName))
                    {
                        // Handle error: Variable not declared
                        string message = $"Unknown Command {parts[0].Trim()}";
                        syntaxReport(message, line);
                    }
                    else
                    {
                        // Handle assignment to existing variable
                        // Additional logic for expression parsing can be added here
                        if(parts.Length == 4)
                        {
                            string message = $"Unknown Command {parts[0].Trim()}";
                            syntaxReport(message, line);
                        }
                    }
                }
                else if (!parts.Contains("=") && !checkValidKeyword(command))
                {
                    // Handle error: Unknown command
                    string message = $"Unknown Command {command.Trim()}";
                    syntaxReport(message, line);
                }

                else if (parts[0] == "loop")
                {
                    int iterationValue;
                    if (int.TryParse(parts[1], out iterationValue))
                    {
                        if(iterationValue < 1)
                        {
                            string message = $"Invalid loop iterations";
                            syntaxReport(message, line);
                        } 
                    } else
                    {
                        if (!variables.ContainsKey(parts[1]))
                        {
                            string message = $"iteration value not declared";
                            syntaxReport(message, line);
                        }
                    }
                }
                else if (parts[0] == "call")
                {
                    string methodName = parts[1].Split(',')[0];
                    int foundMethod = checkMethod(parts[1].Split(',')[0]);
                    if(foundMethod  < 0)
                    {
                        string message = $"method with name {methodName} not found";
                        syntaxReport(message, line);
                    }
                }

            }

            //ShowErrors();
        }

        private void ShowErrors()
        {
            if (!isOk)
            {
                Console.WriteLine("Syntax Errors Found!");
                Console.WriteLine(errors);
            }
        }

        public void processProgram(string[] storedProgram)
        {
          // analyzeProgram(storedProgram);
                while (programCounter < storedProgram.Length && isOk)
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
                            Console.WriteLine($"We found method at {foundMethod}");
                            saveProgramCounter = programCounter + 1;
                            programCounter = methodLocation[foundMethod];
                            Console.WriteLine($"saveProgramCounter = ${saveProgramCounter}");
                            Console.WriteLine($"the program Counter should go to location - {methodLocation[foundMethod]}");

                            Console.WriteLine($"the argument from call is {argument}");
                            Console.WriteLine($"the value is {variables[argument]}");

                            Console.WriteLine($"the method dictionary for {methodName} is {methodDictionary[methodName]}");
                            // set variable for that one
                            variables[methodDictionary[methodName]] = variables[argument];
                            Console.WriteLine($"Setting {variables[methodDictionary[methodName]]} to {variables[argument]}");
                            /*string parameterName = parts[1].Split(',')[0];
                            string variableName = parts[1].Split(',')[1];
                            Console.WriteLine($"From Method Dictionary: parameterName - {parameterName}");
                            // find the parameter in the dictionary
                            if (methodDictionary.ContainsKey(parts[1].Split(',')[0]))
                            {
                                string parameterValue = variables[parts[1].Split(',')[1]].ToString();
                                Console.WriteLine($"{parameterName} should have the value of {parameterValue}");
                                variables[parameterName] = Convert.ToInt32(parameterValue);
                                Console.WriteLine($"{parameterName} now has the variable of {variables[parameterName]}");
                            }*/
                            /*string p = methodDictionary[parts[1].Split(',')[0]];
                            Console.WriteLine($"From Method Dictionary: p - {p}");
                            Console.WriteLine($"{p} should have the value of {variables[parts[1].Split(',')[1]]}");
                            variables["p"] = variables[parts[1].Split(',')[1]];
                            Console.WriteLine($"{p} now has the variable of {variables[p]}");*/
                            // find the value of the defined value
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
                        LoopCommand loopCmd = new LoopCommand();
                        loopCmd.Handle(parts, this);
                        /* iterations = int.Parse(parts[1]);
                         loopFlag = true;
                         loopCounter = 0;
                         loopSize = 0;
                         loopStart = programCounter;*/
                    }

                    if (command == "endloop")
                    {
                        EndLoopCommand endLoopCmd = new EndLoopCommand();
                        endLoopCmd.Handle(this);
                        /*loopSize--;
                        loopFlag = false;
                        if (loopCounter++ < iterations)
                        {
                            programCounter = loopStart;
                        }*/
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

                    if (command == "if")
                    {
                        if (dontExecute == false)
                        {
                            continue;
                        }
                        IfCommand ifCommand = new IfCommand(this);
                        ifCommand.Handle(parts);
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



                    /*if(command == "method")
                    {
                        methodNames[methodCounter] = parts[1];
                        methodLocation[methodCounter++] = programCounter;
                        methodFlag = true;
                        isMethodDefinition = true;
                        *//*if (dontExecute == false)
                        {
                            continue;
                        }
                        MethodCommand methodCommand = new MethodCommand();
                        methodCommand.Handle(parts, this);*//*
                    }*/

                    /*if(command == "endMethod")
                    {
                        if (dontExecute == false)
                        {
                            continue;
                        }
                        EndMethodCommand endMethodCommand = new EndMethodCommand();
                        endMethodCommand.Handle(command, this);
                    }*/
                    /*if(command == "endMethod" && methodExecuting == false)
                    {
                        methodFlag = false;
                    }

                    if(command == "endMethod" && methodExecuting == true)
                    {
                        methodExecuting = false;
                        programCounter = saveProgramCounter;
                        continue;
                    }*/

                    /*if(command == "call")
                    {
                        if (dontExecute == false)
                        {
                            continue;
                        }
                        int foundMethod = checkMethod(parts[1]);
                        if(foundMethod >= 0)
                        {
                            saveProgramCounter = programCounter + 1;
                            programCounter = methodLocation[foundMethod];
                            methodExecuting = true;
                            continue;

                        } else
                        {
                            // Throw Exception
                            throw new GPLException($"Method with name "+ parts[1] +" Not Found");
                        }
                    }*/

                    if (loopFlag == true)
                    {
                        loopSize++;
                    }
                }

            

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

        private int resolveVarName(string varName)
        {
            return variables[varName];
        }
        public void processxxProgram(string[] storedProgram)
        {
            
            foreach(string i in storedProgram)
            {
                
                var line = storedProgram[programCounter++];
                //split line
                string[] parts = line.Split(' ');
                string command = parts[0];
                saveProgramCounter = programCounter;

                if (methodFlag)
                {
                    Console.WriteLine("Method flag is currently on");
                    Console.WriteLine("So method should not execute");
                    //continue;
                }

                

                if(command == "loop")
                {
                    iterations = int.Parse(parts[1]);
                    Console.WriteLine($"loop with iterations - {iterations}");
                    loopFlag = true;
                    loopCounter = 0;
                    loopSize = 0;
                }

                if (command == "circle")
                {
                    Console.WriteLine("Simple Circle Encountered");
                    if (dontExecute == true)
                    {
                        continue;
                    }
                    Circle circle = new Circle();
                    circle.Handle(parts, variables, canvas);
                }

                

                if (command == "endloop")
                {
                    loopSize--;
                    loopFlag = false;
                    Console.WriteLine("Simple Endloop encountered!");
                    Console.WriteLine($"Loop size at endloop: {loopSize}");
                    Console.WriteLine($"At line {line}");
                    Console.WriteLine($"At programCounter {programCounter}");
                    if (loopCounter++ < iterations)
                    {
                        programCounter = programCounter - loopSize;
                    }
                }

                if (loopFlag == true)
                {
                    Console.WriteLine("Loop Flag is on");
                    // Increase size of loop body;
                    loopSize++;
                    Console.WriteLine($"Current Loop size in the loop flag truee: {loopSize}");
                }
                /*switch(command)
                {
                    case "var":
                        Console.WriteLine($" var {command}");
                        if (dontExecute == true)
                        {
                            continue;
                        }
                        VarCommand c = new VarCommand();
                        c.Handle(parts, variables, ref variableCounter);
                        break;
                    case "printvars":
                        PrintVariables();
                        break;
                    case "circle":
                        if (dontExecute == true)
                        {
                            continue;
                        }
                        Circle circle = new Circle();
                        circle.Handle(parts, variables, canvas);
                        break;
                    case "method":
                        MethodCommand methodCommand = new MethodCommand();
                        methodCommand.Handle(parts, this);
                        break;
                    case "endMethod":
                        EndMethodCommand endMethodCommand = new EndMethodCommand();
                        endMethodCommand.Handle(command, this);
                        break;
                    case "call":
                        // check for the method
                        int foundMethod = checkMethod(parts[1]);
                        if (foundMethod >= 0) {
                            methodExecuting = true;
                        }
                        else
                        {
                            Console.WriteLine($"Method Not Found");
                        }
                        break;
                    case "if":
                        if (dontExecute == true)
                        {
                            continue;
                        }
                        IfCommand ifCommand = new IfCommand(this);
                        ifCommand.Handle(parts);
                        break;
                    case "endif":
                        EndIfCommand endIfCommand = new EndIfCommand(this);
                        endIfCommand.Handle(command);
                        break;
                    case "loop":
                        // get the command the iteration
                       
                        if (int.TryParse(parts[1], out int intIteration)){
                            iterations = intIteration;
                            Console.WriteLine($"Iterations is {iterations}");
                        }
                        // set loop flag to on
                        loopFlag = true;
                        loopCounter = 0;
                        Console.WriteLine($"{loopFlag}");
                        break;
                    case "endloop":
                        Console.WriteLine("End loop part!");
                        // set loop flag to off
                        loopFlag = false;
                        //loopCounter++;
                        Console.WriteLine($"{loopFlag}");
                        Console.WriteLine("====== Stats ====");
                        Console.WriteLine($"EndLoop: Iterations - {iterations}");
                        Console.WriteLine($"EndLoop: ProgramCounter - {programCounter}");
                        Console.WriteLine($"EndLoop: LoopSize - {loopSize}");
                        
                        // check that the total iterations is less than the current thing
                        if(loopCounter++ < iterations)
                        {
                            Console.WriteLine($"Endloop: LoopCounter - {loopCounter}");
                            Console.WriteLine($"The loopCounter {loopCounter} is less than iterations {iterations} so we might need to go again");
                            Console.WriteLine($"So the program will be reset to the point {programCounter - loopSize}");
                            programCounter = programCounter - loopSize;
                        }
                        break;
                    default:
                        if(parts.Contains("=") & command != "var")
                        {
                            VarCommand vc = new VarCommand();
                            vc.Handle(parts, variables, ref variableCounter, true);
                        }
                        break;
                }*/
            }
        }

       

        public void PrintVariables()
        {
            Console.WriteLine("Current state of variables:");
            foreach (var variable in variables)
            {
                Console.WriteLine($"{variable.Key} = {variable.Value}");
            }
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        public void processPxrogram(string[] program)
        {
           
              
            for (int lineNumber = 0; lineNumber < program.Length; lineNumber++)
            {
                 string line = program[lineNumber].Trim();

                // process line
                processVariable(line);

            //    // split line into variable name and value
            //    string[] parts = line.Split('=');

            //    string variableName = parts[0].Trim().Substring(4); // var<extraspace>
            //    string variableParams = parts[1].Trim();
            
            //    int variableVal;
            //    if(variables.TryGetValue(variableName, out int savedValue))
            //    {
            //        variableVal = savedValue;
            //    } else if (int.TryParse(variableParams, out int integerValue))
            //    {
            //        variableVal = integerValue;
            //    } else
            //    {
            //        variableVal = evaluateExpression(variableParams);
            //    }

            //    variables[variableName] = variableVal;
            }

            //foreach(var keyValue in variables)
            //{
            //    Console.WriteLine($"{keyValue.Key} {keyValue.Value}");
            //}


        }

        static int evaluateExpression(string expression)
        {
            try
            {
                // Use DataTable.Compute for simple arithmetic expressions
                DataTable dataTable = new DataTable();
                return Convert.ToInt32(dataTable.Compute(expression, ""));
            }
            catch (Exception ex)
            {
                // Handle expression evaluation errors
                Console.WriteLine($"Error evaluating expression: {ex.Message}");
                return 0; // or handle it differently based on your requirements
            }
        }

       

    }

    
    
}
