﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_Framework
{
    class Download
    {
        public Download()
        {
            DownloadFromWeb();
        }

        public void DownloadFromWeb()
        {
            string infile;
            string outfile;
            infile = "Bulletin_Pull.txt";
            outfile = infile.Replace(".txt", ("_" + System.DateTime.Now.ToString().Substring(0, 8).Replace("/", "_") + ".txt"));
            DownloadFile("https://www.navcen.uscg.gov/?pageName=iipB12Out", "files/" + outfile);

            string text = System.IO.File.ReadAllText("files/" + outfile);
            text = text.Replace("</p>", "");
            text = text.Replace("<p>", "");
            text = text.Substring(text.IndexOf("1. NORTH AMERICAN ICE SERVICE (NAIS)"), text.IndexOf("CANCEL THIS MSG") - text.IndexOf("1. NORTH AMERICAN ICE SERVICE (NAIS)") + 31);
            System.IO.File.WriteAllText("files/" + outfile, text);
        }

        public static void DownloadFile(string Bulletin, string SavedBulletin)
        {
            WebClient client = new WebClient();
            client.DownloadFile(Bulletin, SavedBulletin);
        }

    }
}
