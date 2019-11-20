using System;
using System.Collections.Generic;
namespace LID_ClassLibrary {
    class BinaryCreator {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
        public int S { get; set; }
        public int Long { get; set; }
        public string Type { get; set; }
        public string RepeatIndicator { get; set; }
        public string Mmsi { get; set; }
        public string Payload { get; set; }
        public string Encode { get; set; }
        public string DAC { get; set; }
        public string FID { get; set; }
        public string Month { get; set; }
        //public string Year { get; set; }
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



        public BinaryCreator(double[][,] PolarCoords) { //Will take input at some point


            // Try Catch statement -- encode message based upon type
            // Common code. All messages start with the same 38 bits....

            ////////////////////////////////////////////////////////////
            // Bits Len   Description
            //    0-5       6   Message Type
            //    6-7       2   Repeat Indicator
            //    8-37     30   MMSI

            try {
                Console.WriteLine("binary message test");

                Type = Convert.ToString(8, 2).PadLeft(6, '0'); //6bits
                RepeatIndicator = Convert.ToString(0, 2).PadLeft(2, '0'); //2bits
                Mmsi = Convert.ToString(003679999, 2).PadLeft(30, '0'); //30bits
                Spare = Convert.ToString(0, 2).PadLeft(2, '0'); //2bits

                MessageVersion = Convert.ToString(2, 2).PadLeft(6, '0');

                DAC = Convert.ToString(367, 2).PadLeft(10, '0');

                FID = Convert.ToString(22, 2).PadLeft(6, '0');

                Console.WriteLine("Month (UTC, ##): ");
                X = Convert.ToInt16(Console.ReadLine());
                Month = Convert.ToString(X, 2).PadLeft(4, '0');

                Console.WriteLine("Day (UTC, ##): ");
                Y = Convert.ToInt16(Console.ReadLine());
                Day = Convert.ToString(Y, 2).PadLeft(5, '0');

                Console.WriteLine("Hour (UTC, ##): ");
                Z = Convert.ToInt16(Console.ReadLine());
                Hour = Convert.ToString(Z, 2).PadLeft(5, '0');

                Console.WriteLine("Minute (UTC, ##): ");
                W = Convert.ToInt16(Console.ReadLine());
                Minute = Convert.ToString(W, 2).PadLeft(6, '0');

                //maybe msglink and notice
                MsgLink = Convert.ToString(((X * 31) + Y), 2).PadLeft(10, '0');
                Notice = Convert.ToString(24, 2).PadLeft(7, '0');


                //maybe duration?
                Duration = Convert.ToString(86400, 2).PadLeft(18, '0');

                AreaShape = Convert.ToString(0, 2).PadLeft(3, '0');

                //DO WE NEEED THIS IF WE ARE ALREADY IN KM??
                Console.WriteLine("What is your scale factor (1, 10, 100, 1000)?: ");
                S = Convert.ToInt16(Console.ReadLine());
                ScaleFactor = Convert.ToString(S, 2).PadLeft(2, '0');

                Payload = Type + RepeatIndicator + Mmsi + Spare;

                //Creation of AreaShape Will Go HERE

                //EACH AIS message can have 8 subareas
                //EACH subarea can have 4 points

                //Make a line for each coordinate set
                foreach(double[,] area in PolarCoords) {
                    //Sub-Area 0
                    string temp = /*Area Shape x3bits*/"000" + /*Scale Factor x2bits*/"00";
                    int lat = ((int)(area[0,0]*600000) & (2^28-1));
                    temp += /*Longitude x28bits*/ Convert.ToString(lat, 2).PadLeft(28, '0');
                    int lon = ((int)(area[0,1]*600000) & (2^27-1));
                    temp += /*Latitude x27bits*/ Convert.ToString(lon, 2).PadLeft(27, '0');
                    temp += /*Precision x3bits*/ "100";
                    temp += /*Radius x12bits*/ "0".PadLeft(12, '0');
                    temp += /*Spare x21bits*/ "0".PadLeft(21, '0');

                    //Polyline of shape = 3
                    //Sub-Areas 1-8
                    for(int i = 1; i < area.Length/2 - 2; i++) {
                        //Each i is a point
                        if ((i-1) % 4 == 0) {
                            temp +=/*Area Shape x3bits*/"011" + /*Scale Factor x2bits*/"00";
                        }
                        int theta = (int)(area[i,0]*100);
                        if(theta % 10 > 5) {

                        }
                    }

                    LineMessages.Add(temp);
                }

                //End Creation of Area Shape

                Encode = Payload + DAC + FID + Month + Day + Hour + Minute + AreaShape + ScaleFactor;

                Console.WriteLine(Encode);
                Console.WriteLine(Encode.Length);
                Console.ReadKey();

            }
            catch (Exception e) {
                Console.WriteLine("Error: {0}", e.Message);
            }

            // Try Catch Statement that creates an array of variables. 
        }



        public string GetEncoded() {
            return Encode;
        }
    }

    //area shape is subarea
    //sub area 0...
    //for point set scale factor to 0 at 2 bits
    //set precision to default value of 4 at 3 bits
    //radius take 0 and encode it to 12 bits
    //21 spare bits which are 0
    //sub area 3...
    //set scale factor (must be consistent for all 4 points in area shape) to 3(1000) for 2 bits if they are giving meters
    //angle is true bearing in half degree steps (243.4 -> 243.5 *2 = 487 encoded into 10 bits
    //take the distance we get and divide by 1000...
    //dist_km = int((dist_m/1000)+.5)). kilometers is in 11 bits
    //
    //formulate data by every 32 points give me a lat long
    //formulate data by every 32 points give me a lat long
    //TOTAL BITS IS NO MORE THAN 984
    //if run out of points set missing angle to 720 and missing to 0
    //at the end of polyline there are 7 spare bits set to 0
    //maybe msglink and notice? have the msglink 10 bits be the julian date (time function) month *31 plus day
    // notice is 24 in 7  bits... ask mr.cline
    //maybe duration 18 bits? 24hrs -> 3600*24 min
    //Action = 0 1 bit
    //string spare2 = "00";

}