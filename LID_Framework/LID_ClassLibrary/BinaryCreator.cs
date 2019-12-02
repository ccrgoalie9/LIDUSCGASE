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



        public BinaryCreator(double[][,] PolarCoords) { //Will take input at some point


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
                Mmsi = Convert.ToString(003679999, 2).PadLeft(30, '0'); //30bits
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
                            temp +=/*Area Shape x3bits*/Convert.ToString(3, 2).PadLeft(3, '0') + /*Scale Factor x2bits*/Convert.ToString(1, 2).PadLeft(2, '0');
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

        public void Debug() {
            foreach(string x in LineMessages) {
                Console.WriteLine(x);
                Console.WriteLine(x.Length);
            }
        }

        public string GetEncoded() {
            return Encode;
        }

        public string GetBinary(int ind) {
            return LineMessages[ind];
        }

        public List<string> GetAllBinaries() {
            return LineMessages;
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