using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LID_ClassLibrary;

namespace TestCLI {
    class Program {
        static void Main(string[] args) {
            Config config = new Config("config.txt");
            DoIt today = new DoIt(config);
            today.FullProcess();
            Thread.Sleep(5000);
        }
    }
}
