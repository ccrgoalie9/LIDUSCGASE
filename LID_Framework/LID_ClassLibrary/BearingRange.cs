using System;
using System.IO;

namespace LID_ClassLibrary {
    public class BearingRange {
        double a, c, d, R, phi1, phi2, deltaphi, deltalambda, theta1, theta, theta2;
        readonly double[][,] RBSets;
        string output;
        readonly string outFile;

        public BearingRange(Ingestor[] input, Config config) {
            outFile = (config.DirPath + @"\Polar\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Polar.txt");

            //WILL DEFINE LAT/LON BY READING FROM TOMS INPUT (COORDINATES IN DEGREE DECIMAL FORMAT)
            RBSets = new double[input.Length][,];
            ConvertCoordinates(input);
            WriteFile();
        }

        //Main Method
        private void ConvertCoordinates(Ingestor[] input) {
            foreach(Ingestor ingest in input) {
                double[,] coords = ingest.GetCoordinates();
                double[,] temp = new double[ingest.GetCoordinates().Length + 1, 2];
                //Set the first and last set
                temp[0, 0] = coords[0, 0];
                temp[0, 1] = coords[0, 1];
                temp[(temp.Length / 2) - 1, 0] = coords[(coords.Length / 2) - 1, 0];
                temp[(temp.Length / 2) - 1, 1] = coords[(coords.Length / 2) - 1, 1];
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
                        temp[i + 1, 0] = theta;
                        temp[i + 1, 1] = d;
                        output += theta + " " + d + "\n";
                    }
                    output += temp[(temp.Length / 2) - 1, 0] + " " + temp[(temp.Length / 2) - 1, 1] + "\n";
                    RBSets[count] = temp;
                    count++;
                } catch(Exception x) {
                    Console.WriteLine(x.Message);

                }
            }
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
                    decimalFile.Write(output);
                }
            } catch(Exception x) {
                Console.WriteLine(x.Message);
                Console.ReadKey(); //Error
            }
        }
    }
}
