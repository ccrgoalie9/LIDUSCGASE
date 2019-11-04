﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LID_Framework {
    public partial class LID_Form : Form {

        string[] kmls;

        public LID_Form() {
            InitializeComponent();

            //Get the current Bulletin
            Download test1 = new Download();

            //Get the necessary bits from the bulletin
            Scraper test2 = new Scraper(test1.GetOutFile());

            //Set the length for the KML creator array
            Line[] testKML = new Line[test2.GetCoordinatesIngestors().Length];

            //String of names for the kml files to open automatically later maybe
            kmls = new string[test2.GetCoordinatesIngestors().Length];

            //Create the KML files in format: 'date'_ICEBERGS_'ID'.kml

            int i = 0;
            foreach(Ingestor x in test2.GetCoordinatesIngestors()) {
                kmls[i] = (@"Files\KML\" + System.DateTime.UtcNow.ToString().Substring(0, 10).Replace("/", "_") + "_ICEBERGS_" + x.GetID().ToString() + ".kml").Replace(" ","");
                testKML[i] = new Line(x.GetCoordinates(), (kmls[i]));
                i++;
            }
        }

        private void filesButton_Click(object sender, EventArgs e) {
            string filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + @"\Files";
            Process.Start("explorer.exe", filePath);
        }

        private void EarthButton_Click(object sender, EventArgs e) {
            string earthPath = @"C:\Program Files\Google\Google Earth Pro\client\googleearth.exe";
            string kmlPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + @"\";
            //Make sure the user has google earth installed so we don't cause any errors
            if(File.Exists(earthPath)) {
                //Only until we consolidate the kmls to one file
                foreach(string x in kmls) {
                    Process.Start(kmlPath + x);
                    //Trying to open a second one while the program is opening will cause on error, so wait
                    Thread.Sleep(2500);
                }
            }

        }

        private void ExitButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }
    }
}
