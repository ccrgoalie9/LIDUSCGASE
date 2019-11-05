using System;
using System.Net;

namespace LID_Framework {
    class Download {
        string outfile;

        public Download() {
            DownloadFromWeb();
        }

        public void DownloadFromWeb() {
            outfile = (@"Files\Bulletins\" + DateTime.UtcNow.ToString("MM-dd-yyyy") + "_Bulletin_Pull.txt").Replace(" ", "");
            DownloadFile("https://www.navcen.uscg.gov/?pageName=iipB12Out", outfile);

            string text = System.IO.File.ReadAllText(outfile);
            text = text.Replace("</p>", "");
            text = text.Replace("<p>", "");
            text = text.Replace("	", "");
            text = text.Replace(".", ". ");
            text = text.Replace(",", ", ");
            text = text.Substring(text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)"), text.IndexOf("CANCEL THIS MSG") - text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)") + 31);
            System.IO.File.WriteAllText(outfile, text);
        }

        public static void DownloadFile(string Bulletin, string SavedBulletin) {
            WebClient client = new WebClient();
            client.DownloadFile(Bulletin, SavedBulletin);
            client.Dispose();
        }

        //Accessors
        public string GetOutFile() {
            return (outfile);
        }
    }
}
