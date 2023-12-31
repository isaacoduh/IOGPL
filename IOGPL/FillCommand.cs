﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public class FillCommand : Command
    {
        private string[] parameters;

        public FillCommand() { }
        public FillCommand(BaseCanvas c) : base(c)
        {
        }

        public FillCommand(BaseCanvas c, string name, string[] parameters) : base(c, name, parameters)
        {
            this.parameters = parameters;
        }


        public override void Execute()
        {
            if(parameters.Length == 1)
            {
                if (parameters[0].Equals("on") || parameters[0].Equals("off"))
                {
                    c.SetFillStatus();
                }
            }
        }

        public void Handle(string[] parts, BaseCanvas c)
        {
            if(parts.Length > 1)
            {
                if (parts[1].Equals("on") || parts[1].Equals("off"))
                {
                    c.SetFillStatus();
                }
            }
        }
    }
}
