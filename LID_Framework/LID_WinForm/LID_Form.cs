using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using LID_ClassLibrary;

namespace LID_WinForm {
    public partial class LID_Form : Form {

        readonly string partialPath;
        string bulletinPath;
        readonly Config config;
        DoIt today, current;
        Download todayDownload;
        Scraper todayScraper, currentScraper;
        Line todayLine;
        bool flag;

        public LID_Form() {
            InitializeComponent();
            //Read the config file
            config = new Config();
            partialPath = config.DirPath;

            //Initialize the openFile dialogs
            BulletinChooser.InitialDirectory = partialPath + @"\Bulletins";
            BulletinChooser.FileName = "bulletin.txt";
            CoordChooser.InitialDirectory = partialPath + @"\LatLongs";
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
            today = new DoIt(config);
            if(today.FullProcess() == 1) {
                EarthButton.Enabled = true;
                BulletinButton.Enabled = true;
                DegreeButton.Enabled = true;
                DecimalButton.Enabled = true;
                todayDownload = today.GetDownload();
                todayScraper = today.GetScraper();
                todayLine = today.GetLine();
            } else {
                Console.WriteLine("Trying again in 10 minutes");
                EarthButton.Enabled = false;
                BulletinButton.Enabled = false;
                DegreeButton.Enabled = false;
                DecimalButton.Enabled = false;
                ErrorTimer.Enabled = true;
            }
            Console.WriteLine("Application Deployed");

        }


        //Main standard buttons
        private void FilesButton_Click(object sender, EventArgs e) {
            Process.Start("explorer.exe", config.DirPath);
        }
        private void ConfigButton_Click(object sender, EventArgs e) {
            Process.Start("explorer.exe", config.ConfigPath);
        }

        private void EarthButton_Click(object sender, EventArgs e) {
            string earthPath = @"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe";
            //Has google earth?
            if(File.Exists(earthPath)) {
                Process.Start(todayLine.GetOutFile());
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }



        //10 minute delay between attempting to carry out the full process if there is an error
        private void ErrorTimer_Tick(object sender, EventArgs e) {
            ErrorTimer.Enabled = false;
            today = new DoIt(config);
            if(today.FullProcess() == 1) {
                EarthButton.Enabled = true;
                BulletinButton.Enabled = true;
                DegreeButton.Enabled = true;
                DecimalButton.Enabled = true;
                todayDownload = today.GetDownload();
                todayScraper = today.GetScraper();
                todayLine = today.GetLine();
            } else {
                Console.WriteLine("Trying again in 10 minutes");
                EarthButton.Enabled = false;
                BulletinButton.Enabled = false;
                DegreeButton.Enabled = false;
                DecimalButton.Enabled = false;
                ErrorTimer.Enabled = true;
            }
        }



        //Current Day's Files
        private void DegreeButton_Click(object sender, EventArgs e) {
            Process.Start(todayScraper.GetDegOutFile());
        }

        private void DecimalButton_Click(object sender, EventArgs e) {
            Process.Start(todayScraper.GetDecOutFile());
        }

        private void BulletinButton_Click(object sender, EventArgs e) {
            Process.Start(todayDownload.GetOutFile());
        }



        //Most Recent Files
        private void DegreeHistButton_Click(object sender, EventArgs e) {
            string filePath;
            if(flag) {
                filePath = currentScraper.GetDegOutFile();
            } else {
                filePath = currentScraper.GetDegOutFile();
            }
            Process.Start(filePath);
        }

        private void DecimalHistButton_Click(object sender, EventArgs e) {
            string filePath;
            if(flag) {
                filePath = currentScraper.GetDecOutFile();
            } else {
                filePath = currentScraper.GetDecOutFile();
            }
            Process.Start(filePath);
        }

        private void BulletinHistButton_Click(object sender, EventArgs e) {
            Process.Start(bulletinPath);
        }



        //Online resources
        private void ChartButton_Click(object sender, EventArgs e) {
            //Current chart released by the IIP
            Process.Start(config.ChartUrl);
        }

        private void ResBulletinButton_Click(object sender, EventArgs e) {
            //Current bulletin released by the IIP
            Process.Start(config.BulletinUrl);
        }



        //Actionable Buttons
        private void DoItButton_Click(object sender, EventArgs e) {
            config.ReadConfig();
            Console.WriteLine("Process Started...");
            ErrorTimer.Enabled = false;
            today = new DoIt(config);
            if(today.FullProcess() == 1) {
                EarthButton.Enabled = true;
                BulletinButton.Enabled = true;
                DegreeButton.Enabled = true;
                DecimalButton.Enabled = true;
                todayDownload = today.GetDownload();
                todayScraper = today.GetScraper();
                todayLine = today.GetLine();
            } else {
                Console.WriteLine("Trying again in 10 minutes");
                EarthButton.Enabled = false;
                BulletinButton.Enabled = false;
                DegreeButton.Enabled = false;
                DecimalButton.Enabled = false;
                ErrorTimer.Enabled = true;
            }
        }

        private void BulletinHistoryButton_Click(object sender, EventArgs e) {
            config.ReadConfig();
            BulletinChooser.InitialDirectory = partialPath + @"\Bulletins";
            BulletinChooser.FileName = "bulletin.txt";
            Console.WriteLine("Process Started...");
            if(BulletinChooser.ShowDialog() == DialogResult.OK) {
                string file = BulletinChooser.FileName;
                bulletinPath = file;
                current = new DoIt(config);
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

        //Choose either a degree or decimal file
        private void CoordHistoryButton_Click(object sender, EventArgs e) {
            config.ReadConfig();
            CoordChooser.InitialDirectory = partialPath + @"\LatLongs";
            CoordChooser.FileName = "deg or dec.txt";
            Console.WriteLine("Process Started...");
            if(CoordChooser.ShowDialog() == DialogResult.OK) {
                string file = CoordChooser.FileName;
                current = new DoIt(config);
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
