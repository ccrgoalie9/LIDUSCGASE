using System;
using System.IO;

namespace LID_Framework {
    public class Scraper {
        // Summary:
        //     Takes the input of a filename and parses for coordinates. 
        //     It outputs this information in a textfile with the original
        //     degree/decimal format, and in decimal format
        private string inFileName, degOutFile, decOutFile, output, decOutput, ingested;
        private string[] lineTypes, ingests, coordsIngested, append = {"A","B","C","D","E","F","G"};
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

        public Scraper(string inFile, int func) {
            inFileName = inFile;
            CheckInput(inFile);
            ReadFile(func);
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
            if(!(inFileName.EndsWith(".txt"))) {
                inFileName = inFileName + ".txt";
            }
            if(inFileName.Substring(inFileName.LastIndexOf(@"\") + 1, 10) == DateTime.UtcNow.ToString("yyyy-MM-dd")) {
                degOutFile = (@"Files\LatLongs\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Degree.txt").Replace(" ", "");
                decOutFile = (@"Files\LatLongs\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Decimal.txt").Replace(" ", "");
            } else {
                degOutFile = inFileName.Replace("_Bulletin_Pull", "_Degree").Replace("Bulletins", "LatLongs");
                decOutFile = inFileName.Replace("_Bulletin_Pull", "_Decimal").Replace("Bulletins", "LatLongs");
            }
        }

        private void CheckInput(string func) {
            if(func.Contains("_Decimal")) {
                degOutFile = func.Replace("_Decimal.txt", "_Degree.txt");
                decOutFile = func;
            } else if(func.Contains("_Degree")) {
                degOutFile = func;
                decOutFile = func.Replace("_Degree.txt", "_Decimal.txt");
            } else {

            }

        }

        //Read from the file
        private void ReadFile() {
            try {
                ingested = "";
                using(StreamReader coordFile = new StreamReader(inFileName)) {
                    ingested = coordFile.ReadToEnd();
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                ingested = "Read Failed";
            }
        }

        private void ReadFile(int func) {
            string temp;
            int count = 0;
            bool flag = true;
            if(func == 1) {
                using(StreamReader coordFile = new StreamReader(inFileName)) {
                    while((temp = coordFile.ReadLine()) != null) {
                        count++;
                        try {
                            Convert.ToInt32(temp.Substring(0, 1));
                        } catch(Exception) {
                            flag = false;
                        }
                    }
                }
                if(flag) {
                    coordsIngested = new string[count];
                    lineTypes = new string[count];
                    count = 0;
                    using(StreamReader coordFile = new StreamReader(inFileName)) {
                        while((temp = coordFile.ReadLine()) != null) {
                            coordsIngested[count] = temp;
                            lineTypes[count] = "";
                            count++;
                        }
                    }
                } else {
                    coordsIngested = new string[count / 2];
                    lineTypes = new string[count / 2];
                    count = 0;
                    using(StreamReader coordFile = new StreamReader(inFileName)) {
                        while((temp = coordFile.ReadLine()) != null) {
                            lineTypes[count] = temp;
                            count++;
                            coordsIngested[count] = temp;
                            count++;
                        }
                    }
                }
            } else if(func == 2) {

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
                    output += lineTypes[count] + "\n";
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
                    decOutput += lineTypes[count] + "\n";
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
        private void ConvertIngestor() {
            coordinates = new Ingestor[coordsIngested.Length];

            //Add Letters To Reflect Subsections of the same Set
            string indexes = "";
            string temp = "";
            for(int i = 0; i < lineTypes.Length - 1; i++) {
                temp = "";
                if(lineTypes[i] == lineTypes[i+1]) {
                    temp = (i + 1).ToString();
                    indexes += i.ToString() + " ";
                }
            }
            if(temp != "") {
                indexes += temp;
            }
            int[] indexer = new int[indexes.Split().Length];
            int count = 0;
            foreach(string x in indexes.Split()) {
                indexer[count] = Convert.ToInt32(x);
                count++;
            }
            for(int i = 0; i < indexer.Length; i++) {
                lineTypes[indexer[i]] += " " + append[i];
            }
            //End Adding Letters

            for(int i = 0; i < coordinates.Length; i++) {
                coordinates[i] = new Ingestor(coordsIngested[i], i + 1);
                coordinates[i].SetLineType(lineTypes[i]);
            }
        }

        //Write the coordinate sets to a text file
        private void WriteFile() {
            //Output to Degree File
            try {
                using(StreamWriter decimalFile = new StreamWriter(degOutFile)) {
                    decimalFile.Write(output);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey(); //Error
            }

            //Output to Decimal File
            try {
                using(StreamWriter decimalFile = new StreamWriter(decOutFile)) {
                    decimalFile.Write(decOutput);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey(); //Error
            }
        }

    }//End of Class
}
