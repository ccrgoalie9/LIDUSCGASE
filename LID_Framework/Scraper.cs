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
            WriteFile();
        }

        //Accessors
        //Returns the array of all injestors created from the ingested file
        public Ingestor[] GetCoordinatesInjestors() {
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
            outFileName = inFileName.Replace(".txt", ("_" + System.DateTime.Now.ToString().Substring(0, 8).Replace("/", "-") + ".txt"));
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

        private void ScrapeFile() {
            ingests = ingested.Split(' ');
            int count = 0;
            foreach(string x in ingests) {
                if(x.Length >= 6 && x.Length <= 8) {
                    if(x.EndsWith(".")) {
                        count++;
                    }
                }
            }
            coordsIngested = new string[count];
            count = 0;
            string temp = "";
            foreach(string x in ingests) {
                try {
                    if(x.Length >= 6 && x.Length <= 8) {
                        Convert.ToInt32(x.Substring(x.Length-3,1));
                        if(x.EndsWith(".")) {
                            temp += x;
                            coordsIngested[count] = temp;
                            count++;
                            temp = "";
                        } else if(x.EndsWith(",")) {
                            temp += x;
                        } else {
                            temp += x + " ";
                        }
                    }
                } catch(Exception) {

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
            }
        }

        private void WriteFile() {
            try {
                using(StreamWriter decimalFile = new StreamWriter(outFileName)) {
                    decimalFile.Write(output); //Currently set for Test, CHANGE THIS
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

    }//End of Class
}
