using System;
using System.Net;

namespace LID_ClassLibrary {
    public class Download {
        string outfile;

        public Download(Config config) {
            DownloadFromWeb(config);
        }

        public void DownloadFromWeb(Config config) {
            outfile = (config.DirPath + @"\Bulletins\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Bulletin_Pull.txt");
            DownloadFile(config.BulletinUrl, outfile);

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
