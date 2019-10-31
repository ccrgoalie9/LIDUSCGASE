using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace KMLmaker {
    class Download {
        string infile;
        string outfile;

        public Download() {
            DownloadFromWeb();
        }

        public void DownloadFromWeb() {

            infile = "Bulletin_Pull.txt";
            outfile = infile.Replace(".txt", ("_" + System.DateTime.UtcNow.ToString().Substring(0, 10).Replace("/", "-") + ".txt"));
            DownloadFile("https://www.navcen.uscg.gov/?pageName=iipB12Out", "Files/Bulletins/" + outfile);

            string text = System.IO.File.ReadAllText("Files/Bulletins/" + outfile);
            text = text.Replace("</p>", "");
            text = text.Replace("<p>", "");
            text = text.Replace("	","");
            text = text.Replace(".",". ");
            text = text.Replace(",",", ");
            text = text.Substring(text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)"), text.IndexOf("CANCEL THIS MSG") - text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)") + 31);
            System.IO.File.WriteAllText("Files/Bulletins/" + outfile, text);
        }

        public static void DownloadFile(string Bulletin, string SavedBulletin) {
            WebClient client = new WebClient();
            client.DownloadFile(Bulletin, SavedBulletin);
        }

        //Accessors
        public string GetOutFile() {
            return ("Files/Bulletins/" + outfile);
        }
    }
}
