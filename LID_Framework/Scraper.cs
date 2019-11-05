using System;
using System.IO;

namespace LID_Framework {
    class Scraper {
        // Summary:
        //     Takes the input of a filename and parses for coordinates. 
        //     It outputs this information in a textfile with the original
        //     degree/decimal format, and in decimal format
        private string inFileName, degOutFile, decOutFile, output, decOutput, ingested;
        private string[] lineTypes, ingests, coordsIngested;
        private Ingestor[] coordinates;

        public Scraper(string inFile) {
            inFileName = inFile;
            CheckInput();
            ReadFile();
            ScrapeFile();
            ConvertIngestor();
            CreateOutput();
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
            foreach (Ingestor set in coordinates) {
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

        //Mainly for debugging
        public string GetIngested() {
            return ingested;
        }

        //Return what is put into the text file
        public string GetOutput() {
            return output;
        }

        //Returns the input filename, mostly for debugging
        public string GetInFileName() {
            return inFileName;
        }

        //Returns the output filenames (either Degree or Decimal)
        public string GetDegOutFile() {
            return degOutFile;
        }

        public string GetDecOutFile() {
            return decOutFile;
        }

        //Constructor's Methods
        //Make sure the input is machine readable
        private void CheckInput() {
            if (!(inFileName.EndsWith(".txt"))) {
                inFileName = inFileName + ".txt";
            }
            degOutFile = (@"Files\LatLongs\" + DateTime.UtcNow.ToString("MM-dd-yyyy") + "_Degree.txt").Replace(" ", "");
            decOutFile = (@"Files\LatLongs\" + DateTime.UtcNow.ToString("MM-dd-yyyy") + "_Decimal.txt").Replace(" ", "");
        }

        //Read from the file
        private void ReadFile() {
            try {
                ingested = "";
                using (StreamReader coordFile = new StreamReader(inFileName)) {
                    ingested = coordFile.ReadToEnd();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                ingested = "Read Failed";
            }
        }

        //Create the coordinatesIngested array
        private void ScrapeFile() {
            ingests = ingested.Split(' ');
            int count = 0;
            //Determine necessary length of array
            foreach (string x in ingests) {
                try {
                    if (x.Length >= 6 && x.Length <= 8 && !x.Contains("Z")) {
                        if (x.EndsWith(".")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            count++;
                        }
                    }
                }
                catch (Exception) {
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
            foreach (string x in ingests) {
                //For Section Headers
                if (x == "ESTIMATED" || x == "WESTERN" || x == "EASTERN" || x == "NORTHERN" || x == "SOUTHERN" || x == "SEA") {
                    temp1 = x;
                }
                if (x == "ICEBERG" || x == "ICE") {
                    temp2 = x;
                }
                if (x == "LIMIT") {
                    if (temp1 != "") tempHeader = (temp1 + " " + temp2 + " " + x);
                    else if (temp2 != "") tempHeader = (temp2 + " " + x);
                    temp1 = "";
                    temp2 = "";
                }

                //For Coordinates
                try {
                    if (x.Length >= 6 && x.Length <= 8 && !x.Contains("Z")) {
                        if (x.EndsWith(".")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            temp += x;
                            if (tempHeader != "") lineTypes[count] = tempHeader;
                            else lineTypes[count] = lineTypes[count - 1];
                            tempHeader = "";
                            coordsIngested[count] = temp;
                            count++;
                            temp = "";
                        }
                        else if (x.EndsWith(",")) {
                            Convert.ToInt32(x.Substring(x.Length - 3, 1));
                            temp += x;
                        }
                        else {
                            Convert.ToInt32(x.Substring(x.Length - 2, 1));
                            temp += x + " ";
                        }
                    }
                }
                catch (Exception) {
                    //Do nothing
                }
            }
        }

        private void CreateOutput() {
            //Create Degree Output
            try {
                int count = 0;
                output = "";
                foreach (string x in coordsIngested) {
                    output += lineTypes[count] + "\n";
                    output += x + "\n";
                    count++;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                output = e.Message;
                Console.ReadKey(); //Error
            }

            //Create Decimal Output
            try {
                int count = 0;
                decOutput = "";
                foreach (Ingestor x in coordinates) {
                    decOutput += lineTypes[count] + "\n";
                    decOutput += x.GetOutput() + "\n";
                    count++;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                decOutput = e.Message;
                Console.ReadKey();
            }
        }

        //Convert coordinates using the Ingestor class
        private void ConvertIngestor() {
            coordinates = new Ingestor[coordsIngested.Length];
            for (int i = 0; i < coordinates.Length; i++) {
                coordinates[i] = new Ingestor(coordsIngested[i], i + 1);
                coordinates[i].SetLineType(lineTypes[i]);
            }
        }

        //Write the coordinate sets to a text file
        private void WriteFile() {
            //Output to Degree File
            try {
                using (StreamWriter decimalFile = new StreamWriter(degOutFile)) {
                    decimalFile.Write(output);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey(); //Error
            }

            //Output to Decimal File
            try {
                using (StreamWriter decimalFile = new StreamWriter(decOutFile)) {
                    decimalFile.Write(decOutput);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey(); //Error
            }
        }

    }//End of Class
}
