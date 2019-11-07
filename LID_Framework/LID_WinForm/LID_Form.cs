using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LID_Framework;

namespace LID_WinForm {
    public partial class LID_Form : Form {

        readonly string partialPath;
        string bulletinPath;
        DoIt today, current;
        Download todayDownload;
        Scraper todayScraper, currentScraper;
        Line todayLine;

        public LID_Form() {
            InitializeComponent();
            partialPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");

            //Initialize the openFile dialogs
            BulletinChooser.InitialDirectory = partialPath + @"\Files\Bulletin";
            BulletinChooser.FileName = "bulletin.txt";
            CoordChooser.InitialDirectory = partialPath + @"\Files\LatLongs";
            CoordChooser.FileName = "degree or decimal.txt";

            //Disable current buttons until they have references
            BulletinHistButton.Enabled = false;
            DegreeHistButton.Enabled = false;
            DecimalHistButton.Enabled = false;

            //Class DoIt combines everything into one actionable class
            today = new DoIt();
            todayDownload = today.GetDownload();
            todayScraper = today.GetScraper();
            todayLine = today.GetLine();
            Console.WriteLine("Application Deployed Successfully");

        }


        //Main standard buttons
        private void FilesButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\Files";
            Process.Start("explorer.exe", filePath);
        }

        private void EarthButton_Click(object sender, EventArgs e) {
            string earthPath = @"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe";
            string kmlPath = partialPath + @"\";
            //Has google earth?
            if(File.Exists(earthPath)) {
                Process.Start(kmlPath + todayLine.GetOutFile());
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }



        //Current Day's Files
        private void DegreeButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + todayScraper.GetDegOutFile();
            Process.Start(filePath);
        }

        private void DecimalButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + todayScraper.GetDecOutFile();
            Process.Start(filePath);
        }

        private void BulletinButton_Click(object sender, EventArgs e) {
            Process.Start(bulletinPath);
        }



        //Most Recent Files
        private void DegreeHistButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + currentScraper.GetDegOutFile();
            if(currentScraper != null) {
                Process.Start(filePath);
            }
        }

        private void DecimalHistButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + currentScraper.GetDecOutFile();
            Process.Start(filePath);
        }

        private void BulletinHistButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\" + todayDownload.GetOutFile();
            Process.Start(filePath);
        }



        //Online resources
        private void ChartButton_Click(object sender, EventArgs e) {
            //Current chart released by the iip
            Process.Start("https://www.navcen.uscg.gov/?pageName=iipCharts&Current");
        }

        private void ResBulletinButton_Click(object sender, EventArgs e) {
            //Current bulletin released by the iip
            Process.Start("https://www.navcen.uscg.gov/?pageName=iipB12Out");
        }



        //Actionable Buttons
        private void DoItButton_Click(object sender, EventArgs e) {
            Console.WriteLine("Process Started...");
            today = new DoIt();
            todayDownload = today.GetDownload();
            todayScraper = today.GetScraper();
            todayLine = today.GetLine();
            Console.WriteLine("Process Finished");
        }

        private void BulletinHistoryButton_Click(object sender, EventArgs e) {
            Console.WriteLine("Process. Started...");
            if(BulletinChooser.ShowDialog() == DialogResult.OK) {
                string file = BulletinChooser.FileName;
                bulletinPath = file;
                current = new DoIt(file,1);
                currentScraper = current.GetScraper();
                todayLine = current.GetLine();
                Console.WriteLine("Process Finished");

                //Enable Buttons as Possible
                BulletinHistButton.Enabled = true;
                DegreeHistButton.Enabled = true;
                DecimalHistButton.Enabled = true;
            } else {
                Console.WriteLine("Process Canceled by User");
            }
        }

        private void CoordHistoryButton_Click(object sender, EventArgs e) {
            Console.WriteLine("Process. Started...");
            if(BulletinChooser.ShowDialog() == DialogResult.OK) {
                string file = BulletinChooser.FileName;
                current = new DoIt(file,2);
                currentScraper = current.GetScraper();
                todayLine = current.GetLine();
                Console.WriteLine("Process Finished");

                //Enable Buttons as Possible
                BulletinHistButton.Enabled = false;
                DegreeHistButton.Enabled = true;
                DecimalHistButton.Enabled = true;
            } else {
                Console.WriteLine("Process Canceled by User");
            }
        }
    }
}
