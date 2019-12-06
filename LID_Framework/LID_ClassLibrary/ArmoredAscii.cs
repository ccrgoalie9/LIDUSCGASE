using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary {
    class ArmoredAscii {

        public List<string> AsciiStream {get; set;}

        public ArmoredAscii(List<string> input, Config config) {
            AsciiStream = new List<string>();
            ConvertToAscii(input);
        }

        public void ConvertToAscii(List<string> input) {
            List<string> BinaryStream = input;
            string temp = "";
            foreach(String x in BinaryStream) {
                try {
                    for(int i = 0; i < x.Length; i+=6) {
                        temp += Bin2Ascii(x.Substring(i,6));
                    }
                    AsciiStream.Add(temp);
                    temp = "";
                }catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }

        }

        //Convert the string of bits to an integer then to ascii
        //ASCII -> bits
        //ASCII - 48 if (> 40){-8} 
        public char Bin2Ascii(string input) {
            char output;
            int temp = 0;
            temp += Convert.ToInt32(Convert.ToString(input[0])) * 32;
            temp += Convert.ToInt32(Convert.ToString(input[1])) * 16;
            temp += Convert.ToInt32(Convert.ToString(input[2])) * 8;
            temp += Convert.ToInt32(Convert.ToString(input[3])) * 4;
            temp += Convert.ToInt32(Convert.ToString(input[4])) * 2;
            temp += Convert.ToInt32(Convert.ToString(input[5])) * 1;
            /* Debug '*' here -> /
            Console.Write(input[0] + "*32=" + (Convert.ToInt32(Convert.ToString(input[0])) * 32));
            Console.Write(" " + input[1] + "*16=" + (Convert.ToInt32(Convert.ToString(input[1])) * 16));
            Console.Write(" " + input[2] + "*8=" + (Convert.ToInt32(Convert.ToString(input[2])) * 8));
            Console.Write(" " + input[3] + "*4=" + (Convert.ToInt32(Convert.ToString(input[3])) * 4));
            Console.Write(" " + input[4] + "*2=" + (Convert.ToInt32(Convert.ToString(input[4])) * 2));
            Console.Write(" " + input[5] + "*1=" + (Convert.ToInt32(Convert.ToString(input[5])) * 1));
            Console.Write("Raw:" + temp);
            //*/
            //if(temp < 40) temp += 8;
            temp += 48;
            output = Convert.ToChar(temp);
            /* Debug '*' here -> /
            Console.Write(" ASCII:" + temp);
            Console.WriteLine(" Char:" + output);
            */
            return output;
        }

        public void Debug() {
            Console.WriteLine("\nArmored ASCII Debug");
            foreach (string output in AsciiStream) {
                Console.WriteLine(output);
            }
        }

    }

}
