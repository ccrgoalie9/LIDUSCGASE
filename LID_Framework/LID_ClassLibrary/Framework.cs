using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using SharpKml.Dom;
using SharpKml.Base;
using SharpKml.Engine;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary {
    public class Framework {
        //Declare Variables
        //Configuration variables
        public string configPath { get; set; }
        public string DirPath { get; set; }
        public string BulletinUrl { get; set; }
        public string ChartUrl { get; set; }
        public string WebUrl { get; set; }
        public int MMSI { get; set; }
        public string KmlColor1 { get; set; }
        public string KmlColor2 { get; set; }
        public string KmlColor3 { get; set; }
        public int KmlWidth { get; set; }
        public string ErrorFile { get; set; }
        public bool Debug { get; set; }
        private string outfile, ingested;
        private string[] lineTypes, ingests, coordsIngested;
        readonly private string[] append = { "A", "B", "C", "D", "E", "F", "G" };
        private double[,] coords;
        //Polar Variables
        double a, c, d, R, phi1, phi2, deltaphi, deltalambda, theta1, theta, theta2;
        readonly double[][,] polarSets;
        private double[][,] coordinates;
        //Binary Variables
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
        public int S { get; set; }
        public int Long { get; set; }
        public string Type { get; set; }
        public string RepeatIndicator { get; set; }
        public string Mmsi { get; set; }
        public string Encode { get; set; }
        public string DAC { get; set; }
        public string FID { get; set; }
        //public string Year { get; set; }
        public string Month { get; set; }

        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public List<string> LineMessages { get; set; }
        public string AreaShape { get; set; }
        public string ScaleFactor { get; set; }
        public string Longitude { get; set; }
        public string MsgLink { get; set; }
        public string Notice { get; set; }
        public string Spare { get; set; }
        public string MessageVersion { get; set; }
        public string Duration { get; set; }
        public string Action { get; set; }
        //Armored ASCII Variables
        public List<string> AsciiStream { get; set; }
        public List<string> AISMessages { get; set; }
        private readonly int timeStamp;

        //Constructor
        public Framework() {
            //Start by reading or creating CONFIG
            configPath = @"config.txt";
            if(!File.Exists(configPath)) {
                //Create file from defaults
                CreateConfig();
                ReadConfig();
            }
            if(File.Exists(configPath)) {
                ReadConfig();
            }

            DirectoryCheck();

            //Starts Error Checking file
            if(!File.Exists(ErrorFile)) {
                ErrorFile = "./LID_Files/ErrorLogs/Error.txt";
                File.Create(ErrorFile).Dispose();
            }
            string text = File.ReadAllText(ErrorFile);
            if(!text.Contains(DateTime.UtcNow.ToString("yyyy-MM-dd"))) {
                File.AppendAllText(ErrorFile, "Error Checking Starting: " + DateTime.UtcNow.ToString("yyyy-MM-dd") + "\n");
            }

            try {//Download the Bulletin
                Console.Write("Fetching Current Bulletin...\t");
                DownloadFromWeb();
                Console.WriteLine("Current Bulletin Fetched");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Fetch The Bulletin\n" + x.Message);
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n"); //Write to error file
            }

            try {//Get the necessary bits from the bulletin
                ReadFile();
                ScrapeFile();
                ConvertIngestor();
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Scrape The Bulletin\n" + x.Message);
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Create the KML file in format: ICEBERGS_'date'.kml
                Console.Write("Creating KML File...\t\t");
                Line();
                Console.WriteLine("KML File Created");
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The KML File\n" + x.Message);
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Math for the Bearings and Ranges
                Console.Write("Creating Bearings And Ranges...\t");
                polarSets = new double[coordinates.Length][,];
                ConvertCoordinates(coordinates);
                Console.WriteLine("Bearing And Ranges Created");
                if(Debug) {
                    //Debug();
                }
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The File");
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Create the Binary from the Bearing and Ranges
                Console.Write("Creating Binary...\t\t");
                BinaryCreator(polarSets);
                Console.WriteLine("Binaries Created");
                if(Debug) {
                    //Debug();
                }
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create The Binary");
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

            try {//Convert the Binary to Armored ASCII
                Console.Write("Creating Armored ASCII...\t");
                timeStamp = CalcTimeStamp();
                AsciiStream = new List<string>();
                AISMessages = new List<string>();
                ConvertToAscii(LineMessages);
                MessageConstructor();
                Console.WriteLine("ASCII Created");
                if(Debug) {
                    //Debug();
                }
            } catch(Exception x) {
                Console.WriteLine("Error: Failed To Create Armored ASCII");
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }

        }

        public Framework(string[] args) {
            //Index tracker
            int ind;
            //Boolean Variables to determine what to do

            //Start Options
            // <-h>,<-H>,<--Help> for Help
            if(args.Contains<string>("-h") || args.Contains<string>("-H") || args.Contains<string>("--help") || args.Contains<string>("--Help") || args.Contains<string>("help") || args.Contains<string>("Help")) {
                Console.WriteLine("" +
                    "Usage  : LID_CLI <optional args>\n" +
                    "  -c   : Declare config path, if in a directory other than that of the executable.\n" +
                    "         The path follows the option\n" +
                    "  -dd  : 'Don't Download' Do not execute the download step\n" +
                    "  -kml : Create KML, default is to not\n" +
                    "  -ais : Generate AIS message, default is to not\n" +
                    "  -dir : Open LID_Files directory");
                Environment.Exit(0);
            }
            // <-c> for Config Location
            if(args.Contains<string>("-c")) {
                if(args.Length > 1) {
                    ind = Array.IndexOf(args, "-c");
                    if(File.Exists(args[Array.IndexOf(args, "-c")])) {
                        configPath = args[ind + 1];
                        Console.WriteLine(configPath);
                    }
                } else {
                    Console.WriteLine("A path string must be passed after the <-c> option");
                    Environment.Exit(0);
                }
            } else {
                configPath = @"config.txt";
            }

            // <-dd> Don't download the bulletin
            if(args.Contains<string>("-dd")) {

            }

            // <-kml> Create kml, default is to not
            if(args.Contains<string>("-kml")) {

            }

            // <-ais> Generate AIS message, default is to not
            if(args.Contains<string>("-ais")) {

            }

            // <-dir> Open the LID_Files directory
            if(args.Contains<string>("-dir")) {
                System.Diagnostics.Process.Start("explorer.exe", DirPath);
            }

            //End Options

            //Start Program Given Options
            if(!File.Exists(configPath)) {
                //Create file from defaults
                CreateConfig();
                ReadConfig();
            }
            if(File.Exists(configPath)) {
                ReadConfig();
            }
        }

        //Modifiers
        public void ReadConfig() {
            int flag = 0;
            int flagCount = 11;
            try {
                using(StreamReader configReader = new StreamReader(configPath)) {
                    string temp;
                    while((temp = configReader.ReadLine()) != null) {
                        if(temp.StartsWith("#")) { continue; }
                        if(temp.Contains("Files Directory Location")) { DirPath = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Bulletin URL")) { BulletinUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Chart URL")) { ChartUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Website URL")) { WebUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("KML Color Berg Limit")) { KmlColor1 = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("KML Color Est Berg Limit")) { KmlColor2 = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("KML Color Sea Ice Limit")) { KmlColor3 = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Error File Location")) { ErrorFile = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        try {
                            if(temp.Contains("KML Line Width")) { KmlWidth = Convert.ToInt32(temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1)); flag++; }
                        } catch(Exception x) {
                            Console.WriteLine("Invalid Value for 'KML Line Width'\n" + x.Message);
                            File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                            //If error, default is 5
                            KmlWidth = 5;
                        }
                        try {
                            if(temp.Contains("Message MMSI")) { MMSI = Convert.ToInt32(temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1)); flag++; }
                        } catch(Exception x) {
                            Console.WriteLine("Invalid Value for 'MMSI'\n" + x.Message);
                            File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                            //If error, default is 003679999
                            MMSI = 003679999;
                        }
                        try {
                            if(temp.Contains("Debug")) { Debug = Convert.ToBoolean(temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1)); flag++; }
                        } catch(Exception x) {
                            Console.WriteLine("Invalid Value For An Expected Boolean\n" + x.Message);
                            File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                        }
                        //if(!configPath.Contains(Environment.UserName)) {
                        //    throw new Exception("Username does not match current directory");
                        //}
                    }
                    if(flag < flagCount) {
                        throw new Exception("Not All Expected Values Were Present");
                    }
                }
            } catch(Exception x) {
                Console.WriteLine("Error in reading configuration file, re-writing");
                ErrorFile = @"/ErrorLogs/Error.txt";
                CreateConfig();
                ReadConfig();
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }
        }
        //Scraper Methods
        //Read from the file
        private void ReadFile() {
            try {
                ingested = "";
                using(StreamReader coordFile = new StreamReader(outfile)) {
                    ingested = coordFile.ReadToEnd();
                }
            } catch(Exception x) {
                Console.WriteLine(x.Message);
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                ingested = "Read Failed";
            }
        }

        //Create the coordinatesIngested array
        private void ScrapeFile() {
            ingests = ingested.Split(' ');
            int count = 0;
            //Determine necessary length of array
            foreach(string x in ingests) {
                try {
                    if(x.Length >= 6 && x.Length <= 8 && !x.Contains("Z")) {
                        if(x.EndsWith(".")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            count++;
                        }
                    }
                } catch(Exception) {
                    //Do nothing
                }
            }
            //Set arrays of specific length
            coordsIngested = new string[count];
            lineTypes = new string[count];
            string temp = "";
            count = 0;
            //Temporary Variables for the 
            string temp1 = "";
            string temp2 = "";
            string tempHeader = "";
            //Parse for coordinates
            foreach(string x in ingests) {
                //For Section Headers
                if(x == "ESTIMATED" || x == "WESTERN" || x == "EASTERN" || x == "NORTHERN" || x == "SOUTHERN" || x == "SEA") {
                    temp1 = x;
                }
                if(x == "ICEBERG" || x == "ICE") {
                    temp2 = x;
                }
                if(x == "LIMIT") {
                    if(temp1 != "") tempHeader = (temp1 + " " + temp2 + " " + x);
                    else if(temp2 != "") tempHeader = (temp2 + " " + x);
                    temp1 = "";
                    temp2 = "";
                }

                //For Coordinates
                try {
                    if(x.Length >= 6 && x.Length <= 8 && !x.Contains("Z")) {
                        if(x.EndsWith(".")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            temp += x;
                            if(tempHeader != "") lineTypes[count] = tempHeader;
                            else lineTypes[count] = lineTypes[count - 1];
                            tempHeader = "";
                            coordsIngested[count] = temp;
                            count++;
                            temp = "";
                        } else if(x.EndsWith(",")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            temp += x;
                        } else {
                            Convert.ToInt32(x.Substring(x.Length - 2, 1));
                            temp += x + " ";
                        }
                    }
                } catch(Exception) {
                    //Do nothing
                }
            }
        }

        //Convert coordinates using the Ingestor class
        private void ConvertIngestor() {
            coordinates = new double[coordsIngested.Length][,];

            //Add Letters To Reflect Subsections of the same Set
            string indexes = "";
            string temp = "";
            for(int i = 0; i < lineTypes.Length - 1; i++) {
                temp = "";
                if(lineTypes[i] == lineTypes[i + 1]) {
                    temp = (i + 1).ToString();
                    indexes += i.ToString() + " ";
                }
            }
            if(temp != "") {
                indexes += temp;
            }
            if(indexes.Split().Length > 1) {
                int[] indexer = new int[indexes.Split().Length];
                int count = 0;
                foreach(string x in indexes.Split()) {
                    indexer[count] = Convert.ToInt32(x);
                    count++;
                }
                for(int i = 0; i < indexer.Length; i++) {
                    lineTypes[indexer[i]] += " " + append[i];
                }
            }
            //End Adding Letters

            for(int i = 0; i < coordinates.Length; i++) {
                coordinates[i] = ConvertInput(coordsIngested[i]);
            }
        }

        //Ingestor Methods
        //Convert ingest to array
        private double[,] ConvertInput(string ingested) {
            try {
                ingested = ingested.Replace(", ", ","); //Get rid of pesky spaces
                string[] tempByComma = ingested.Split(',');
                int length = tempByComma.Length;
                coords = new double[length, 2];
                //Convert from lat/long to degrees
                //North is Positive, East is Positive
                int left = 0;
                foreach(string line in tempByComma) {
                    string[] tempBySpace = line.Split(' ');
                    string temp1;
                    string temp2;
                    string direction;
                    int sign;

                    //Convert Lat
                    temp1 = tempBySpace[0].Substring(0, 2);
                    temp2 = tempBySpace[0].Substring(3, 2);
                    direction = tempBySpace[0].Substring(5, 1);
                    //Determine sign based on direction
                    if(direction == "S") {
                        sign = -1;
                    } else {
                        sign = 1;
                    }
                    coords[left, 0] = (Math.Truncate((Convert.ToDouble(temp1) + Convert.ToDouble(temp2) / 60) * sign * 1000000)) / 1000000;

                    //Convert Long
                    temp1 = tempBySpace[1].Substring(0, 3);
                    temp2 = tempBySpace[1].Substring(4, 2);
                    direction = tempBySpace[1].Substring(6, 1);
                    //Determine the sign based on direction
                    if(direction == "W") {
                        sign = -1;
                    } else {
                        sign = 1;
                    }
                    coords[left, 1] = (Math.Truncate((Convert.ToDouble(temp1) + Convert.ToDouble(temp2) / 60) * sign * 1000000)) / 1000000;

                    //Increment the array
                    left++;
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                //To easily determine if something went wrong
                coords = new double[,] { { -1, -1 }, { -1, -1 } };
            }
            return coords;
        }

        //Line methods



        //Line Class
        public void Line() {
            string filepath = (DirPath + @"/KML/" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_ICEBERGS.kml");
            string filename = (DateTime.UtcNow.ToString("yyyy-MM-dd") + "_ICEBERGS");

            //Check if file already exists
            if(File.Exists(filepath)) {
                File.Delete(filepath);
            }

            //Document Creation
            var document = new Document {
                Id = "KML",
                Open = true,
                Name = filename
            };

            //Styling
            string colorCode1 = KmlColor1;
            string colorCode2 = KmlColor2;
            string colorCode3 = KmlColor3;

            //Timespan
            SharpKml.Dom.TimeSpan lineTimespan = new SharpKml.Dom.TimeSpan {
                Begin = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd") + " 00:00:01"),
                End = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd") + " 23:59:59")
            };


            //Actual reads style and adds poly and line together
            string styleIL = "lineIcebergLimit";
            string styleEIL = "lineEstimatedIcebergLimit";
            string styleSIL = "lineSeaIceLimit";
            {
                //Style for Iceberg Limit
                Style SimpleStyle = new Style();
                SimpleStyle.Id = styleIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode1),
                    Width = KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode1)
                };
                document.AddStyle(SimpleStyle);
                //Style for Estimated Iceberg Limit
                SimpleStyle = new Style();
                SimpleStyle.Id = styleEIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode2),
                    Width = KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode2)
                };
                document.AddStyle(SimpleStyle);
                //Style for Sea Ice Limit
                SimpleStyle = new Style();
                SimpleStyle.Id = styleSIL;
                SimpleStyle.Line = new LineStyle {
                    Color = Color32.Parse(colorCode3),
                    Width = KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode3)
                };
                document.AddStyle(SimpleStyle);
            }

            //LINE STRING & PLACEMARK CONSTRUCTION ZONE
            int lineNum = 0;
            foreach(double[,] ingest in coordinates) {
                //One per segment
                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                linestring.AltitudeMode = AltitudeMode.ClampToGround;
                linestring.Extrude = true;
                linestring.Tessellate = true;

                double[,] coordArray = ingest;

                double[] lat = new double[(coordArray.Length / 2)];
                double[] lon = new double[(coordArray.Length / 2)];

                for(int i = 0; i < (coordArray.Length / 2); i++) {
                    lon[i] = coordArray[i, 1];
                }
                for(int i = 0; i < (coordArray.Length / 2); i++) {
                    lat[i] = coordArray[i, 0];
                }

                for(int i = 0; i < lon.Length; i++) {
                    coordinates.Add(new Vector(lat[i], lon[i]));
                }

                linestring.Coordinates = coordinates;
                Placemark placemark = new Placemark {
                    Name = lineTypes[lineNum],
                    Visibility = true,
                    Geometry = linestring,
                    //Moved StyleUrl out in order to be able to set it conditionally
                    //Moved Description out as well
                    Time = lineTimespan                                    //Timespan
                };
                if(lineTypes[lineNum].Contains("ESTIMATED ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleEIL), UriKind.Relative); //Uri makes url refrence to indocument style rather than cloud sourced
                    placemark.Description = new Description() { Text = "Based off of data from Greenland" };
                } else if(lineTypes[lineNum].Contains("ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                } else if(lineTypes[lineNum].Contains("SEA ICE LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleSIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Sea ice within limit displayed" };
                } else {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                }

                document.AddFeature(placemark);
                lineNum++;
            }
            //END LINE STRING CONSTRUCTION ZONE


            //Creates KML assignes it from document
            var kml = new Kml {
                Feature = document
            };

            //Outputs KML File
            KmlFile kmlFile = KmlFile.Create(kml, true);
            using(FileStream stream = File.OpenWrite(filepath)) {
                kmlFile.Save(stream);
            }
        }

        //Bearing Range Method
        private void ConvertCoordinates(double[][,] input) {
            string output = "";
            int count = 0;
            foreach(double[,] ingest in input) {
                double[,] coords = ingest;
                double[,] temp = new double[ingest.Length / 2, 2];
                //Set the first and last set
                temp[0, 0] = Math.Round(coords[0, 0], 2);
                temp[0, 1] = Math.Round(coords[0, 1], 2);
                output += lineTypes[count] + "\n";
                output += temp[0, 0] + " " + temp[0, 1] + "\n";

                //Do the math

                try {
                    for(int i = 0; i < (coords.Length / 2); i++) {
                        for(int j = 0; j <= 1; j++) {
                            coords[i, j] = coords[i, j] * (Math.PI / 180); // Convert to Radians
                        }
                    }
                    for(int i = 0; i < (coords.Length / 2 - 1); i++) {
                        phi1 = coords[i, 0];
                        phi2 = coords[i + 1, 0];
                        deltaphi = phi2 - phi1;
                        deltalambda = (coords[i, 1] - coords[i + 1, 1]);

                        //DISTANCE
                        R = 6371000; //RADIUS OF EARTH IN METERS
                        a = Math.Sin(deltaphi / 2) * Math.Sin(deltaphi / 2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Sin(deltalambda / 2) * Math.Sin(deltalambda / 2);
                        c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                        d = R * c; //DISTANCE IN METERS

                        //BEARING
                        theta1 = ((Math.Atan2(Math.Sin(deltalambda) * Math.Cos(phi2), Math.Cos(phi1) * Math.Sin(phi2) - Math.Sin(phi1) * Math.Cos(phi2) * Math.Cos(deltalambda)) * (180 / Math.PI)) + 360) % 360;
                        theta2 = ((Math.Atan2(Math.Sin(-deltalambda) * Math.Cos(phi1), Math.Cos(phi2) * Math.Sin(phi1) - Math.Sin(phi2) * Math.Cos(phi1) * Math.Cos(-deltalambda)) * (180 / Math.PI)) + 180) % 360;
                        theta = (theta1 + theta2) / 2;
                        theta = Math.Round((360 - theta), 2);
                        if(Math.Abs(theta - 360) < 5) {
                            theta = 0;
                        }
                        temp[i + 1, 0] = theta;
                        temp[i + 1, 1] = Math.Round((d / 1000), 2); //Distance in Kilometers
                        output += Math.Round((360 - theta), 2) + " " + Math.Round((d / 1000), 2) + "\n";
                    }
                    polarSets[count] = temp;
                    count++;
                } catch(Exception x) {
                    Console.WriteLine(x.Message);

                }
            }
        }

        //Binary Creator Method
        public void BinaryCreator(double[][,] PolarCoords) { //Will take input at some point


            // Try Catch statement -- encode message based upon type
            // Common code. All messages start with the same 38 bits....

            ////////////////////////////////////////////////////////////
            // Bits Len   Description
            //    0-5       6   Message Type
            //    6-7       2   Repeat Indicator
            //    8-37     30   MMSI

            try { // Try Catch Statement that creates an array of variables. 
                Type = Convert.ToString(8, 2).PadLeft(6, '0'); //6bits
                RepeatIndicator = Convert.ToString(0, 2).PadLeft(2, '0'); //2bits
                Mmsi = Convert.ToString(MMSI, 2).PadLeft(30, '0'); //30bits
                Spare = Convert.ToString(0, 2).PadLeft(2, '0'); //2bits

                MessageVersion = Convert.ToString(2, 2).PadLeft(6, '0');

                DAC = Convert.ToString(367, 2).PadLeft(10, '0');

                FID = Convert.ToString(22, 2).PadLeft(6, '0');

                //Set the time
                X = Convert.ToInt16(DateTime.UtcNow.ToString("MM"));
                Month = Convert.ToString(X, 2).PadLeft(4, '0');

                Y = Convert.ToInt16(DateTime.UtcNow.ToString("dd"));
                Day = Convert.ToString(Y, 2).PadLeft(5, '0');

                Z = Convert.ToInt16(DateTime.UtcNow.ToString("HH"));
                Hour = Convert.ToString(Z, 2).PadLeft(5, '0');

                W = Convert.ToInt16(DateTime.UtcNow.ToString("mm"));
                Minute = Convert.ToString(W, 2).PadLeft(6, '0');

                //maybe msglink and notice
                MsgLink = Convert.ToString(((X * 31) + Y), 2).PadLeft(10, '0');
                Notice = Convert.ToString(24, 2).PadLeft(7, '0');


                //maybe duration?
                Duration = Convert.ToString(1440, 2).PadLeft(18, '0');

                Action = Convert.ToString(0, 2).PadLeft(1, '0');

                AreaShape = Convert.ToString(0, 2).PadLeft(3, '0');

                //Everything that is the same for each AIS message
                Encode = Type + RepeatIndicator + Mmsi + Spare + DAC + FID + MessageVersion + MsgLink + Notice + Month + Day + Hour + Minute + Duration + Action + Spare;

                LineMessages = new List<string>();
                string temp;
                //EACH AIS message can have 8 subareas
                //EACH subarea can have 4 points
                //Make a line for each coordinate set
                foreach(double[,] area in PolarCoords) {
                    //Sub-Area 0
                    temp = /*Area Shape x3bits*/Convert.ToString(0, 2).PadLeft(3, '0') + /*Scale Factor x2bits*/Convert.ToString(1, 2).PadLeft(2, '0');
                    int lon = (int)((area[0, 1] * 600000)) & (int)(Math.Pow(2, 28) - 1);
                    temp += /*Longitude x28bits*/ Convert.ToString(lon, 2).PadLeft(28, '0');
                    int lat = (int)((area[0, 0] * 600000)) & (int)(Math.Pow(2, 27) - 1);
                    temp += /*Latitude x27bits*/ Convert.ToString(lat, 2).PadLeft(27, '0');
                    temp += /*Precision x3bits*/ "100";
                    temp += /*Radius x12bits*/ "0".PadLeft(12, '0');
                    temp += /*Spare x21bits*/ "0".PadLeft(21, '0');

                    //Polyline of shape = 3
                    //Sub-Areas 1-8
                    int numOfSubAreas = (int)Math.Ceiling((((double)area.Length / 2) - 1) / 4) * 4; //Multiples Of 4 (actual #subareas = this/4)
                    for(int i = 1; i <= numOfSubAreas; i++) {

                        //Each i is a point
                        if((i - 1) % 4 == 0) {
                            temp +=/*Area Shape x3bits*/Convert.ToString(3, 2).PadLeft(3, '0') + /*Scale Factor x2bits*/Convert.ToString(3, 2).PadLeft(2, '0');
                        }
                        if(i < (area.Length / 2))/*Add bearing and range if it is within the index*/
                        {
                            temp += Convert.ToString(Convert.ToInt32(area[i, 0] * 2), 2).PadLeft(10, '0'); //Bearing
                            temp += Convert.ToString(Convert.ToInt32(area[i, 1]), 2).PadLeft(11, '0'); //Range
                        } else {
                            temp += Convert.ToString(720, 2).PadLeft(10, '0'); //Default Bearing
                            temp += Convert.ToString(0, 2).PadLeft(11, '0'); //Default Range
                        }
                        if(((i) / 4) >= 1 && ((i - 1) % 4) == 3) {
                            temp += "0".PadLeft(7, '0'); //Spare 7bits
                        }
                    }
                    LineMessages.Add(temp);
                }
                //End Creation of Area Shape

                //Encode = Payload + DAC + FID + MessageVersion + MsgLink + Notice + Month + Day + Hour + Minute + Duration + Action + Spare;

                for(int i = 0; i < LineMessages.Count; i++) {
                    LineMessages[i] = Encode + LineMessages[i];
                }

            } catch(Exception e) {
                Console.WriteLine("Error: {0}", e.Message);
            }
        } //End of Constructor

        //Armored ASCII Methods
        //Convert from Binary to Ascii
        public void ConvertToAscii(List<string> input) {
            List<string> BinaryStream = input;
            string temp = "";
            foreach(String x in BinaryStream) {
                try {
                    for(int i = 0; i < x.Length; i += 6) { //Every 6 bits turns into one Ascii character
                        temp += Bin2Ascii(x.Substring(i, 6));
                    }
                    AsciiStream.Add(temp);
                    temp = "";
                } catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
        //Construct the messages
        public void MessageConstructor() {
            int numOfSentances; //Calculate Number of Sentances
            int serialnum = ((Convert.ToInt32(DateTime.UtcNow.ToString("mm")) * 60) + Convert.ToInt32(DateTime.UtcNow.ToString("ss"))) % 10; //Different for each line
            string temp;
            foreach(string AA in AsciiStream) {
                double tempNum = AA.Length / 60;
                numOfSentances = Convert.ToInt32(Math.Ceiling(tempNum)) + 1;
                serialnum = (serialnum + 1) % 10;
                try {
                    if(AA.Length * 6 < 372) {
                        temp = "!" + "AIVDM" + "," + numOfSentances + "," + "1" + "," + "," + "A" + ",";
                        temp += AA + "," + "0";
                        temp += "*" + Checksum(temp);
                        AISMessages.Add(temp);
                    } else {
                        for(int i = 1; ((i - 1) * 60) < AA.Length; i++) {
                            temp = "!" + "AIVDM" + "," + numOfSentances + "," + i + ",";

                            if(AA.Substring((i - 1) * 60).Length > 60) {
                                temp += serialnum + "," + "A" + "," + AA.Substring((i - 1) * 60, 60) + "," + "0";
                            } else {
                                temp += serialnum + "," + "A" + "," + AA.Substring((i - 1) * 60, AA.Length - ((i - 1) * 60)) + "," + "0";
                            }

                            temp += "*" + Checksum(temp);
                            AISMessages.Add(temp);
                        }
                    }
                } catch(Exception x) {
                    Console.WriteLine(x.Message);
                }
            }
            string output = "";
            foreach(string message in AISMessages) {
                output += message + "," + timeStamp + "\n";
            }
            try {
                using(StreamWriter AISWriter = new StreamWriter(DirPath + @"/AIS/" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_BERG_MSG.txt")) {
                    AISWriter.Write(output);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);

            }
        }
        //Calculate the Time Stamp
        private int CalcTimeStamp() {
            return Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds());
        }
        public static string Checksum(string Ascii2check) {
            // Compute the checksum by XORing all the character values in the string.
            int checksum = 0;
            for(int i = 1; i < Ascii2check.Length; i++) {
                checksum ^= Convert.ToUInt16(Ascii2check.ToCharArray()[i]);
            }

            // Convert it to hexadecimal (base-16, upper case, most significant nybble first).
            string hexsum = checksum.ToString("X").ToUpper();
            if(hexsum.Length < 2) {
                hexsum = ("00" + hexsum).Substring(hexsum.Length);
            }
            // Display the result
            return hexsum;
        }

        //Convert the string of bits to an integer then to ascii
        //ASCII -> bits
        //ASCII - 48 if (> 40){-8} 
        public char Bin2Ascii(string input) {
            string compare = "0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVW`abcdefghijklmnopqrstuvw";
            char output;
            int temp = 0;
            //Sum the conversion of each bit to its value in decimal
            //[2^5,2^4,2^3,2^2,2^1,2^0]
            //[ 32, 16, 8 , 4 , 2 , 1 ]
            temp += Convert.ToInt32(Convert.ToString(input[0])) * 32;
            temp += Convert.ToInt32(Convert.ToString(input[1])) * 16;
            temp += Convert.ToInt32(Convert.ToString(input[2])) * 8;
            temp += Convert.ToInt32(Convert.ToString(input[3])) * 4;
            temp += Convert.ToInt32(Convert.ToString(input[4])) * 2;
            temp += Convert.ToInt32(Convert.ToString(input[5])) * 1;
            temp += 48;
            if(temp > 87) temp += 8;
            output = Convert.ToChar(temp);
            if(!compare.Contains(output)) {
                Console.WriteLine(output);
            }
            return output;
        }

        //Create the configuration file for first time or for new users
        public void CreateConfig() {
            using(StreamWriter configWriter = new StreamWriter(configPath, false)) {
                configWriter.WriteLine("#Configuration file for the LID program, please only edit between the single quotes");
                configWriter.WriteLine("#Config path: " + Environment.CurrentDirectory + "\n");
                configWriter.WriteLine(@"Files Directory Location: './LID_Files'");
                configWriter.WriteLine(@"Error File Location: './LID_Files/ErrorLogs/Error.txt'");
                configWriter.WriteLine("\nUpdate links only if they have changed");
                configWriter.WriteLine("Website URL: 'https://lidtesting.azurewebsites.net'");
                configWriter.WriteLine(@"Bulletin URL: 'https://www.navcen.uscg.gov/?pageName=iipB12Out'");
                configWriter.WriteLine(@"Chart URL: 'https://www.navcen.uscg.gov/?pageName=iipCharts&Current'");
                configWriter.WriteLine("\n#KML file parameters");
                configWriter.WriteLine(@"#Color is set by TTBBGGRR");
                configWriter.WriteLine(@"#TT is the transparency from 00 being clear to FF being opaque.");
                configWriter.WriteLine(@"#BB, GG, and RR are the level of blue, green, and red respectively");
                configWriter.WriteLine(@"KML Color Berg Limit    : 'ffc702ff'");
                configWriter.WriteLine(@"KML Color Est Berg Limit: 'ff00ffff'");
                configWriter.WriteLine(@"KML Color Sea Ice Limit : 'ffffff00'");
                configWriter.WriteLine(@"KML Line Width: '5'");
                configWriter.WriteLine(@"Message MMSI: '003679999'");
                configWriter.WriteLine(@"Debug: 'False'");
            }
        }

        public void DownloadFromWeb() {
            //Outfile Naming so that verision can be kept and a path from config file is present.
            outfile = (DirPath + @"/Bulletins/" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Bulletin_Pull.txt");
            //Calls Download to grab the html from the bullentin website
            DownloadFile(BulletinUrl, outfile);

            //Parser and neatness
            string text = File.ReadAllText(outfile);
            text = text.Replace("</p>", "");
            text = text.Replace("<p>", "");
            text = text.Replace("	", "");
            text = text.Replace(".", ". ");
            text = text.Replace(",", ", ");
            text = text.Substring(text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)"), text.IndexOf("CANCEL THIS MSG") - text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)") + 31);
            File.WriteAllText(outfile, text);
        }

        //Method that downloads the Bullentin using System.net
        public static void DownloadFile(string Bulletin, string SavedBulletin) {
            WebClient client = new WebClient();
            client.DownloadFile(Bulletin, SavedBulletin);
            client.Dispose();
        }

        //Check if directories exists
        private void DirectoryCheck() {
            Console.Write("Updating Directories...\t\t");
            try { Directory.CreateDirectory(DirPath + @"/Bulletins"); } catch(Exception x) {
                CreateConfig();
                ReadConfig();
                Console.WriteLine("Error Creating Directories, Attempting To Fix By Re-Writing Configuration File");
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }
            Directory.CreateDirectory(DirPath + @"/KML");
            Directory.CreateDirectory(DirPath + @"/ErrorLogs");
            Directory.CreateDirectory(DirPath + @"/AIS");
            Console.WriteLine("Directories Updated");
        }

        //Accessors

        //End of Framework
    }
}
