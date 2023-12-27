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
        Dictionary<string, int> variables = new Dictionary<string, int>();
        public int programCounter = 0;
        int variableCounter = 0;
       public  bool methodFlag = false;
        public bool methodExecuting = false;
       public int methodCounter = 0;
        public int saveProgramCounter = 0;
        public string[] methodNames = new string[100];
       public  int[] methodLocation = new int[100];
        bool executeLinesFlag = true;
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
            foreach(string i in storedProgram)
            {
                
                var line = storedProgram[programCounter++];
                //split line
                string[] parts = line.Split(' ');
                string command = parts[0];

                if (methodFlag)
                {
                    Console.WriteLine("Method flag is currently on");
                    Console.WriteLine("So method should not execute");
                    //continue;
                }

                if(executeLinesFlag == false)
                {
                    continue;
                }
                switch(command)
                {
                    case "var":
                        Console.WriteLine($" var {command}");
                        VarCommand c = new VarCommand();
                        c.Handle(parts, variables, ref variableCounter);
                        break;
                    case "printvars":
                        PrintVariables();
                        break;
                    case "circle":
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
                    default:
                        if(parts.Contains("=") & command != "var")
                        {
                            VarCommand vc = new VarCommand();
                            vc.Handle(parts, variables, ref variableCounter, true);
                        }
                        break;
                }
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
