using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace IOGPL
{
    public class Proto
    {
        BaseCanvas canvas;
        public Dictionary<string, int> variables = new Dictionary<string, int>();
        public int programCounter = 0;
        int variableCounter = 0;
       public  bool methodFlag = false;
        public bool methodExecuting = false;
       public int methodCounter = 0;
        public int saveProgramCounter = 0;
        public string[] methodNames = new string[100];
       public  int[] methodLocation = new int[100];
       public bool executeLinesFlag = true;
        public bool dontExecute = false;
        public bool loopFlag = false;
        public int loopSize = 0;
        public int loopCounter = 0;
        public int loopStart = 0;
        public int iterations = 0;
        /// <summary>
        /// This class will be the sandbox for the nextpart of the application
        /// </summary>
        public Proto() { }

        public Proto(BaseCanvas canvas)
        {
            this.canvas = canvas;
        }

        public void processProgram(string[] storedProgram)
        {
            while(programCounter < storedProgram.Length)
            {
                var line = storedProgram[programCounter++];
                var parts = line.Split(' ');
                var command = parts[0];

                if(command == "var")
                {
                    if(dontExecute == true)
                    {
                        continue;
                    }
                    VarCommand varCmd = new VarCommand();
                    varCmd.Handle(parts, variables, ref variableCounter);
                }

                

                if(command == "loop")
                {
                    iterations = int.Parse(parts[1]);
                    loopFlag = true;
                    loopCounter = 0;
                    loopSize = 0;
                    loopStart = programCounter;
                }

                if(command == "circle")
                {
                    Console.WriteLine("Simple Circle Encountered");
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

                if (command == "endloop")
                {
                    loopSize--;
                    loopFlag = false;
                    if(loopCounter++ < iterations)
                    {
                        programCounter = loopStart;
                    }
                }

                if (loopFlag == true)
                {
                    loopSize++;
                }
            }
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

        private int checkMethod(string methodName)
        {
            for(int i = 0; i < methodCounter; i++)
            {
                if (methodNames[i]== methodName)
                {
                    return i;
                }
            }
            return -1;
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
