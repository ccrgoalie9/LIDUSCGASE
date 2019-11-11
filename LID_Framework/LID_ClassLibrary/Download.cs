using System;
using System.Net;

namespace LID_ClassLibrary {

    //This class downloads and parses the Bullentin from the IIP website. 
    public class Download {
        //Global Variable for outfile name
        string outfile;

        //Constructor
        public Download(Config config) {
            DownloadFromWeb(config);
        }

        //Loads the bullentin in and parses out the Data.
        public void DownloadFromWeb(Config config) {
            //Outfile Naming so that verision can be kept and a path from config file is present.
            outfile = (config.DirPath + @"\Bulletins\" + DateTime.UtcNow.ToString("yyyy-MM-dd") + "_Bulletin_Pull.txt");
            //Calls Download to grab the html from the bullentin website
            DownloadFile(config.BulletinUrl, outfile);
            
            //Parser and neatness
            string text = System.IO.File.ReadAllText(outfile);
            text = text.Replace("</p>", "");
            text = text.Replace("<p>", "");
            text = text.Replace("	", "");
            text = text.Replace(".", ". ");
            text = text.Replace(",", ", ");
            text = text.Substring(text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)"), text.IndexOf("CANCEL THIS MSG") - text.IndexOf("NORTH AMERICAN ICE SERVICE (NAIS)") + 31);
            System.IO.File.WriteAllText(outfile, text);
        }

        //Method that downloads the Bullentin using System.net
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
