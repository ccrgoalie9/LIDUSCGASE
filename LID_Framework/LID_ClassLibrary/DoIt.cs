using System;
using System.IO;

namespace LID_ClassLibrary {
    public class DoIt {
        // Summary:
        //     Enables the aggregation of all classes for the program. 
        //     You can access the produced objects using the accessor
        //     methods.
        BinaryCreator todayBin;
        BearingRange todayBR;
        Download todayDownload;
        Scraper todayScraper;
        readonly Config config;
        Line todayLine;

        public DoIt(Config config) {
            this.config = config;
        }

        //Do Stuff
        public int FullProcess() {
            //See if Directories exist yet
            //If not make them
            DirectoryCheck();

            //Starts Error Checking file
            if(!File.Exists(config.ErrorFile)) {
                config.ErrorFile = @"C:\Users\" + Environment.UserName + @"\Documents\LID Files\ErrorLogs\Error.txt";
                File.Create(config.ErrorFile).Dispose();
            }
            string text = File.ReadAllText(config.ErrorFile);
            if(!text.Contains(DateTime.UtcNow.ToString("yyyy-MM-dd"))) {
                File.AppendAllText(config.ErrorFile, "Error Checking Starting: " + DateTime.UtcNow.ToString("yyyy-MM-dd") + "\n");

            }


            try {//Get the current Bulletin
                Console.Write("Fetching Current Bulletin...\t");
                todayDownload = new Download(config);
                Console.WriteLine("Current Bulletin Fetched");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Fetch The Bulletin\n" + x.Message);
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                return -1; //Error
            }

            try {//Get the necessary bits from the bulletin
                todayScraper = new Scraper(todayDownload.GetOutFile(), config);
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Scrape The Bulletin\n" + x.Message);
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                return -1; //Error
            }

            try {//Create the KML file in format: ICEBERGS_'date'.kml
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors(), config);
                Console.WriteLine("KML File Created");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The KML File\n" + x.Message);
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                return -1; //Error
            }

            try {//Math for the Bearings and Ranges
                Console.Write("Creating Bearings And Ranges...\t");
                todayBR = new BearingRange(todayScraper.GetCoordinatesIngestors(), config);
                Console.WriteLine("Bearing And Ranges Created");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The File");
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                return -1;
            }

            try {
                Console.Write("Creating Binary...\t\t");
                todayBin = new BinaryCreator(todayBR.GetCoordinates());
                Console.WriteLine("Binaries Created");
                todayBin.Debug();
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The Binary");
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                return -1;
            }

            return 1; //Success
        }

        public int PartialFromCoordinateFile(string filePath, int indicator) {
            DateTime into;
            try {
                into = Convert.ToDateTime(filePath.Substring(filePath.LastIndexOf(@"\") + 1, 10));
            } catch(Exception x) {
                Console.WriteLine(x.Message);
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                //Default Date Is Today in Case of error
                into = DateTime.UtcNow;
            }
            //1 is bulletin
            if(indicator == 1) {
                Console.Write("Fetching Historic Bulletin...\t");
                todayScraper = new Scraper(filePath, config);
                Console.WriteLine("Historic Bulletin Fetched");
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors(), into, config);
                Console.WriteLine("KML File Created");
            }
            //2 is Degree File, 3 is Decimal File
            if(indicator == 2 || indicator == 3) {
                Console.Write("Fetching Historic Coordinates...\t");
                todayScraper = new Scraper(filePath, indicator, config);
                Console.WriteLine("Historic Coordinates Fetched");
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors(), into, config);
                Console.WriteLine("KML File Created");
            }
            return 1;
        }

        //Accessor Methods
        public Download GetDownload() {
            return todayDownload;
        }

        public Scraper GetScraper() {
            return todayScraper;
        }

        public Line GetLine() {
            return todayLine;
        }

        public BearingRange GetBR() {
            return todayBR;
        }

        //Static Methods
        private void DirectoryCheck() {
            Console.Write("Updating Directories...\t\t");
            try { Directory.CreateDirectory(config.DirPath + @"\Bulletins"); }
            catch(Exception x) {
                config.CreateConfig();
                config.ReadConfig();
                Console.WriteLine("Error Creating Directories, Attempting To Fix By Re-Writing Configuration File");
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }
            Directory.CreateDirectory(config.DirPath + @"\KML");
            Directory.CreateDirectory(config.DirPath + @"\LatLongs");
            Directory.CreateDirectory(config.DirPath + @"\Polar");
            Directory.CreateDirectory(config.DirPath + @"\ErrorLogs");
            Console.WriteLine("Directories Updated");
        }

    }
}
