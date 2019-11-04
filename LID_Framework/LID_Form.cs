using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LID_Framework {
    public partial class LID_Form : Form {

        string partialPath;
        string[] kmls;
        Download todayDownload;
        Scraper todayScraper;


        public LID_Form() {
            InitializeComponent();
            partialPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");
            //See if Directories exist yet
            //If not make them
            DirectoryCheck();

            //Get the current Bulletin
            Console.Write("Fetching Current Bulletin...\t");
            todayDownload = new Download();
            Console.WriteLine(" Current Bulletin Fetched");

            //Get the necessary bits from the bulletin
            todayScraper = new Scraper(todayDownload.GetOutFile());

            //Set the length for the KML creator array
            Line[] testKML = new Line[todayScraper.GetCoordinatesIngestors().Length];

            //String of names for the kml files to open automatically later maybe
            kmls = new string[todayScraper.GetCoordinatesIngestors().Length];

            //Create the KML files in format: 'date'_ICEBERGS_'ID'.kml
            Console.Write("Creating KML Files...\t\t");
            int i = 0;
            foreach(Ingestor x in todayScraper.GetCoordinatesIngestors()) {
                kmls[i] = (@"Files\KML\" + DateTime.UtcNow.ToString().Substring(0, 10).Replace("/", "_").Replace(" ", "") + "_ICEBERGS_" + x.GetID().ToString() + ".kml").Replace(" ","");
                testKML[i] = new Line(x.GetCoordinates(), (kmls[i]));
                i++;
            }
            Console.WriteLine(" KML Files Created");
            Console.WriteLine("Application Deployed Successfully");

        }

        private void DirectoryCheck() {
            Console.Write("Updating Directories...\t\t");
            DirectoryInfo dir2 = Directory.CreateDirectory(partialPath + @"\Files\Bulletins");
            DirectoryInfo dir3 = Directory.CreateDirectory(partialPath + @"\Files\KML");
            DirectoryInfo dir4 = Directory.CreateDirectory(partialPath + @"\Files\LatLongs");
            DirectoryInfo dir5 = Directory.CreateDirectory(partialPath + @"\Files\Radials");
            Console.WriteLine(" Directories Updated");
        }

        private void filesButton_Click(object sender, EventArgs e) {
            string filePath = partialPath + @"\Files";
            Process.Start("explorer.exe", filePath);
        }

        private void EarthButton_Click(object sender, EventArgs e) {
            string earthPath = @"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe";
            string kmlPath = partialPath + @"\";
            //Has google earth?
            if(File.Exists(earthPath)) {
                //Until we consolidate the kmls
                foreach(string x in kmls) {
                    Process.Start(kmlPath + x);
                    //Wait to avoid error
                    Thread.Sleep(2500);
                }
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
    }
}
