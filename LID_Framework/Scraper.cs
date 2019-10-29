using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace KMLmaker {

    class Scraper {
        private string inFileName;
        private string outFileName;
        private string output;
        private string ingested;
        private string[] ingests;
        private string[] coordsIngested;
        private Ingestor[] coordinates;

        public Scraper(string inFile) {
            inFileName = inFile;
            CheckInput();
            ReadFile();
            ScrapeFile();
            CreateOutput();
            ConvertIngestor();
            WriteFile();
        }

        //Accessors
        //Returns the array of all injestors created from the ingested file
        public Ingestor[] GetCoordinatesIngestors() {
            return coordinates;
        }

        //You can then index for each coordinate set individually without mixing data
        public double[][,] GetCoordinates() {
            double[][,] coords = new double[coordinates.Length][,];
            int i = 0;
            foreach(Ingestor set in coordinates) {
                coords[i] = set.GetCoordinates();
                i++;
            }
            return coords;
        }

        //Overloaded to supply the coordinates of a specific set if you don't want to deal with an array of arrays
        public double[,] GetCoordinates(int i) {
            double[,] coordSet = coordinates[i].GetCoordinates();
            return coordSet;
        }

        public string GetIngested() {
            return ingested;
        }

        public string GetOutput() {
            return output;
        }

        public string GetInFileName() {
            return inFileName;
        }

        public string GetOutFileName() {
            return outFileName;
        }

        //Constructor's Methods
        //Make sure the input is machine readable
        private void CheckInput() {
            if(!(inFileName.EndsWith(".txt"))) {
                inFileName = inFileName + ".txt";
            }
            outFileName = ("LatLong_" + System.DateTime.Now.ToString().Substring(0, 10).Replace("/", "-") + ".txt");
        }

        //Read from the file
        private void ReadFile() {
            try {
                ingested = "";
                using(StreamReader coordFile = new StreamReader(inFileName)) {
                    ingested = coordFile.ReadToEnd();
                    //ingested = ingested.Replace(", ", ","); //Get rid of pesky spaces
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
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
                }
            }
            coordsIngested = new string[count];
            count = 0;
            string temp = "";
            //Parse for usable information
            foreach(string x in ingests) {
                try {
                    if(x.Length >= 6 && x.Length <= 8 && !x.Contains("Z")) {
                        if(x.EndsWith(".")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            temp += x;
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
            try {
                output = "";
                foreach(string x in coordsIngested) {
                    output += x + "\n";
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                output = e.Message;
                Console.ReadKey(); //Error
            }
        }

        //Convert coordinates using the Ingestor class
        private void ConvertIngestor() {
            coordinates = new Ingestor[coordsIngested.Length];
            for(int i = 0; i < coordinates.Length; i++) {
                coordinates[i] = new Ingestor(coordsIngested[i],i);
            }
        }

        //Write the coordinate sets to a text file
        private void WriteFile() {
            try {
                using(StreamWriter decimalFile = new StreamWriter("Files/LatLongs/"+outFileName)) {
                    decimalFile.Write(output); //Currently set for Test, CHANGE THIS
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

    }//End of Class
}
