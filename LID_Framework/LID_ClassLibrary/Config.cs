using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary {
    public class Config {
        public string ConfigPath { get; }
        public string DirPath { get; }
        public string BulletinUrl { get; }
        public string ChartUrl { get; }
        public string KmlColor { get; }
        public int KmlWidth { get; }
        public string ErrorFile { get; }

        public Config() {
            ConfigPath = @"C:\Users\" + Environment.UserName + @"\Documents\LID Files" + @"\config.txt";
            Directory.CreateDirectory(ConfigPath);
            if (!File.Exists(ConfigPath.Replace(@"\config.txt", ""))) {
                //Create file from defaults
                using(StreamWriter configWriter = new StreamWriter(ConfigPath, true)) {
                    configWriter.WriteLine("Configuration file for the LID program, please only edit between the single quotes\nThe program must be reloaded for changes to take effect\n");
                    configWriter.WriteLine(@"Files Directory Location: 'C:\Users\" + Environment.UserName + @"\Documents\LID Files'");
                    configWriter.WriteLine("\nUpdate links only if they have changed");
                    configWriter.WriteLine(@"Bulletin URL: 'https://www.navcen.uscg.gov/?pageName=iipB12Out'");
                    configWriter.WriteLine(@"Chart URL: 'https://www.navcen.uscg.gov/?pageName=iipCharts&Current'");
                    configWriter.WriteLine("\nKML file parameters");
                    configWriter.WriteLine(@"KML Color Code: 'ffffe481'");
                    configWriter.WriteLine(@"KML Line Width: '5'");
                    configWriter.WriteLine(@"Error File Location: 'C:\Users\" + Environment.UserName + @"\Documents\LID Files\ErrorLogs\Error.txt'");
                }

            }
            using(StreamReader configReader = new StreamReader(ConfigPath)) {
                string temp;
                while((temp = configReader.ReadLine()) != null){
                    if(temp.Contains("Files Directory Location:")) { DirPath = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); }
                    if(temp.Contains("Bulletin URL:")) { BulletinUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); }
                    if(temp.Contains("Chart URL:")) { ChartUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); }
                    if(temp.Contains("KML Color Code:")) { KmlColor = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); }
                    if(temp.Contains("Error File Location:")) { ErrorFile = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); }
                    try {
                        if(temp.Contains("KML Line Width:")) { KmlWidth = Convert.ToInt32(temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1)); }
                    }catch(Exception e) {
                        Console.WriteLine("Invalid Value for 'KML Line Width'\n" + e.Message);
                        File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + e.Message);
                        //If error, default is 5
                        KmlWidth = 5;
                    }
                }
            }
        }

        //Accessors
        /*
        public string GetDirPath() {
            return dirPath;
        }

        public string GetBulletinURL() {
            return bulletinUrl;
        }

        public string GetChartURL() {
            return chartUrl;
        }

        public string GetKMLColor() {
            return kmlColor;
        }

        public int GetKMLWidth() {
            return kmlWidth;
        }
        */

    }
}
