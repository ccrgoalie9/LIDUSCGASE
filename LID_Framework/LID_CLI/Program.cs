using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LID_ClassLibrary;

namespace LID_CLI {
    class Program {
        static void Main(string[] args) {
            if(args.Length > 0)
                new Framework(args);
            else
                new Framework();
            Thread.Sleep(2000);
            //Console.ReadKey();
        }
    }
}
