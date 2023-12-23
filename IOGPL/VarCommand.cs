using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class VarCommand
    {
        private string[] parameters;
        private string valString;
        private int Val;

        public VarCommand() { }
        public VarCommand(BaseCanvas c) { }
        public VarCommand(string[] body)
        {
            this.parameters = body;
        }

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
                string varName = parameters[1];
                string varVal = parameters[3];

                DeclareVariable(variables, ref variableCounter, varName, varVal);
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

        public void SetVariable(string varName, string varValue)
        {
            Console.WriteLine($"Setting...: {varName} = {varValue}");
        }

        public void checkVariable(string varName)
        {
            // check if the variable is already declared!
        }

    }
}
