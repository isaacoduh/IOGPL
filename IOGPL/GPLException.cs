﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOGPL
{
    public abstract class GPLException : Exception
    {
        public GPLException(string message) : base(message)
        {
        }
    }

    public class InvalidTokenCountException : GPLException
    {
        public InvalidTokenCountException(string message) : base(message)
        {

        }
    }

    public class InvalidCommandException : GPLException
    {
        public InvalidCommandException(string message) : base(message) { }
    }
}
