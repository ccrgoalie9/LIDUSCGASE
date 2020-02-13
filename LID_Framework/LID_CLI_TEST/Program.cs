using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LID_ClassLibrary;

namespace LID_CLI_TEST {
    class Program {
        private static Config config;
        private static DoIt today;
        private static Download todayDownload;
        private static BearingRange todayBR;
        private static Scraper todayScraper;
        private static Line todayLine;
        static void Main(string[] args) {
            config = new Config();
            today = new DoIt(config);
            todayDownload = today.GetDownload();
            todayScraper = today.GetScraper();
            todayLine = today.GetLine();
            todayBR = today.GetBR();
        }
    }
}
