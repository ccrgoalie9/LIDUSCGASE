using System;
using System.IO;
using System.Text;

namespace LID_ClassLibrary {
    public class perl2Csharp {
        public perl2Csharp() {
            // Case statement -- encode message based upon type
            // Common code. All messages start with the same 38 bits....

            ////////////////////////////////////////////////////////////
            // Bits Len   Description
            //    0-5       6   Message Type
            //    6-7       2   Repeat Indicator
            //    8-37     30   MMSI
            string type = "000000"; //6bits
            string placeHolder = "00"; //2bits
            string mmsi = "000000000000000000000000000000"; //30bits
            string payload;




        try {
                Console.WriteLine("binary message test");

                int typeNum = 5;
                string temp = Convert.ToString(typeNum, 2);

                Console.WriteLine(temp);
                type = type.Remove(0, temp.Length);
                type = type + temp;

                payload = type + placeHolder + mmsi;

                Console.WriteLine(payload);
                Console.WriteLine(payload.Length);
                Console.ReadKey();
            } catch(Exception e) {
                Console.WriteLine("Error: {0}", e.Message);
            }
            
        }
    }
}