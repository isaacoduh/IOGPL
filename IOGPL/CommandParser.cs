﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{

    public class CommandParser
    {
        BaseCanvas canvas;
        List<ParsedCommand> parsedCommands;

        public CommandParser() { }

        public CommandParser(BaseCanvas canvas)
        {
            this.canvas = canvas;
            this.canvas.RenderCursor();
        }
        public string Action { get; set; }
        public string[] Tokens { get; set; }

        public class ParsedCommand
        {
            public string Action { get; set; }
            public string[] Tokens { get; set; }
        }

        public ParsedCommand parsedCommand;

        public void processCommand() 
        {
            
            switch(parsedCommand.Action) 
            {
                case "moveTo":
                    Console.WriteLine(parsedCommand.Action);
                    Console.WriteLine(parsedCommand.Tokens.Length);
                    foreach(string token in parsedCommand.Tokens)
                    {
                        Console.WriteLine(token);
                    }
                    Command c = new MoveTo(canvas, parsedCommand.Action, parsedCommand.Tokens);
                    c.Execute();
                break;
                
            }
        }

        public void ParseProgram(string[] program)
        {
            // for each line pass and verify each command
            foreach(string line in  program)
            {
                string tLine = line.Trim();
                if(!string.IsNullOrEmpty(tLine) )
                {
                    try
                    {
                       //ParsedCommand p =  ParseCommand(line);
                        //processCommand();
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        public void ParseCommand(string command)
        {
            string[] parts = command.Split(' ');

            if(parts.Length == 1 ) {
                if(parts[0] == "clear" || parts[0] == "reset")
                {
                    Action = parts[0];
                    Tokens = null;
                } else
                {
                    throw new InvalidCommandException($"Invalid command. Only clear or reset can be called without a token list");
                }
            }
            else if (parts.Length >= 1)
            {
                Action = parts[0];
                Console.WriteLine($"action {Action}");
                string[] t = parts[1].Split(',');

                if (Action != "clear" || Action != "reset")
                {
                    if(t.Length == 1)
                    {
                        Tokens = t;
                    }
                    else
                    {
                        if (IsValidCommand(Action, parts[1].Split(',').Length))
                        {

                            if (parts.Length > 1)
                            {
                                Tokens = parts[1].Split(',');
                            }
                            else
                            {
                                Tokens = new string[0];
                            }

                        }
                        else
                        {
                            throw new InvalidCommandException($"Invalid command. Expected '{Action}' with the required number of tokens");
                        }
                    }

                    
                    /**/
                }

            }
            else
            {
                throw new InvalidCommandException("Invalid command format. Expected 'action tokens',");
            }

            parsedCommand = new ParsedCommand
            {
                Action = Action,
                Tokens = Tokens
            };
        }

        private bool IsValidCommand(string action, int tokenCount)
        {
            // Define valid commands and their associated token counts
            Dictionary<string, int> validCommands = new Dictionary<string, int>
            {
                {"moveTo",2 },
                {"drawTo", 2 },
                {"circle", 1 },
                {"rect", 2 },
                {"tri", 6 },
                {"pen",1 }
            };
            if(validCommands.ContainsKey(action.Trim())) 
            {
                int expectedTokenCount = validCommands[action.Trim()];
                if(tokenCount == expectedTokenCount)
                {
                    return true;
                } else
                {
                    throw new InvalidTokenCountException($"Invalid token count '{action}'. Expected {expectedTokenCount} tokens");
                }

            }

            return false;
        }
    }
}
