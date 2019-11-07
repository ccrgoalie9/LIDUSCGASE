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
        bool flag;

        public LID_Form() {
            InitializeComponent();
            partialPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");

            //Initialize the openFile dialogs
            BulletinChooser.InitialDirectory = partialPath + @"\Files\Bulletin";
            BulletinChooser.FileName = "bulletin.txt";
            CoordChooser.InitialDirectory = partialPath + @"\Files\LatLongs";
            CoordChooser.FileName = "deg or dec.txt";

            //Disable current buttons until they have references
            BulletinHistButton.Enabled = false;
            DegreeHistButton.Enabled = false;
            DecimalHistButton.Enabled = false;

            EarthButton.Enabled = false;

            BulletinButton.Enabled = false;
            DegreeButton.Enabled = false;
            DecimalButton.Enabled = false;

            //Class DoIt combines everything into one actionable class
            today = new DoIt();
            if(today.FullProcess() == 1) {
                EarthButton.Enabled = true;
                BulletinButton.Enabled = true;
                DegreeButton.Enabled = true;
                DecimalButton.Enabled = true;
            }
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
            string filePath = partialPath + @"\" + todayDownload.GetOutFile();
            Process.Start(filePath);
        }



        //Most Recent Files
        private void DegreeHistButton_Click(object sender, EventArgs e) {
            string filePath;
            if(flag) {
                filePath = currentScraper.GetDegOutFile();
            } else {
                filePath = currentScraper.GetDegOutFile();
            }
            Console.WriteLine(filePath);
            Process.Start(filePath);
        }

        private void DecimalHistButton_Click(object sender, EventArgs e) {
            string filePath;
            if(flag) {
                filePath = currentScraper.GetDecOutFile();
            } else {
                filePath = currentScraper.GetDecOutFile();
            }
            Console.WriteLine(filePath);
            Process.Start(filePath);
        }

        private void BulletinHistButton_Click(object sender, EventArgs e) {
            Process.Start(bulletinPath);
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
            if(today.FullProcess() == 1) {
                EarthButton.Enabled = true;
                BulletinButton.Enabled = true;
                DegreeButton.Enabled = true;
                DecimalButton.Enabled = true;
            }
            todayDownload = today.GetDownload();
            todayScraper = today.GetScraper();
            todayLine = today.GetLine();
            Console.WriteLine("Process Finished");
        }

        private void BulletinHistoryButton_Click(object sender, EventArgs e) {
            Console.WriteLine("Process Started...");
            if(BulletinChooser.ShowDialog() == DialogResult.OK) {
                string file = BulletinChooser.FileName;
                bulletinPath = file;
                current = new DoIt();
                if(current.PartialFromCoordinateFile(file, 1) == 1) {
                    currentScraper = current.GetScraper();
                    todayLine = current.GetLine();
                    Console.WriteLine("Process Finished");

                    //Enable Buttons as Possible
                    EarthButton.Enabled = true;
                    BulletinHistButton.Enabled = true;
                    DegreeHistButton.Enabled = true;
                    DecimalHistButton.Enabled = true;
                    flag = true;
                } else {
                    //If you return -1, doesn't currently happen
                }

            } else {
                Console.WriteLine("Process Canceled by User");
            }
        }

        private void CoordHistoryButton_Click(object sender, EventArgs e) {
            Console.WriteLine("Process Started...");
            if(CoordChooser.ShowDialog() == DialogResult.OK) {
                string file = CoordChooser.FileName;
                current = new DoIt();
                if(file.Contains("_Degree")) {
                    current.PartialFromCoordinateFile(file, 2);

                    //Enable Buttons as Possible
                    EarthButton.Enabled = true;
                    DegreeHistButton.Enabled = true;
                    BulletinHistButton.Enabled = false;
                    DecimalHistButton.Enabled = true;
                } else if(file.Contains("_Decimal")) {
                    current.PartialFromCoordinateFile(file, 3);

                    //Enable Buttons as Possible
                    EarthButton.Enabled = true;
                    DegreeHistButton.Enabled = false;
                    BulletinHistButton.Enabled = false;
                    DecimalHistButton.Enabled = true;
                }
                currentScraper = current.GetScraper();
                todayLine = current.GetLine();
                Console.WriteLine("Process Finished");
                flag = false;
            } else {
                Console.WriteLine("Process Canceled by User");
            }
        }
    }
}
