using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class CommandParser
    {
        public string Action { get; set; }
        public string[] Tokens { get; set; }

        public void Parse(string command)
        {
            string[] parts = command.Split(' ');
            if (parts.Length == 2)
            {
                Action = parts[0];
                Tokens = parts[1].Split(',');
            }

        }
    }
}
