using System;
using System.Drawing;
using System.Windows.Forms;
using LID_ClassLibrary;

namespace LID_WinForm {
    public partial class About_Form : Form {
        readonly Config config;

        public About_Form() {
            InitializeComponent();
            config = new Config();

            if(config.FuNtImE) {
                Random winPos = new Random();
                this.Location = new Point(winPos.Next(0,900),winPos.Next(0,600));
                this.StartPosition = FormStartPosition.Manual;
            }
        }

        private void backButton_Click(object sender, EventArgs e) {
            //Just closes this window
            Dispose();
        }
    }
}
