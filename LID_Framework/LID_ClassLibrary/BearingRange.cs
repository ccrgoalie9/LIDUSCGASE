using System;
using System.IO;

namespace LID_ClassLibrary {
    public class BearingRange {
        double a, c, d, R, phi1, phi2, deltaphi, deltalambda, theta1, theta, theta2;
        readonly double[][,] PolarSets;
        string output;
        readonly string outFile;

        public BearingRange(Ingestor[] input, Config config) {
            outFile = (config.DirPath + @"\Polar\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Polar.txt");

            //WILL DEFINE LAT/LON BY READING FROM TOMS INPUT (COORDINATES IN DEGREE DECIMAL FORMAT)
            PolarSets = new double[input.Length][,];
            ConvertCoordinates(input);
            //Debug();
            WriteFile();
        }

        //Main Method
        private void ConvertCoordinates(Ingestor[] input) {
            int count = 0;
            foreach(Ingestor ingest in input) {
                double[,] coords = ingest.GetCoordinates();
                double[,] temp = new double[ingest.GetCoordinates().Length/2, 2];
                //Set the first and last set
                temp[0, 0] = Math.Round(coords[0, 0],2);
                temp[0, 1] = Math.Round(coords[0, 1],2);
                output += ingest.GetLineType() + "\n";
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
                        temp[i + 1, 0] = Math.Round((360 - theta),2);
                        temp[i + 1, 1] = Math.Round((d / 1000),2); //Distance in Kilometers
                        output += Math.Round((360 - theta),2) + " " + Math.Round((d/1000),2) + "\n";
                    }
                    PolarSets[count] = temp;
                    count++;
                } catch(Exception x) {
                    Console.WriteLine(x.Message);

                }
            }
        }

        //Accessors
        //Return coordinate sets
        public double[][,] GetCoordinates() {
            return PolarSets;
        }

        //Return outfile location
        public string GetOutFile() {
            return outFile;
        }

        public void Debug() {
            bool flag = true;
            Console.WriteLine("\nBearing Range Debug");
            foreach(double[,] x in PolarSets) {
                foreach(double y in x) {
                    Console.Write(y + " ");
                    flag = !flag;
                    if(flag) { Console.WriteLine(""); };
                }
                Console.WriteLine("");
            }
            Console.WriteLine("\nPolar Coordinate Validation");
            ValidatePolar();
        }

        private void ValidatePolar()
        {
            double lat2, lat1, sigma, theta, long2, long1;

            foreach (double[,] Sets in PolarSets) {
                lat1 = Sets[0, 0];
                long1 = Sets[0, 1];
                Console.WriteLine(lat1 + "," + long1 + " ");

                for ( int i =1; i< Sets.Length / 2; i++){
                    sigma = Sets[i, 1];
                    theta = Sets[i, 0];

                    lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(sigma) + Math.Cos(lat1) * Math.Sin(sigma) * Math.Cos(theta));
                    long2 = long1 + Math.Atan2(Math.Sin(theta) * Math.Sin(sigma) * Math.Cos(lat1), Math.Cos(sigma) - Math.Sin(lat1) * Math.Sin(lat2));
                    Console.WriteLine(lat2 + "," + long2 + " "); 

                    lat1 = lat2;
                    long1 = long2;
                }

            }
            

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
