using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary {
    public class Framework {
        //Declare Variables
        //Configuration variables
        public string ConfigPath { get; set; }
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
        //Scraper variables
        private string inFileName, degOutFile, decOutFile, output, decOutput, ingested;
        private string[] lineTypes, ingests, coordsIngested;
        readonly private string partialPath;
        readonly private string[] append = { "A", "B", "C", "D", "E", "F", "G" };
        //Ingestor Variables
        //private string ingested;
        //private string output;
        private string lineType;
        private double[,] coords;
        //Line Varaibles
        readonly string filepath;
        //Polar Variables
        double a, c, d, R, phi1, phi2, deltaphi, deltalambda, theta1, theta, theta2;
        readonly double[][,] PolarSets;
        //string output;
        //readonly string outFile;
        //

        //Constructor
        public Framework() {
            ConfigPath = @"config.txt";
            if(!File.Exists(ConfigPath)) {
                //Create file from defaults
                CreateConfig();
            }
            if(File.Exists(ConfigPath)) {
                ReadConfig();
            }
        }


        //Modifiers
        public void ReadConfig() {
            int flag = 0;
            int flagCount = 11;
            try {
                using(StreamReader configReader = new StreamReader(ConfigPath)) {
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
                    }
                    if(flag < flagCount) {
                        throw new Exception("Not All Expected Values Were Present");
                    }
                }
            } catch(Exception x) {
                Console.WriteLine("Error in reading configuration file, re-writing");
                ErrorFile = @"C:\Users\" + Environment.UserName + @"\Documents\LID Files\ErrorLogs\Error.txt";
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
                using(StreamReader coordFile = new StreamReader(inFileName)) {
                    ingested = coordFile.ReadToEnd();
                }
            } catch(Exception x) {
                Console.WriteLine(x.Message);
                File.AppendAllText(config.ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
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

        private void CreateOutput() {
            //Create Degree Output
            try {
                int count = 0;
                output = "";
                foreach(string x in coordsIngested) {
                    output += lineTypes[count];
                    if(count == 0) {
                        output += DateTime.UtcNow.ToString(" yyyy-MM-dd");
                    }
                    output += "\n";
                    output += x + "\n";
                    count++;
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                output = e.Message;
                Console.ReadKey(); //Error
            }

            //Create Decimal Output
            try {
                int count = 0;
                decOutput = "";
                foreach(Ingestor x in coordinates) {
                    decOutput += lineTypes[count];
                    if(count == 0) {
                        decOutput += DateTime.UtcNow.ToString(" yyyy-MM-dd");
                    }
                    decOutput += "\n";
                    decOutput += x.GetOutput() + "\n";
                    count++;
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                decOutput = e.Message;
                Console.ReadKey();
            }
        }

        //Convert coordinates using the Ingestor class
        private void ConvertIngestor(int check) {
            coordinates = new Ingestor[coordsIngested.Length];

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

            if(check != 3) {
                for(int i = 0; i < coordinates.Length; i++) {
                    coordinates[i] = new Ingestor(coordsIngested[i], i + 1);
                    coordinates[i].SetLineType(lineTypes[i]);
                }
            } else {
                for(int i = 0; i < coordinates.Length; i++) {
                    coordinates[i] = new Ingestor(coordsIngested[i], i + i, check);
                    coordinates[i].SetLineType(lineTypes[i]);
                }
            }
        }

        //Ingestor Methods
        //Convert ingest to array
        private void ConvertInput() {
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
        }

        //Line methods

        //Write the coordinate sets to a text file
        private void WriteFile() {
            //Output to Degree File
            try {
                using(StreamWriter decimalFile = new StreamWriter(degOutFile)) {
                    decimalFile.Write(output);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }

            //Output to Decimal File
            try {
                using(StreamWriter decimalFile = new StreamWriter(decOutFile)) {
                    decimalFile.Write(decOutput);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        //Line Class
        public Line(Ingestor[] input, Config config) {
            filepath = (config.DirPath + @"\KML\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_ICEBERGS.kml");
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
            string colorCode1 = config.KmlColor1;
            string colorCode2 = config.KmlColor2;
            string colorCode3 = config.KmlColor3;
            /*
            LineStyle lineStyle = new LineStyle {
                Color = Color32.Parse(colorCode),
                Width = config.KmlWidth
            };
            */

            /*
            PolygonStyle PolyStyle = new PolygonStyle {
                Color = Color32.Parse(colorCode)
            };
            */

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
                    Width = config.KmlWidth
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
                    Width = config.KmlWidth
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
                    Width = config.KmlWidth
                };
                SimpleStyle.Polygon = new PolygonStyle {
                    Color = Color32.Parse(colorCode3)
                };
                document.AddStyle(SimpleStyle);
            }

            //LINE STRING & PLACEMARK CONSTRUCTION ZONE
            foreach(Ingestor ingest in input) {
                //One per segment
                LineString linestring = new LineString();
                CoordinateCollection coordinates = new CoordinateCollection();
                linestring.AltitudeMode = AltitudeMode.ClampToGround;
                linestring.Extrude = true;
                linestring.Tessellate = true;

                double[,] coordArray = ingest.GetCoordinates();

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
                    Name = ingest.GetLineType(),
                    Visibility = true,
                    Geometry = linestring,
                    //Moved StyleUrl out in order to be able to set it conditionally
                    //Moved Description out as well
                    Time = lineTimespan                                    //Timespan
                };
                if(ingest.GetLineType().Contains("ESTIMATED ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleEIL), UriKind.Relative); //Uri makes url refrence to indocument style rather than cloud sourced
                    placemark.Description = new Description() { Text = "Based off of data from Greenland" };
                } else if(ingest.GetLineType().Contains("ICEBERG LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                } else if(ingest.GetLineType().Contains("SEA ICE LIMIT")) {
                    placemark.StyleUrl = new Uri(("#" + styleSIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Sea ice within limit displayed" };
                } else {
                    placemark.StyleUrl = new Uri(("#" + styleIL), UriKind.Relative);
                    placemark.Description = new Description() { Text = "Based off of data from the IIP and Canadian Ice Patrol" };
                }

                document.AddFeature(placemark);

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

        //Create the configuration file for first time or for new users
        public void CreateConfig() {
            using(StreamWriter configWriter = new StreamWriter(ConfigPath, false)) {
                configWriter.WriteLine("#Configuration file for the LID program, please only edit between the single quotes");
                configWriter.WriteLine("#Config path: " + Environment.CurrentDirectory + "\n");
                configWriter.WriteLine(@"Files Directory Location: 'C:\Users\" + Environment.UserName + @"\Documents\LID Files'");
                configWriter.WriteLine(@"Error File Location: 'C:\Users\" + Environment.UserName + @"\Documents\LID Files\ErrorLogs\Error.txt'");
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

        public void DownloadFromWeb(Config config) {
            //Outfile Naming so that verision can be kept and a path from config file is present.
            outfile = (config.DirPath + @"\Bulletins\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Bulletin_Pull.txt");
            //Calls Download to grab the html from the bullentin website
            DownloadFile(config.BulletinUrl, outfile);

            //Parser and neatness
            string text = System.IO.File.ReadAllText(outfile);
            text = text.Replace("</p>", "");
            text = text.Replace("<p>", "");
            text = text.Replace("	", "");
            text = text.Replace(".", ". ");
            text = text.Replace(",", ", ");
            text = text.Substring(text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)"), text.IndexOf("CANCEL THIS MSG") - text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)") + 31);
            System.IO.File.WriteAllText(outfile, text);
        }

        //Method that downloads the Bullentin using System.net
        public static void DownloadFile(string Bulletin, string SavedBulletin) {
            WebClient client = new WebClient();
            client.DownloadFile(Bulletin, SavedBulletin);
            client.Dispose();
        }

        //Accessors

        //End of CLI
    }
}
