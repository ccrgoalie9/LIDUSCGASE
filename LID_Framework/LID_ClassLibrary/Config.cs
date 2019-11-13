using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LID_ClassLibrary {
    public class Config {
        public string ConfigPath { get; set; }
        public string DirPath { get; set; }
        public string BulletinUrl { get; set; }
        public string ChartUrl { get; set; }
        public string KmlColor { get; set; }
        public int KmlWidth { get; set; }
        public string ErrorFile { get; set; }

        public Config() {
            ConfigPath = @"config.txt";
            if(!File.Exists(ConfigPath)) {
                //Create file from defaults
                CreateConfig();
            }
            if(File.Exists(ConfigPath)) {
                ReadConfig();
            }
        }

        //Modifiers
        public void ReadConfig() {
            int flag = 0;
            try {
                using(StreamReader configReader = new StreamReader(ConfigPath)) {
                    string temp;
                    while((temp = configReader.ReadLine()) != null) {
                        if(temp.Contains("Files Directory Location:")) { DirPath = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Bulletin URL:")) { BulletinUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Chart URL:")) { ChartUrl = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("KML Color Code:")) { KmlColor = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        if(temp.Contains("Error File Location:")) { ErrorFile = temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1); flag++; }
                        try {
                            if(temp.Contains("KML Line Width:")) { KmlWidth = Convert.ToInt32(temp.Substring(temp.IndexOf('\'') + 1, temp.LastIndexOf('\'') - temp.IndexOf('\'') - 1)); flag++; }
                        } catch(Exception x) {
                            Console.WriteLine("Invalid Value for 'KML Line Width'\n" + x.Message);
                            File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
                            //If error, default is 5
                            KmlWidth = 5;
                        }
                    }
                    if(flag < 6) {
                        throw new Exception("Not All Expected Values Were Present");
                    }
                }
            } catch(Exception x) {
                Console.WriteLine("Error in reading configuration file, re-writing");
                CreateConfig();
                ReadConfig();
                File.AppendAllText(ErrorFile, DateTime.UtcNow.ToString("HH:mm:ss") + " : " + x.Message + "\n");
            }
        }

        //Create the configuration file for first time or for new users
        public void CreateConfig() {
            using(StreamWriter configWriter = new StreamWriter(ConfigPath, false)) {
                configWriter.WriteLine("Configuration file for the LID program, please only edit between the single quotes\n");
                configWriter.WriteLine(@"Files Directory Location: 'C:\Users\" + Environment.UserName + @"\Documents\LID Files'");
                configWriter.WriteLine(@"Error File Location: 'C:\Users\" + Environment.UserName + @"\Documents\LID Files\ErrorLogs\Error.txt'");
                configWriter.WriteLine("\nUpdate links only if they have changed");
                configWriter.WriteLine(@"Bulletin URL: 'https://www.navcen.uscg.gov/?pageName=iipB12Out'");
                configWriter.WriteLine(@"Chart URL: 'https://www.navcen.uscg.gov/?pageName=iipCharts&Current'");
                configWriter.WriteLine("\nKML file parameters");
                configWriter.WriteLine(@"Color is set by TTBBGGRR");
                configWriter.WriteLine(@"TT is the transparency from 00 being clear to FF being opaque.");
                configWriter.WriteLine(@"BB, GG, and RR are the level of blue, green, and red respectively");
                configWriter.WriteLine(@"KML Color Code: 'ffffe481'");
                configWriter.WriteLine(@"KML Line Width: '5'");
            }
        }

    }
}
