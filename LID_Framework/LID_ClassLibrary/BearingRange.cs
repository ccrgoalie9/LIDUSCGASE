using System;
using System.IO;

namespace LID_ClassLibrary {
    class BearingRange {
        double a, c, d, R, phi1, phi2, deltaphi, deltalambda, theta1, theta;
        double[][,] RBSets;
        string output;
        string outFile;

        public BearingRange(Ingestor[] input, Config config) {
            outFile = (config.DirPath + @"\BearingRange\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_BRSets.txt");

            R = 6371000; //RADIUS OF EARTH IN METERS

            //WILL DEFINE LAT/LON BY READING FROM TOMS INPUT (COORDINATES IN DEGREE DECIMAL FORMAT)
            double[][,] RBSets = new double[input.Length][,];

            foreach(Ingestor ingest in input) {
                double[,] coords = ingest.GetCoordinates();
                double[,] temp = new double[ingest.GetCoordinates().Length + 1, 2];
                //Set the first and last set
                temp[0, 0] = coords[0, 0];
                temp[0, 1] = coords[0, 1];
                temp[temp.Length / 2 - 1, 0] = coords[coords.Length / 2 - 1, 0];
                temp[temp.Length / 2 - 1, 1] = coords[coords.Length / 2 - 1, 1];
                output += ingest.GetLineType() + "\n";
                output += temp[0, 0] + " " + temp[0, 1] + "\n";

                //Do the math
                int count = 0;
                try {
                    for(int i = 0; i < (coords.Length / 2); i++) {
                        for(int j = 0; j <= 1; j++) {
                            coords[i, j] = coords[i, j] * (Math.PI / 180); // Convert to Radians
                        }
                    }
                    for(int i = 0; i < (coords.Length / 2 - 1); i++) {
                        phi1 = coords[0, i];
                        phi2 = coords[0, i + 1];
                        deltaphi = phi2 - phi1;
                        deltalambda = (coords[1, i] - coords[1, i + 1]);

                        //DISTANCE
                        R = 6371000; //RADIUS OF EARTH IN METERS
                        a = Math.Sin(deltaphi / 2) * Math.Sin(deltaphi / 2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Sin(deltalambda / 2) * Math.Sin(deltalambda / 2);
                        c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                        d = R * c; //DISTANCE IN METERS

                        //BEARING
                        theta1 = Math.Atan2(Math.Sin(deltalambda) * Math.Cos(phi2), Math.Cos(phi1) * Math.Sin(phi2) - Math.Sin(phi1) * Math.Cos(phi2) * Math.Cos(deltalambda));
                        theta = theta1 * (180 / Math.PI); //NEGATIVES INDICATE BEARING ON RHS OF PLANE

                        temp[i + 1, 0] = theta;
                        temp[i + 1, 1] = d;
                        output += theta + " " + d + "\n";
                    }
                    RBSets[count] = temp;
                    count++;
                } catch(Exception e) {
                    Console.WriteLine(e.Message);

                }
            }
            WriteFile();
        }

        //Accessors
        //Return coordinate sets
        public double[][,] GetCoordinates() {
            return RBSets;
        }

        //Return outfile location
        public string GetOutFile() {
            return outFile;
        }

        //Output
        private void WriteFile() {
            //Output to File
            try {
                using(StreamWriter decimalFile = new StreamWriter(outFile)) {
                    Console.WriteLine(output);
                    decimalFile.Write(output);
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey(); //Error
            }
        }
    }
}
