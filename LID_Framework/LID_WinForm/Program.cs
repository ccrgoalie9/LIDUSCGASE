using System;
using System.Windows.Forms;

namespace LID_WinForm {
    class Program {
        static void Main(string[] args) {
            //Setup the window
            Console.Title = "LID";
            Console.SetWindowSize(70, 10);

            Console.WriteLine("Deploying Application...\t");

            //LID GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LID_Form todayForm = new LID_Form();
            Application.Run(todayForm);
            todayForm.Dispose();
        }
    }
}
