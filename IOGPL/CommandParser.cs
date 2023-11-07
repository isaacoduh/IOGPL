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

        public CommandParser() { }

        public CommandParser(BaseCanvas canvas)
        {
            this.canvas = canvas;
            this.canvas.RenderCursor();
        }
        public string Action { get; set; }
        public string[] Tokens { get; set; }

        public void ParseCommand(string command)
        {
            string[] parts = command.Split(' ');
            
            Tokens = parts[1].Split(',');
            if(parts.Length >= 1)
            {
                Action = parts[0];
                if(IsValidCommand(Action, parts[1].Split(',').Length))
                {

                    if (parts.Length > 1)
                    {
                        Tokens = parts[1].Split(',');
                    }
                    else
                    {
                        Tokens = new string[0];
                    }

                } else
                {
                    throw new InvalidCommandException($"Invalid command. Expected '{Action}' with the required number of tokens");
                }
            } else
            {
                throw new InvalidCommandException("Invalid command format. Expected 'action tokens',");
            }

            
        }

        private bool IsValidCommand(string action, int tokenCount)
        {
            // Define valid commands and their associated token counts
            Dictionary<string, int> validCommands = new Dictionary<string, int>
            {
                {"moveTo",2 },
                {"drawTo", 2 },
                {"circle", 1 },
                {"rect", 4 },
                {"triangle", 4 },
                {"clear", 0 },
                {"reset", 0 },
                {"run", 0 }
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
