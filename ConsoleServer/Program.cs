﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            AsynchronousSocketListener.StartListening();
        }
    }
}
