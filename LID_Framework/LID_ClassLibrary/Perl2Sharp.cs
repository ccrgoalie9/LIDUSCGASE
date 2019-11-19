using System;

namespace LID_ClassLibrary {
    class Perl2Sharp {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
        public int S { get; set; }
        public int Long { get; set; }
        public string Type { get; set; }
        public string PlaceHolder { get; set; }
        public string Mmsi { get; set; }
        public string Payload { get; set; }
        public string Encode { get; set; }
        public string DAC { get; set; }
        public string FID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string AreaShape { get; set; }
        public string ScaleFactor { get; set; }
        public string Longitude { get; set; }

        public Perl2Sharp() { //will take input at some point


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
                PlaceHolder = "00"; //2bits
                Mmsi = Convert.ToString(123456789, 2).PadLeft(30, '0'); //30bits


                DAC = Convert.ToString(367, 2).PadLeft(10, '0');

                FID = Convert.ToString(22, 2).PadLeft(6, '0');

                //maybe msglink and notice?

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

                //maybe duration?

                AreaShape = Convert.ToString(0, 2).PadLeft(3, '0');

                Console.WriteLine("What is your scale factor (1, 10, 100, 1000)?: ");
                S = Convert.ToInt16(Console.ReadLine());
                ScaleFactor = Convert.ToString(S, 2).PadLeft(2, '0');

                //need to figure out how to scale this by 600000???
                Console.WriteLine("What is the Latitude?: ");
                Long = Convert.ToInt16(Console.ReadLine());
                Longitude = Convert.ToString(Long, 2).PadLeft(28, '0');


                Payload = Type + PlaceHolder + Mmsi;

                //Creation of AreaShape Will Go HERE
                //
                //
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
}