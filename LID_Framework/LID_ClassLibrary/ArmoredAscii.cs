using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary {
    class ArmoredAscii {

        List<string> AsciiStream;
        public ArmoredAscii(List<string> input, Config config) {
            AsciiStream = new List<string>();
            ConvertToAscii(input);
        }

        public void ConvertToAscii(List<string> input) {
            List<string> BinaryStream = input;
            string temp = "";
            byte[] convert;
            int count = 0;
            foreach(String x in BinaryStream) {
                convert = new byte[x.Length/6];
                for(int i = 0; i < x.Length; i++) {
                    temp += x[i];
                    if(i % 6 == 0) {
                        convert[0] = Convert.ToByte(temp);
                        count++;
                        temp = "";
                    }
                }
                //AsciiStream.Add(Encoding.ASCII.GetString(convert[], 6));
            }

        }

    }

}
