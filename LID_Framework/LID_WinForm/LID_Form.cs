using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LID_Framework;

namespace LID_WinForm {
    public partial class LID_Form : Form {

        readonly string partialPath;
        DoIt today;
        Download todayDownload;
        Scraper todayScraper;
        Line todayLine;

        public LID_Form() {
            InitializeComponent();
            partialPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");

            //Class DoIt combines everything into one actionable class
            today = new DoIt();
            todayDownload = today.GetDownload();
            todayScraper = today.GetScraper();
            todayLine = today.GetLine();
            Console.WriteLine("Application Deployed Successfully");

        }

        private void FilesButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\Files";
            Process.Start("explorer.exe", filePath);
        }

        private void EarthButton_Click(object sender, EventArgs e) {
            string earthPath = @"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe";
            string kmlPath = partialPath + @"\";
            //Has google earth?
            if (File.Exists(earthPath)) {
                Process.Start(kmlPath + todayLine.GetOutFile());
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void DegreeButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + todayScraper.GetDegOutFile(); ;
            Process.Start(filePath);
        }

        private void DecimalButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + todayScraper.GetDecOutFile(); ;
            Process.Start(filePath);
        }

        private void BulletinButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + todayDownload.GetOutFile(); ;
            Process.Start(filePath);
        }

        private void ChartButton_Click(object sender, EventArgs e) {
            //Current chart released by the iip
            Process.Start("https://www.navcen.uscg.gov/?pageName=iipCharts&Current");
        }

        private void ResBulletinButton_Click(object sender, EventArgs e) {
            //Current bulletin released by the iip
            Process.Start("https://www.navcen.uscg.gov/?pageName=iipB12Out");
        }

        private void DoItButton_Click(object sender, EventArgs e) {
            Console.WriteLine("Process Started...");
            today = new DoIt();
            todayDownload = today.GetDownload();
            todayScraper = today.GetScraper();
            todayLine = today.GetLine();
            Console.WriteLine("Process Finished");
        }
    }
}
