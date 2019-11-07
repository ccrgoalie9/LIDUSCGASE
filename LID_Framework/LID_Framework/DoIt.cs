using System;
using System.IO;

namespace LID_Framework {
    public class DoIt {
        // Summary:
        //     Enables the aggregation of all classes for the program. 
        //     You can access the produced objects using the accessor
        //     methods.
        readonly string partialPath;
        Download todayDownload;
        Scraper todayScraper;
        Line todayLine;

        public DoIt() {
            partialPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "");
        }

        //Do Stuff
        public int FullProcess() {
            //See if Directories exist yet
            //If not make them
            DirectoryCheck();

            //Get the current Bulletin
            Console.Write("Fetching Current Bulletin...\t");
            try {
                todayDownload = new Download();
                Console.WriteLine("Current Bulletin Fetched");
                //Get the necessary bits from the bulletin
                todayScraper = new Scraper(todayDownload.GetOutFile());
                //Create the KML file in format: ICEBERGS_'date'.kml
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors());
                Console.WriteLine("KML File Created");
                return 1;
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Fetch The Bulletin\n" + x.Message);
                return -1;
            }
        }

        public int PartialFromCoordinateFile(string filePath, int indicator) {
            DateTime into;
            try {
                into = Convert.ToDateTime(filePath.Substring(filePath.LastIndexOf(@"\") + 1, 10));
            } catch(Exception x) {
                Console.WriteLine(x.Message);
                //Default Date Is Today in Case of error
                into = DateTime.UtcNow;
            }
            //1 is bulletin
            if(indicator == 1) {
                Console.Write("Fetching Historic Bulletin...\t");
                todayScraper = new Scraper(filePath);
                Console.WriteLine("Historic Bulletin Fetched");
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors(), into);
                Console.WriteLine("KML File Created");
            }
            //2 is Degree File, 3 is Decimal File
            if(indicator == 2 || indicator == 3) {
                Console.Write("Fetching Historic Coordinates...\t");
                todayScraper = new Scraper(filePath, indicator);
                Console.WriteLine("Historic Coordinates Fetched");
                Console.Write("Creating KML File...\t\t");
                todayLine = new Line(todayScraper.GetCoordinatesIngestors(), into);
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

        //Static Methods
        private void DirectoryCheck() {
            Console.Write("Updating Directories...\t\t");
            Directory.CreateDirectory(partialPath + @"\Files\Bulletins");
            Directory.CreateDirectory(partialPath + @"\Files\KML");
            Directory.CreateDirectory(partialPath + @"\Files\LatLongs");
            Directory.CreateDirectory(partialPath + @"\Files\Radials");
            Console.WriteLine("Directories Updated");
        }

    }
}
