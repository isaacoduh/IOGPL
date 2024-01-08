using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace IOGPL
{
    /// <summary>
    /// Represents a command handler for variable-related operations in a custom programming language.
    /// </summary>
    public class VarCommand
    {
        private string[] parameters;
        private string valString;
        private int Val;

        /// <summary>
        /// Default constructor for the VarCommand class.
        /// </summary>

        public VarCommand() { }


        /// <summary>
        /// Constructor that takes a BaseCanvas parameter. (Note: The purpose of this constructor is unclear from the provided code.)
        /// </summary>
        /// <param name="c">An instance of the BaseCanvas class.</param>

        public VarCommand(BaseCanvas c) { }
        public VarCommand(string[] body)
        {
            this.parameters = body;
        }



        /// <summary>
        /// Handles variable-related operations based on the provided parameters.
        /// </summary>
        /// <param name="parameters">An array of strings representing the parameters of the command.</param>
        /// <param name="variables">A dictionary representing the current state of variables.</param>
        /// <param name="variableCounter">A reference to the variable counter.</param>
        /// <param name="reassignment">A flag indicating whether the operation is a variable reassignment.</param>


        public void Handle(string[] parameters, Dictionary<string, int> variables, ref int variableCounter, bool reassignment = false)
        {
            // split the parameters into var x = 10
            // expect the format to be 

            if (reassignment)
            {
                string varName = parameters[0];
                Console.WriteLine($" reassignment varName: {varName}");
                // check if the value exists in variables
                if (variables.ContainsKey(varName))
                {
                    int value = variables[varName];
                    Console.WriteLine($" reassignment value: {value}");
                    if (parameters.Contains("+"))
                    {
                        Console.WriteLine($" reassignment has +");
                        if (int.TryParse(parameters[4], out int operand))
                        {
                            Console.WriteLine($" reassignment operand: {operand}");
                            value += operand;
                            Console.WriteLine($" reassignment value + operand: {value}");
                            variables[varName] = value;
                            Console.WriteLine($" reassignment value + operand then variables: {variables[varName]}");
                            Console.WriteLine($"{varName} = {value}");
                        }
                        else
                        {
                            Console.WriteLine("Syntax error: Invalid operand value");
                        }
                    }
                    else if (parameters.Contains("-"))
                    {
                        if (int.TryParse(parameters[4], out int operand))
                        {
                            value -= operand;
                            variables[varName] = value;
                            Console.WriteLine($"{varName} = {value}");
                        }
                        else
                        {
                            Console.WriteLine("Syntax error: Invalid operand value");
                        }
                    }
                }
                
            }
            
            // Print each part
            if(parameters.Contains("="))
            {
                // check if the value after the = is a variable declared
                if (int.TryParse(parameters[3], out int result))
                {
                    Console.WriteLine("The value is a direct integer");
                    string varName = parameters[1];
                    string varVal = parameters[3];

                    DeclareVariable(variables, ref variableCounter, varName, varVal);
                } else
                {
                    // declare the variable and then assign the value of the other operand
                    // check variables contains
                    if (variables.ContainsKey(parameters[3]))
                    {
                        // then the value of x is not defined!
                        int leftOperand = variables[parameters[3]];
                        int rightOperand = int.Parse(parameters[5]);
                        if (parameters[4] == "+")
                        {
                            variables[parameters[1]] = leftOperand + rightOperand;
                        } else if (parameters[4] == "-")
                        {
                            variables[parameters[1]] = leftOperand - rightOperand;
                        } else if (parameters[4] == "*")
                        {
                            variables[parameters[1]] = leftOperand * rightOperand;
                        } else if (parameters[4] == "/")
                        {
                            variables[parameters[1]] = leftOperand / rightOperand;
                        }
                        
                    } else
                    {
                        Console.WriteLine($"{parameters[3]} is not declared!");
                    }
                }
               
                //SetVariable(varName, varVal);
            } else if(parameters.Contains("=") && !parameters.Contains("var"))
            {
                // check if the value exists
                // x = x + 1
                string varName = parameters[1];
                if (variables.ContainsKey(varName))
                {
                    // the variable is declared already
                    // get the value of the varName
                    int value = variables[varName];
                    // check what the operation is
                    if (parameters.Contains("+"))
                    {
                       if (int.TryParse(parameters[4], out int operand))
                        {
                            value += operand;
                            variables[varName] = value;
                            Console.WriteLine($"{varName} = {value}");
                        } else
                        {
                            Console.WriteLine("Syntax error: Invalid operand value");
                        }
                    } else if (parameters.Contains("-"))
                    {
                        if (int.TryParse(parameters[4], out int operand))
                        {
                            value -= operand;
                            variables[varName] = value;
                            Console.WriteLine($"{varName} = {value}");
                        }
                        else
                        {
                            Console.WriteLine("Syntax error: Invalid operand value");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Syntax error: Variable not declared");
                }
            } 
        }

        /// <summary>
        /// Declares a variable and sets its initial value.
        /// </summary>
        /// <param name="variables">A dictionary representing the current state of variables.</param>
        /// <param name="variableCounter">A reference to the variable counter.</param>
        /// <param name="varName">The name of the variable to declare.</param>
        /// <param name="varValue">The initial value of the variable.</param>

        public void DeclareVariable(Dictionary<string, int> variables, ref int variableCounter, string varName, string varValue)
        {
            if (!variables.ContainsKey(varName))
            {
                variables.Add(varName, 0);
                if (variables.ContainsKey(varName))
                {
                    // Try parsing the variable name as an integer
                    if (int.TryParse(varValue, out int v))
                    {
                        variables[varName] = v;
                    }
                    else
                    {
                        Console.WriteLine("Syntax error: Invalid variable value");
                    }

                    variableCounter++;
                    Console.WriteLine($"Variable declared: {varName} = {variables[varName]}");
                }
                else
                {
                    Console.WriteLine("Syntax error: Variable already declared");
                }
            }
            else
            {
                Console.WriteLine("Syntax error: Variable already declared");
            }
        }


        /// <summary>
        /// Sets the value of a variable.
        /// </summary>
        /// <param name="varName">The name of the variable to set.</param>
        /// <param name="varValue">The new value of the variable.</param>

        public void SetVariable(string varName, string varValue)
        {
            Console.WriteLine($"Setting...: {varName} = {varValue}");
        }


        /// <summary>
        /// Checks if a variable with the specified name is already declared.
        /// </summary>
        /// <param name="varName">The name of the variable to check.</param>

        public void checkVariable(string varName)
        {
            // check if the variable is already declared!
        }

    }
}
