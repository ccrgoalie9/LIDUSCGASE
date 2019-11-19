using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using LID_ClassLibrary;

namespace LID_WinForm {
    public partial class About_Form : Form {

        public About_Form() {
            InitializeComponent();

            Random winPos = new Random();
            this.Location = new Point(winPos.Next(0, 900), winPos.Next(0, 600));
            this.StartPosition = FormStartPosition.Manual;

            ReadAbout();
        }

        private void BackButton_Click(object sender, EventArgs e) {
            //Just closes this window
            Dispose();
        }

        private void About_Form_Load(object sender, EventArgs e) {

        }

        private void ReadAbout() {
            
                ChangelogLabel.Text = Properties.Resources.about.ToString();
        }
    }
}
