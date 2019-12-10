using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LID_ClassLibrary {
    class ArmoredAscii {
        public List<string> AsciiStream { get; set; }

        public List<string> AISMessages { get; set; }
        readonly private Config config;
        private DateTime epoch;
        private DateTime current;
        private int timeStamp;


        public ArmoredAscii(List<string> input, Config config) {
            this.config = config;
            epoch = Convert.ToDateTime("1970-1-1");
            current = DateTime.UtcNow;
            timeStamp = CalcTimeStamp();
            AsciiStream = new List<string>();
            AISMessages = new List<string>();
            ConvertToAscii(input);
            MessageConstructor();
            WriteFile();
        }

        //Convert from Binary to Ascii
        public void ConvertToAscii(List<string> input) {
            List<string> BinaryStream = input;
            string temp = "";
            foreach(String x in BinaryStream) {
                try {
                    for(int i = 0; i < x.Length; i += 6) { //Every 6 bits turns into one Ascii character
                        temp += Bin2Ascii(x.Substring(i, 6));
                    }
                    AsciiStream.Add(temp);
                    temp = "";
                } catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }

        //Construct the messages
        public void MessageConstructor() {
            int sentenceNum = 1; //Start at 1 and increase for each line
            int serialnum = ((Convert.ToInt32(DateTime.UtcNow.ToString("mm")) * 60) + Convert.ToInt32(DateTime.UtcNow.ToString("ss"))) % 10; //Different for each line
            string temp;
            foreach(string AA in AsciiStream) {
                serialnum = (serialnum + 1) % 10;
                try {
                    if(AA.Length * 6 < 372) {
                        temp = "!" + "AIVDM" + "," + sentenceNum + "," + "1" + "," + "," + "A" + ",";
                        temp += AA + "," + "0" + "*";
                        temp += Checksum(temp);
                        AISMessages.Add(temp);
                    } else {
                        for(int i = 1; ((i - 1) * 60) < AA.Length; i++) { 
                            temp = "!" + "AIVDM" + "," + sentenceNum + "," + i + ",";

                            if(AA.Substring((i - 1) * 60).Length > 60) {
                                temp += serialnum + "," + "A" + "," + AA.Substring((i - 1) * 60, 60) + "," + "0" + "*";
                            } else {
                                temp += serialnum + "," + "A" + "," + AA.Substring((i - 1) * 60, AA.Length - ((i - 1) * 60)) + "," + "0" + "*";
                            }

                            temp += Checksum(temp);
                            AISMessages.Add(temp);
                        }
                    }
                } catch(Exception x) {
                    Console.WriteLine(x.Message);
                }
                sentenceNum++;
            }
        }

        //Calculate the Time Stamp
        private int CalcTimeStamp() {
            //31,536,000 seconds in a year, 86400 seconds in a day
            int epochSeconds = (Convert.ToInt32(epoch.ToString("yyyy")) * 31536000) + ((DaysOfMonth(Convert.ToInt32(epoch.ToString("MM"))) + Convert.ToInt32(epoch.ToString("dd"))-1) * 86400) + (Convert.ToInt32(epoch.ToString("HH")) * 3600) + (Convert.ToInt32(epoch.ToString("mm")) * 60) + (Convert.ToInt32(epoch.ToString("ss")));
            int currentSeconds = (Convert.ToInt32(current.ToString("yyyy")) * 31536000) + ((DaysOfMonth(Convert.ToInt32(current.ToString("MM"))) + Convert.ToInt32(current.ToString("dd")) - 1) * 86400) + (Convert.ToInt32(current.ToString("HH")) * 3600) + (Convert.ToInt32(current.ToString("mm")) * 60) + (Convert.ToInt32(current.ToString("ss")));
            int tStamp = currentSeconds - epochSeconds;
            return tStamp;
        }

        private int DaysOfMonth(int numMonth) {
            int numOfDays = 0;
            for(int i = 1; i<numMonth; i++) { //Does not include current month
                switch(i) {
                    case 1:
                        numOfDays += 31;
                        break;
                    case 2:
                        numOfDays += 28; //29 in leap years
                        break;
                    case 3:
                        numOfDays += 31;
                        break;
                    case 4:
                        numOfDays += 30;
                        break;
                    case 5:
                        numOfDays += 31;
                        break;
                    case 6:
                        numOfDays += 30;
                        break;
                    case 7:
                        numOfDays += 31;
                        break;
                    case 8:
                        numOfDays += 31;
                        break;
                    case 9:
                        numOfDays += 30;
                        break;
                    case 10:
                        numOfDays += 31;
                        break;
                    case 11:
                        numOfDays += 30;
                        break;
                    case 12:
                        numOfDays += 31;
                        break;
                        }
            }
            return numOfDays;
        }

        public static string Checksum(string Ascii2check) {
            // Compute the checksum by XORing all the character values in the string.
            int checksum = 0;
            for(int i = 1; i < Ascii2check.Length; i++) {
                checksum ^= Convert.ToUInt16(Ascii2check.ToCharArray()[i]);
            }

            // Convert it to hexadecimal (base-16, upper case, most significant nybble first).
            string hexsum = checksum.ToString("X").ToUpper();
            if(hexsum.Length < 2) {
                hexsum = ("00" + hexsum).Substring(hexsum.Length);
            }
            // Display the result
            return hexsum;
        }

        private void WriteFile() {
            string output = "";
            foreach(string message in AISMessages) {
                output += message + "\n";
                try {
                    using(StreamWriter AISWriter = new StreamWriter(config.DirPath + @"\AISToday.txt")) {
                        AISWriter.Write(output);
                    }
                } catch(Exception e) {
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
            //Sum the conversion of each bit to its value in decimal
            //[2^5,2^4,2^3,2^2,2^1,2^0]
            //[ 32, 16, 8 , 4 , 2 , 1 ]
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
            foreach(string output in AsciiStream) {
                Console.WriteLine(output);
            }
            Console.WriteLine("\nAIS Message Debug");
            foreach(string output in AISMessages) {
                Console.WriteLine(output);
            }
        }



    }

}