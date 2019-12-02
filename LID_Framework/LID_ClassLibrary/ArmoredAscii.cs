using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary
{
    class ArmoredAscii
    { 
        public ArmoredAscii(List<string> input, Config config)
        {
            ConvertToAscii(input);
        }

        public void ConvertToAscii(List<string> input)
        {
            List<string> BinaryStream = input;

            /*foreach(String x in BinaryStream)
            {
                List<string> AsciiStream = Encoding.ASCII.GetString(byte[], BinaryStream);
            }*/


        }

    }
    
}
