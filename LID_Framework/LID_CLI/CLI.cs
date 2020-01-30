using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LID_ClassLibrary;

namespace LID_CLI {
    class CLI {
        //Framework is the workhorse of the program
        private static Framework framework;

        static void Main(string[] args) {
            //See if Directories exist yet
            //If not make them
            DirectoryCheck();
            framework = new Framework();
            
            //Starts Error Checking file
            if(!File.Exists(framework.ErrorFile)) {
                framework.ErrorFile = @"C:\Users\" + Environment.UserName + @"\Documents\LID Files\ErrorLogs\Error.txt";
                File.Create(framework.ErrorFile).Dispose();
            }
            string text = File.ReadAllText(framework.ErrorFile);
            if(!text.Contains(DateTime.UtcNow.ToString("yyyy-MM-dd"))) {
                File.AppendAllText(framework.ErrorFile, "Error Checking Starting: " + DateTime.UtcNow.ToString("yyyy-MM-dd") + "\n");
            }


            try {//Get the current Bulletin
                Console.Write("Fetching Current Bulletin...\t");
                todayDownload = new Download(framework);
                Console.WriteLine("Current Bulletin Fetched");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Fetch The Bulletin\n" + x.Message);
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n"); //Write to error file
            }

            try {//Get the necessary bits from the bulletin
                todayScraper = new Scraper(todayDownload.GetOutFile(), framework);
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Scrape The Bulletin\n" + x.Message);
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Create the KML file in format: ICEBERGS_'date'.kml
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors(), framework);
                Console.WriteLine("KML File Created");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The KML File\n" + x.Message);
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Math for the Bearings and Ranges
                Console.Write("Creating Bearings And Ranges...\t");
                todayBR = new BearingRange(todayScraper.GetCoordinatesIngestors(), framework);
                Console.WriteLine("Bearing And Ranges Created");
                if(framework.Debug) {
                    todayBR.Debug();
                }
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The File");
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Create the Binary from the Bearing and Ranges
                Console.Write("Creating Binary...\t\t");
                todayBin = new BinaryCreator(todayBR.GetCoordinates(), framework);
                Console.WriteLine("Binaries Created");
                if(framework.Debug) {
                    todayBin.Debug();
                }
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The Binary");
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Convert the Binary to Armored ASCII
                Console.Write("Creating Armored ASCII...\t");
                todayAscii = new ArmoredAscii(todayBin.LineMessages, framework);
                Console.WriteLine("ASCII Created");
                if(framework.Debug) {
                    todayAscii.Debug();
                }
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create Armored ASCII");
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");

            }
        }
        private static void DirectoryCheck() {
            Console.Write("Updating Directories...\t\t");
            try { Directory.CreateDirectory(framework.DirPath + @"\Bulletins"); } catch(Exception x) {
                framework.CreateConfig();
                framework.ReadConfig();
                Console.WriteLine("Error Creating Directories, Attempting To Fix By Re-Writing Configuration File");
                File.AppendAllText(framework.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }
            Directory.CreateDirectory(framework.DirPath + @"\KML");
            Directory.CreateDirectory(framework.DirPath + @"\LatLongs");
            Directory.CreateDirectory(framework.DirPath + @"\Polar");
            Directory.CreateDirectory(framework.DirPath + @"\ErrorLogs");
            Console.WriteLine("Directories Updated");
        }


    }
}
