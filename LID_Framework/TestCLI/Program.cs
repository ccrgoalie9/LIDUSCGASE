using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LID_ClassLibrary;

namespace TestCLI {
    class Program {
        static void Main(string[] args) {
            Config config = new Config();
            DoIt today = new DoIt(config);
            today.FullProcess();
            System.Diagnostics.Process.Start("explorer.exe", config.DirPath);
            Console.ReadKey();
        }
    }
}
